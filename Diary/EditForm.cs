namespace Diary;

/// <summary>
/// Вікно для додавання нової події або редагування вже існуючої.
/// </summary>
public partial class EditForm : Form
{
    /// <summary>
    /// Подія, яку користувач створив або відредагував.
    /// </summary>
    public Event ResultEvent { get; private set; } = new Event();

    /// <summary>
    /// Створює вікно для нової події.
    /// </summary>
    public EditForm()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Створює вікно для редагування вже існуючої події.
    /// </summary>
    public EditForm(Event existing) : this()
    {
        ResultEvent = existing;
        dateTimePickerDate.Value = existing.Start.Date;
        dateTimePickerTime.Value = DateTime.Today + existing.Start.TimeOfDay;
        numericDuration.Value = (decimal)existing.Duration.TotalMinutes;
        textBoxPlace.Text = existing.Place;
        textBoxDescription.Text = existing.Description;
    }

    private void buttonOk_Click(object sender, EventArgs e)
    {
        if (numericDuration.Value <= 0)
        {
            MessageBox.Show("Тривалість має бути більшою за нуль.", "Перевірте поле",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        if (string.IsNullOrWhiteSpace(textBoxPlace.Text))
        {
            MessageBox.Show("Будь ласка, введіть місце події.", "Перевірте поле",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            textBoxPlace.Focus();
            return;
        }
        if (string.IsNullOrWhiteSpace(textBoxDescription.Text))
        {
            MessageBox.Show("Будь ласка, введіть опис події.", "Перевірте поле",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            textBoxDescription.Focus();
            return;
        }

        DateTime date = dateTimePickerDate.Value.Date;
        TimeSpan time = dateTimePickerTime.Value.TimeOfDay;
        ResultEvent.Start = date + time;
        ResultEvent.Duration = TimeSpan.FromMinutes((double)numericDuration.Value);
        ResultEvent.Place = textBoxPlace.Text.Trim();
        ResultEvent.Description = textBoxDescription.Text.Trim();

        DialogResult = DialogResult.OK;
        Close();
    }

    private void buttonCancel_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }
}
