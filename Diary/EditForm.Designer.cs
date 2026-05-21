namespace Diary;

partial class EditForm
{
    private System.ComponentModel.IContainer components = null;

    private Label labelDate;
    private DateTimePicker dateTimePickerDate;
    private Label labelTime;
    private DateTimePicker dateTimePickerTime;
    private Label labelDuration;
    private NumericUpDown numericDuration;
    private Label labelPlace;
    private TextBox textBoxPlace;
    private Label labelDescription;
    private TextBox textBoxDescription;
    private Button buttonOk;
    private Button buttonCancel;

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

        labelDate = new Label()
        {
            Text = "Дата:",
            Location = new Point(12, 15),
            AutoSize = true
        };
        dateTimePickerDate = new DateTimePicker()
        {
            Location = new Point(160, 12),
            Size = new Size(280, 23),
            Format = DateTimePickerFormat.Long,
            Value = DateTime.Today
        };

        labelTime = new Label()
        {
            Text = "Час початку:",
            Location = new Point(12, 45),
            AutoSize = true
        };
        dateTimePickerTime = new DateTimePicker()
        {
            Location = new Point(160, 42),
            Size = new Size(120, 23),
            Format = DateTimePickerFormat.Time,
            ShowUpDown = true,
            Value = DateTime.Today.AddHours(12)
        };

        labelDuration = new Label()
        {
            Text = "Тривалість (хв):",
            Location = new Point(12, 75),
            AutoSize = true
        };
        numericDuration = new NumericUpDown()
        {
            Location = new Point(160, 72),
            Size = new Size(120, 23),
            Minimum = 1,
            Maximum = 1440,
            Value = 60
        };

        labelPlace = new Label()
        {
            Text = "Місце:",
            Location = new Point(12, 105),
            AutoSize = true
        };
        textBoxPlace = new TextBox()
        {
            Location = new Point(160, 102),
            Size = new Size(280, 23)
        };

        labelDescription = new Label()
        {
            Text = "Опис:",
            Location = new Point(12, 135),
            AutoSize = true
        };
        textBoxDescription = new TextBox()
        {
            Location = new Point(160, 132),
            Size = new Size(280, 110),
            Multiline = true,
            AcceptsReturn = true,
            ScrollBars = ScrollBars.Vertical
        };

        buttonOk = new Button()
        {
            Text = "Зберегти",
            Location = new Point(160, 260),
            Size = new Size(135, 32)
        };
        buttonOk.Click += buttonOk_Click;

        buttonCancel = new Button()
        {
            Text = "Скасувати",
            Location = new Point(305, 260),
            Size = new Size(135, 32)
        };
        buttonCancel.Click += buttonCancel_Click;

        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(460, 310);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        StartPosition = FormStartPosition.CenterParent;
        MinimizeBox = false;
        MaximizeBox = false;
        Text = "Подія";
        AcceptButton = buttonOk;
        CancelButton = buttonCancel;

        Controls.Add(labelDate);
        Controls.Add(dateTimePickerDate);
        Controls.Add(labelTime);
        Controls.Add(dateTimePickerTime);
        Controls.Add(labelDuration);
        Controls.Add(numericDuration);
        Controls.Add(labelPlace);
        Controls.Add(textBoxPlace);
        Controls.Add(labelDescription);
        Controls.Add(textBoxDescription);
        Controls.Add(buttonOk);
        Controls.Add(buttonCancel);
    }
}
