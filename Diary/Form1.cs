namespace Diary;

/// <summary>
/// Головне вікно щоденника. Показує список подій
/// та дозволяє керувати ними.
/// </summary>
public partial class Form1 : Form
{
    private readonly EventStorage storage = new EventStorage();
    private readonly EventManager manager;

    /// <summary>
    /// Створює головне вікно.
    /// </summary>
    public Form1()
    {
        InitializeComponent();
        manager = new EventManager(storage);
    }

    private void Form1_Load(object sender, EventArgs e)
    {
        try
        {
            storage.Load();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Не вдалося прочитати файл зі справами:\r\n" + ex.Message,
                "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        RefreshList(storage.All);
        UpdateNearest();
        ShowStartupReminder();
    }

    private void Form1_FormClosing(object sender, FormClosingEventArgs e)
    {
        SaveSafely();
    }

    private void timerRefresh_Tick(object sender, EventArgs e)
    {
        UpdateNearest();
    }

    private void RefreshList(IEnumerable<Event> events)
    {
        listEvents.BeginUpdate();
        listEvents.Items.Clear();
        foreach (var ev in events)
        {
            var item = new ListViewItem(ev.Start.ToString("dd.MM.yyyy"));
            item.SubItems.Add(ev.Start.ToString("HH:mm"));
            item.SubItems.Add(((int)ev.Duration.TotalMinutes).ToString() + " хв");
            item.SubItems.Add(ev.Place);
            item.SubItems.Add(ev.Description);
            item.Tag = ev;
            listEvents.Items.Add(item);
        }
        listEvents.EndUpdate();
    }

    private void UpdateNearest()
    {
        var n = manager.GetNearest();
        if (n == null)
        {
            labelNearest.Text = "Найближча подія: немає запланованих";
            return;
        }
        labelNearest.Text = string.Format("Найближча подія: {0} о {1} — {2}",
            n.Start.ToString("dd.MM.yyyy"),
            n.Start.ToString("HH:mm"),
            n.Description);
    }

    private void ShowStartupReminder()
    {
        var n = manager.GetNearest();
        if (n == null)
        {
            return;
        }

        TimeSpan diff = n.Start - DateTime.Now;
        if (diff.TotalSeconds <= 0 || diff.TotalHours > 12)
        {
            return;
        }

        string when;
        if (diff.TotalMinutes < 60)
        {
            when = "через " + (int)diff.TotalMinutes + " хв";
        }
        else
        {
            when = "через " + (int)diff.TotalHours + " год " + diff.Minutes + " хв";
        }

        MessageBox.Show(
            "Скоро подія " + when + ":\r\n" +
            n.Description + "\r\n" +
            "о " + n.Start.ToString("HH:mm") + " (" + n.Place + ")",
            "Нагадування",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);
    }

    private Event? GetSelectedEvent()
    {
        if (listEvents.SelectedItems.Count == 0)
        {
            return null;
        }
        return listEvents.SelectedItems[0].Tag as Event;
    }

    private void buttonAdd_Click(object sender, EventArgs e)
    {
        using (var f = new EditForm())
        {
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                storage.Add(f.ResultEvent);
                SaveSafely();
                RefreshList(storage.All);
                UpdateNearest();
            }
        }
    }

    private void buttonEdit_Click(object sender, EventArgs e)
    {
        var ev = GetSelectedEvent();
        if (ev == null)
        {
            MessageBox.Show("Спочатку оберіть подію зі списку.", "Підказка",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        using (var f = new EditForm(ev))
        {
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                SaveSafely();
                RefreshList(storage.All);
                UpdateNearest();
            }
        }
    }

    private void buttonDelete_Click(object sender, EventArgs e)
    {
        var ev = GetSelectedEvent();
        if (ev == null)
        {
            MessageBox.Show("Спочатку оберіть подію зі списку.", "Підказка",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var answer = MessageBox.Show(
            "Видалити подію \"" + ev.Description + "\"?",
            "Підтвердження",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

        if (answer == DialogResult.Yes)
        {
            storage.Remove(ev);
            SaveSafely();
            RefreshList(storage.All);
            UpdateNearest();
        }
    }

    private void buttonSearch_Click(object sender, EventArgs e)
    {
        string text = textBoxSearch.Text;
        if (string.IsNullOrWhiteSpace(text))
        {
            RefreshList(storage.All);
            return;
        }

        var found = manager.SearchByText(text);
        if (found.Count == 0)
        {
            MessageBox.Show("Подій з таким текстом не знайдено.", "Пошук",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        RefreshList(found);
    }

    private void buttonShowAll_Click(object sender, EventArgs e)
    {
        textBoxSearch.Text = "";
        RefreshList(storage.All);
    }

    private void buttonOnDate_Click(object sender, EventArgs e)
    {
        var list = manager.OnDate(dateFilter.Value);
        RefreshList(list);
    }

    private void buttonOverlaps_Click(object sender, EventArgs e)
    {
        var overlaps = manager.FindOverlaps();
        if (overlaps.Count == 0)
        {
            MessageBox.Show("Накладок між справами не виявлено.", "Накладки",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var lines = new List<string>();
        foreach (var pair in overlaps)
        {
            lines.Add("• " + pair.First.Description +
                " (" + pair.First.Start.ToString("dd.MM HH:mm") +
                "-" + pair.First.End.ToString("HH:mm") + ")" +
                "  ↔  " + pair.Second.Description +
                " (" + pair.Second.Start.ToString("dd.MM HH:mm") +
                "-" + pair.Second.End.ToString("HH:mm") + ")");
        }

        MessageBox.Show(string.Join("\r\n", lines), "Знайдені накладки",
            MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }

    private void buttonRemoveOld_Click(object sender, EventArgs e)
    {
        var ans = MessageBox.Show(
            "Видалити всі справи, що відбулися до сьогодні?",
            "Підтвердження",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);
        if (ans != DialogResult.Yes)
        {
            return;
        }

        int count = manager.RemoveOld();
        SaveSafely();
        RefreshList(storage.All);
        UpdateNearest();

        MessageBox.Show("Видалено справ: " + count, "Готово",
            MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void buttonMoveOld_Click(object sender, EventArgs e)
    {
        DateTime target = dateFilter.Value.Date;
        if (target < DateTime.Today)
        {
            MessageBox.Show("Не можна перенести справи на минулу дату. " +
                "Оберіть сьогодні або пізніше у полі \"На дату\".",
                "Підказка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var ans = MessageBox.Show(
            "Усі справи до сьогодні буде перенесено на " +
            target.ToString("dd.MM.yyyy") + ". Продовжити?",
            "Підтвердження",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);
        if (ans != DialogResult.Yes)
        {
            return;
        }

        int count = manager.MoveOldTo(target);
        SaveSafely();
        RefreshList(storage.All);
        UpdateNearest();

        MessageBox.Show("Перенесено справ: " + count, "Готово",
            MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void menuHelp_Click(object sender, EventArgs e)
    {
        using (var f = new HelpForm())
        {
            f.ShowDialog(this);
        }
    }

    private void SaveSafely()
    {
        try
        {
            storage.Save();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Не вдалося зберегти зміни у файл:\r\n" + ex.Message,
                "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
