namespace TheTool.UI.Logging;

partial class LogWindow
{
    /// <summary> 
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// Clean up any resources being used.
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogWindow));
        logTextBox = new RichTextBox();
        SuspendLayout();
        // 
        // logTextBox
        // 
        logTextBox.Dock = DockStyle.Fill;
        logTextBox.HideSelection = false;
        logTextBox.Location = new Point(0, 0);
        logTextBox.Name = "logTextBox";
        logTextBox.ReadOnly = true;
        logTextBox.Size = new Size(832, 195);
        logTextBox.TabIndex = 2;
        logTextBox.Text = resources.GetString("logTextBox.Text");
        // 
        // LogWindow
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        AutoSizeMode = AutoSizeMode.GrowAndShrink;
        Controls.Add(logTextBox);
        Name = "LogWindow";
        Size = new Size(832, 195);
        ResumeLayout(false);
    }

    #endregion

    internal RichTextBox logTextBox;
}
