namespace Diary;

partial class Form1
{
    private System.ComponentModel.IContainer components = null;

    private MenuStrip menuStrip;
    private ToolStripMenuItem menuHelp;

    private Label labelNearest;

    private Label labelSearch;
    private TextBox textBoxSearch;
    private Button buttonSearch;
    private Button buttonShowAll;

    private Label labelDate;
    private DateTimePicker dateFilter;
    private Button buttonOnDate;

    private ListView listEvents;
    private ColumnHeader columnDate;
    private ColumnHeader columnTime;
    private ColumnHeader columnDuration;
    private ColumnHeader columnPlace;
    private ColumnHeader columnDescription;

    private Button buttonAdd;
    private Button buttonEdit;
    private Button buttonDelete;
    private Button buttonOverlaps;
    private Button buttonRemoveOld;
    private Button buttonMoveOld;

    private System.Windows.Forms.Timer timerRefresh;

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

        // Меню
        menuStrip = new MenuStrip();
        menuHelp = new ToolStripMenuItem()
        {
            Text = "Допомога",
            ShortcutKeys = Keys.F1
        };
        menuHelp.Click += menuHelp_Click;
        menuStrip.Items.Add(menuHelp);
        menuStrip.Location = new Point(0, 0);
        menuStrip.Size = new Size(830, 24);

        // Найближча подія
        labelNearest = new Label()
        {
            Text = "Найближча подія: —",
            Location = new Point(12, 35),
            Size = new Size(810, 22),
            Font = new Font("Segoe UI", 10F, FontStyle.Bold)
        };

        // Пошук
        labelSearch = new Label()
        {
            Text = "Пошук:",
            Location = new Point(12, 70),
            AutoSize = true
        };
        textBoxSearch = new TextBox()
        {
            Location = new Point(80, 67),
            Size = new Size(300, 23)
        };
        buttonSearch = new Button()
        {
            Text = "Знайти",
            Location = new Point(390, 65),
            Size = new Size(100, 27)
        };
        buttonSearch.Click += buttonSearch_Click;

        buttonShowAll = new Button()
        {
            Text = "Показати всі",
            Location = new Point(500, 65),
            Size = new Size(120, 27)
        };
        buttonShowAll.Click += buttonShowAll_Click;

        // Фільтр по даті
        labelDate = new Label()
        {
            Text = "На дату:",
            Location = new Point(12, 105),
            AutoSize = true
        };
        dateFilter = new DateTimePicker()
        {
            Location = new Point(80, 102),
            Size = new Size(200, 23),
            Format = DateTimePickerFormat.Long,
            Value = DateTime.Today
        };
        buttonOnDate = new Button()
        {
            Text = "Показати на цю дату",
            Location = new Point(290, 100),
            Size = new Size(200, 27)
        };
        buttonOnDate.Click += buttonOnDate_Click;

        // Колонки списку
        columnDate = new ColumnHeader() { Text = "Дата", Width = 100 };
        columnTime = new ColumnHeader() { Text = "Час", Width = 70 };
        columnDuration = new ColumnHeader() { Text = "Тривалість", Width = 90 };
        columnPlace = new ColumnHeader() { Text = "Місце", Width = 150 };
        columnDescription = new ColumnHeader() { Text = "Опис", Width = 380 };

        listEvents = new ListView()
        {
            Location = new Point(12, 140),
            Size = new Size(810, 320),
            View = View.Details,
            FullRowSelect = true,
            GridLines = true,
            MultiSelect = false,
            HideSelection = false
        };
        listEvents.Columns.AddRange(new ColumnHeader[]
        {
            columnDate, columnTime, columnDuration, columnPlace, columnDescription
        });

        // Кнопки внизу
        buttonAdd = new Button()
        {
            Text = "Додати",
            Location = new Point(12, 475),
            Size = new Size(120, 32)
        };
        buttonAdd.Click += buttonAdd_Click;

        buttonEdit = new Button()
        {
            Text = "Редагувати",
            Location = new Point(140, 475),
            Size = new Size(120, 32)
        };
        buttonEdit.Click += buttonEdit_Click;

        buttonDelete = new Button()
        {
            Text = "Видалити",
            Location = new Point(268, 475),
            Size = new Size(120, 32)
        };
        buttonDelete.Click += buttonDelete_Click;

        buttonOverlaps = new Button()
        {
            Text = "Знайти накладки",
            Location = new Point(410, 475),
            Size = new Size(150, 32)
        };
        buttonOverlaps.Click += buttonOverlaps_Click;

        buttonRemoveOld = new Button()
        {
            Text = "Видалити старі",
            Location = new Point(570, 475),
            Size = new Size(130, 32)
        };
        buttonRemoveOld.Click += buttonRemoveOld_Click;

        buttonMoveOld = new Button()
        {
            Text = "Перенести старі",
            Location = new Point(705, 475),
            Size = new Size(130, 32)
        };
        buttonMoveOld.Click += buttonMoveOld_Click;

        // Таймер оновлення найближчої події
        timerRefresh = new System.Windows.Forms.Timer(components)
        {
            Interval = 60000,
            Enabled = true
        };
        timerRefresh.Tick += timerRefresh_Tick;

        // Форма
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(840, 520);
        Text = "Щоденник";
        MainMenuStrip = menuStrip;
        StartPosition = FormStartPosition.CenterScreen;

        Controls.Add(labelNearest);
        Controls.Add(labelSearch);
        Controls.Add(textBoxSearch);
        Controls.Add(buttonSearch);
        Controls.Add(buttonShowAll);
        Controls.Add(labelDate);
        Controls.Add(dateFilter);
        Controls.Add(buttonOnDate);
        Controls.Add(listEvents);
        Controls.Add(buttonAdd);
        Controls.Add(buttonEdit);
        Controls.Add(buttonDelete);
        Controls.Add(buttonOverlaps);
        Controls.Add(buttonRemoveOld);
        Controls.Add(buttonMoveOld);
        Controls.Add(menuStrip);

        Load += Form1_Load;
        FormClosing += Form1_FormClosing;
    }
}
