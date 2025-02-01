using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using TheTool.Core;
using TheTool.UI.Logging;
using TheTool.UI.Settings;
using TheTool.UI.Transport;

namespace TheTool.UI;

public partial class Form1 : Form
{
    private readonly IServerClient _serverClient;
    private readonly ILogger<Form1> _logger1;
    private readonly IServiceProvider _serviceProvider;
    public List<SourceDirectory> Cache { get; set; } = new List<SourceDirectory>();

    public LogWindow LogWindow => this.logWindow1;
    
    public Form1(IServerClient serverClient, ILogger<Form1> logger1, IServiceProvider serviceProvider)
    {
        InitializeComponent();
        _serverClient = serverClient;
        _logger1 = logger1;
        _serviceProvider = serviceProvider;

        var tags = serviceProvider.GetRequiredService<SourceConfig>().Sources.Select(s => s.Tag).Distinct();

        this.comboBoxTags.Items.AddRange(tags.ToArray());

        this.listBoxSearch.SelectedValueChanged += ListBoxSearchOnSelectedValueChanged;
        this.listBoxDir.SelectedValueChanged += ListBoxDirOnSelectedValueChanged;
        this.searchTextbox.KeyUp += SearchTextboxOnKeyUp;
    }

    private void SearchTextboxOnKeyUp(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            searchButton_Click(sender, e);
        }
    }

    private void ListBoxSearchOnSelectedValueChanged(object? sender, EventArgs e)
    {
        var listBoxSearchHasSelectedItem = listBoxSearch.SelectedItem != null;
        if (listBoxSearchHasSelectedItem)
        {
            listBoxDir.ClearSelected();
        }

        getSelectedButton.Enabled = listBoxSearchHasSelectedItem;
    }

    private void ListBoxDirOnSelectedValueChanged(object? sender, EventArgs e)
    {
        var listBoxDirHasSelectedItem = listBoxDir.SelectedItem != null;
        if (listBoxDirHasSelectedItem)
        {
            listBoxSearch.ClearSelected();
        }

        getSelectedButton.Enabled = listBoxDirHasSelectedItem;
    }

    private async void getAllButton_Click(object sender, EventArgs e)
    {
        try
        {
            ShowProgressBar();

            var messages = await _serverClient.Get();
            Cache = messages;
            _logger1.LogDebug("Messages received");

            ResetListbox();
        }
        finally
        {
            HideProgressBar();
        }
    }

    private void ResetListbox()
    {
        listBoxSearch.BeginUpdate();
        listBoxSearch.Items.Clear();

        foreach (var item in Cache)
        {
            listBoxSearch.Items.Add(item);
        }

        listBoxSearch.EndUpdate();

        label1.Text = listBoxSearch.Items.Count + " items";
    }

    private void Form1_FormClosing(object sender, FormClosingEventArgs e)
    {
        _logger1.LogDebug("Closing form");
        Task.Delay(500).Wait();
    }

    private async void refreshButton_Click(object sender, EventArgs e)
    {
        await _serverClient.RefreshSources();
    }

    private void searchButton_Click(object sender, EventArgs e)
    {
        var searchPattern = searchTextbox.Text.AsSpan();

        if (searchPattern.IsEmpty)
        {
            ResetListbox();
            return;
        }

        if (searchPattern.Length < 3)
        {
            return;
        }

        ShowProgressBar();

        getSelectedButton.Enabled = false;
        listBoxSearch.BeginUpdate();
        listBoxSearch.Items.Clear();

        if (comboBoxTags.SelectedItem is string tag && !string.IsNullOrEmpty(tag))
        {
            foreach (var item in Cache)
            {
                if (!item.Tag.Equals(tag))
                {
                    continue;
                }

                if (IsMatch(item.Name.AsSpan(), searchPattern))
                {
                    listBoxSearch.Items.Add(item);
                }
            }
        }
        else
        {
            foreach (var item in Cache)
            {
                if (IsMatch(item.Name.AsSpan(), searchPattern))
                {
                    listBoxSearch.Items.Add(item);
                }
            }
        }

        listBoxSearch.EndUpdate();

        label1.Text = listBoxSearch.Items.Count + " items";

        HideProgressBar();
    }

    private static bool IsMatch(ReadOnlySpan<char> input, ReadOnlySpan<char> pattern)
    {
        return input.Contains(pattern, StringComparison.OrdinalIgnoreCase);
    }

    private void toggleLogButton_Click(object sender, EventArgs e)
    {
        if (logWindow1.Visible)
        {
            tableLayoutPanel1.RowStyles[2].SizeType = SizeType.AutoSize;
            logWindow1.Visible = false;
            toggleLogButton.Text = "Show log";
        }
        else
        {
            tableLayoutPanel1.RowStyles[2].SizeType = SizeType.Percent;
            logWindow1.Visible = true;
            toggleLogButton.Text = "Hide log";
        }
    }

    private void ShowProgressBar()
    {
        progressBar1.Style = ProgressBarStyle.Marquee;
        progressBar1.MarqueeAnimationSpeed = 300;
    }

    private void HideProgressBar()
    {
        progressBar1.Style = ProgressBarStyle.Continuous;
        progressBar1.MarqueeAnimationSpeed = 0;
    }

    private async void getSelectedButton_Click(object sender, EventArgs e)
    {
        var dir = listBoxSearch.SelectedItem as SourceDirectory;
        var file = listBoxDir.SelectedItem as SourceFile;

        if (dir is null && file is null)
        {
            return;
        }

        try
        {
            ShowProgressBar();
            getSelectedButton.Enabled = false;

            if (dir is not null)
            {
                listBoxDir.BeginUpdate();
                listBoxDir.Items.Clear();
                listBoxDir.EndUpdate();
                var result = await _serverClient.ScanSource(dir);

                _logger1.LogDebug("Finished scanning directory {Directory}", dir.Name);

                listBoxDir.BeginUpdate();
                foreach (var sourceFile in result)
                {
                    listBoxDir.Items.Add(sourceFile);
                }
                listBoxDir.EndUpdate();
            }
            else if (file is not null)
            {
                await _serverClient.RequestFromSource(file);

                _logger1.LogDebug("Finished requesting item {Item}", file.Name);

                var clientSettings = _serviceProvider.GetRequiredService<ClientSettings>();
                if (clientSettings.ActionCompletedFolder != null && Directory.Exists(clientSettings.ActionCompletedFolder))
                {
                    Process.Start("explorer.exe", clientSettings.ActionCompletedFolder);
                }
            }
        }
        catch (Exception ex)
        {
            if (dir is not null)
            {
                _logger1.LogError(ex, "Failed scanning directory {Directory}", dir.Name);
            }
            else if (file is not null)
            {
                _logger1.LogError(ex, "Failed requesting item {Item}", file.Name);
            }
        }
        finally
        {
            getSelectedButton.Enabled = true;
            HideProgressBar();
        }
    }
}
