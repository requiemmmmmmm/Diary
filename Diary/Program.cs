namespace Diary;

static class Program
{
    // головний метод
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        // якщо станеться непередбачена помилка — покажемо повідомлення,
        // а не дамо програмі впасти
        Application.ThreadException += (sender, e) =>
        {
            MessageBox.Show(
                "Сталася помилка:\r\n" + e.Exception.Message,
                "Помилка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        };
        AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
        {
            MessageBox.Show(
                "Сталася критична помилка. Програму буде закрито.",
                "Помилка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        };

        Application.Run(new Form1());
    }
}
