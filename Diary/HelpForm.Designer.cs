namespace Diary;

partial class HelpForm
{
    private System.ComponentModel.IContainer components = null;
    private RichTextBox richTextBoxHelp;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();

        richTextBoxHelp = new RichTextBox()
        {
            Dock = DockStyle.Fill,
            ReadOnly = true,
            BorderStyle = BorderStyle.None,
            Font = new Font("Segoe UI", 10F),
            BackColor = Color.White,
            Margin = new Padding(20)
        };

        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(620, 480);
        StartPosition = FormStartPosition.CenterParent;
        MinimizeBox = false;
        MaximizeBox = false;
        Text = "Довідка";
        Padding = new Padding(20);

        Controls.Add(richTextBoxHelp);
    }
}
