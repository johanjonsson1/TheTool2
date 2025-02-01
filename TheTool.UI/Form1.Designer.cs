namespace TheTool.UI;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
        getAllButton = new Button();
        refreshButton = new Button();
        listBoxSearch = new ListBox();
        searchTextbox = new TextBox();
        searchButton = new Button();
        progressBar1 = new ProgressBar();
        getSelectedButton = new Button();
        toggleLogButton = new Button();
        label1 = new Label();
        tableLayoutPanel1 = new TableLayoutPanel();
        flowLayoutPanel1 = new FlowLayoutPanel();
        flowLayoutPanel2 = new FlowLayoutPanel();
        listBoxDir = new ListBox();
        comboBoxTags = new ComboBox();
        logWindow1 = new Logging.LogWindow();
        tableLayoutPanel1.SuspendLayout();
        flowLayoutPanel1.SuspendLayout();
        flowLayoutPanel2.SuspendLayout();
        SuspendLayout();
        // 
        // getAllButton
        // 
        getAllButton.Location = new Point(3, 3);
        getAllButton.Name = "getAllButton";
        getAllButton.Size = new Size(195, 80);
        getAllButton.TabIndex = 0;
        getAllButton.Text = "Get all";
        getAllButton.UseVisualStyleBackColor = true;
        getAllButton.Click += getAllButton_Click;
        // 
        // refreshButton
        // 
        refreshButton.Location = new Point(3, 89);
        refreshButton.Name = "refreshButton";
        refreshButton.Size = new Size(195, 80);
        refreshButton.TabIndex = 2;
        refreshButton.Text = "Refresh all";
        refreshButton.UseVisualStyleBackColor = true;
        refreshButton.Click += refreshButton_Click;
        // 
        // listBoxSearch
        // 
        listBoxSearch.DisplayMember = "Name";
        listBoxSearch.Dock = DockStyle.Fill;
        listBoxSearch.FormattingEnabled = true;
        listBoxSearch.ItemHeight = 15;
        listBoxSearch.Location = new Point(3, 36);
        listBoxSearch.Name = "listBoxSearch";
        listBoxSearch.Size = new Size(486, 506);
        listBoxSearch.TabIndex = 3;
        listBoxSearch.ValueMember = "Name";
        // 
        // searchTextbox
        // 
        searchTextbox.BorderStyle = BorderStyle.FixedSingle;
        searchTextbox.Location = new Point(3, 3);
        searchTextbox.Name = "searchTextbox";
        searchTextbox.Size = new Size(321, 23);
        searchTextbox.TabIndex = 4;
        // 
        // searchButton
        // 
        searchButton.Location = new Point(330, 3);
        searchButton.Name = "searchButton";
        searchButton.Size = new Size(75, 23);
        searchButton.TabIndex = 6;
        searchButton.Text = "Search";
        searchButton.UseVisualStyleBackColor = true;
        searchButton.Click += searchButton_Click;
        // 
        // progressBar1
        // 
        progressBar1.Dock = DockStyle.Top;
        progressBar1.ForeColor = Color.DarkSeaGreen;
        progressBar1.Location = new Point(711, 3);
        progressBar1.Name = "progressBar1";
        progressBar1.Size = new Size(200, 27);
        progressBar1.TabIndex = 7;
        // 
        // getSelectedButton
        // 
        getSelectedButton.Enabled = false;
        getSelectedButton.Location = new Point(3, 175);
        getSelectedButton.Name = "getSelectedButton";
        getSelectedButton.Size = new Size(195, 80);
        getSelectedButton.TabIndex = 8;
        getSelectedButton.Text = "Get selected";
        getSelectedButton.UseVisualStyleBackColor = true;
        getSelectedButton.Click += getSelectedButton_Click;
        // 
        // toggleLogButton
        // 
        toggleLogButton.Location = new Point(3, 261);
        toggleLogButton.Name = "toggleLogButton";
        toggleLogButton.Size = new Size(195, 80);
        toggleLogButton.TabIndex = 9;
        toggleLogButton.Text = "Show log";
        toggleLogButton.UseVisualStyleBackColor = true;
        toggleLogButton.Click += toggleLogButton_Click;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(438, 8);
        label1.Margin = new Padding(30, 8, 3, 0);
        label1.Name = "label1";
        label1.Size = new Size(45, 15);
        label1.TabIndex = 11;
        label1.Text = "0 items";
        // 
        // tableLayoutPanel1
        // 
        tableLayoutPanel1.AutoSize = true;
        tableLayoutPanel1.ColumnCount = 3;
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 69.49152F));
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30.5084743F));
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 205F));
        tableLayoutPanel1.Controls.Add(flowLayoutPanel1, 2, 1);
        tableLayoutPanel1.Controls.Add(progressBar1, 2, 0);
        tableLayoutPanel1.Controls.Add(listBoxSearch, 0, 1);
        tableLayoutPanel1.Controls.Add(flowLayoutPanel2, 0, 0);
        tableLayoutPanel1.Controls.Add(listBoxDir, 1, 1);
        tableLayoutPanel1.Controls.Add(comboBoxTags, 1, 0);
        tableLayoutPanel1.Controls.Add(logWindow1, 0, 2);
        tableLayoutPanel1.Dock = DockStyle.Fill;
        tableLayoutPanel1.Location = new Point(0, 0);
        tableLayoutPanel1.Name = "tableLayoutPanel1";
        tableLayoutPanel1.RowCount = 3;
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 5.009634F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 77.60968F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 17.3978825F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
        tableLayoutPanel1.Size = new Size(914, 661);
        tableLayoutPanel1.TabIndex = 13;
        // 
        // flowLayoutPanel1
        // 
        flowLayoutPanel1.Controls.Add(getAllButton);
        flowLayoutPanel1.Controls.Add(refreshButton);
        flowLayoutPanel1.Controls.Add(getSelectedButton);
        flowLayoutPanel1.Controls.Add(toggleLogButton);
        flowLayoutPanel1.Dock = DockStyle.Fill;
        flowLayoutPanel1.Location = new Point(711, 36);
        flowLayoutPanel1.Name = "flowLayoutPanel1";
        flowLayoutPanel1.Size = new Size(200, 506);
        flowLayoutPanel1.TabIndex = 14;
        // 
        // flowLayoutPanel2
        // 
        flowLayoutPanel2.Controls.Add(searchTextbox);
        flowLayoutPanel2.Controls.Add(searchButton);
        flowLayoutPanel2.Controls.Add(label1);
        flowLayoutPanel2.Location = new Point(3, 3);
        flowLayoutPanel2.Name = "flowLayoutPanel2";
        flowLayoutPanel2.Size = new Size(486, 27);
        flowLayoutPanel2.TabIndex = 15;
        // 
        // listBoxDir
        // 
        listBoxDir.DisplayMember = "Name";
        listBoxDir.Dock = DockStyle.Fill;
        listBoxDir.FormattingEnabled = true;
        listBoxDir.ItemHeight = 15;
        listBoxDir.Location = new Point(495, 36);
        listBoxDir.Name = "listBoxDir";
        listBoxDir.Size = new Size(210, 506);
        listBoxDir.TabIndex = 16;
        listBoxDir.ValueMember = "Path";
        // 
        // comboBoxTags
        // 
        comboBoxTags.Dock = DockStyle.Fill;
        comboBoxTags.FormattingEnabled = true;
        comboBoxTags.Location = new Point(495, 6);
        comboBoxTags.Margin = new Padding(3, 6, 3, 3);
        comboBoxTags.Name = "comboBoxTags";
        comboBoxTags.Size = new Size(210, 23);
        comboBoxTags.TabIndex = 17;
        // 
        // logWindow1
        // 
        logWindow1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        logWindow1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        tableLayoutPanel1.SetColumnSpan(logWindow1, 3);
        logWindow1.Location = new Point(3, 548);
        logWindow1.Name = "logWindow1";
        logWindow1.Size = new Size(908, 110);
        logWindow1.TabIndex = 18;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        AutoSizeMode = AutoSizeMode.GrowAndShrink;
        ClientSize = new Size(914, 661);
        Controls.Add(tableLayoutPanel1);
        Icon = (Icon)resources.GetObject("$this.Icon");
        MinimumSize = new Size(930, 700);
        Name = "Form1";
        SizeGripStyle = SizeGripStyle.Show;
        Text = "TheTool";
        FormClosing += Form1_FormClosing;
        tableLayoutPanel1.ResumeLayout(false);
        flowLayoutPanel1.ResumeLayout(false);
        flowLayoutPanel2.ResumeLayout(false);
        flowLayoutPanel2.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Button getAllButton;
    private Button refreshButton;
    private ListBox listBoxSearch;
    private TextBox searchTextbox;
    private Button searchButton;
    private ProgressBar progressBar1;
    private Button getSelectedButton;
    private Button toggleLogButton;
    private Label label1;
    private Logging.LogWindow logWindow1;
    private TableLayoutPanel tableLayoutPanel1;
    private FlowLayoutPanel flowLayoutPanel1;
    private FlowLayoutPanel flowLayoutPanel2;
    private ListBox listBoxDir;
    private ComboBox comboBoxTags;
}
