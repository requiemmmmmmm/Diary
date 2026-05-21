namespace Diary;

/// <summary>
/// Запис про заплановану подію в щоденнику.
/// </summary>
public class Event
{
    /// <summary>
    /// Дата і час, коли подія починається.
    /// </summary>
    public DateTime Start { get; set; }

    /// <summary>
    /// Скільки часу триватиме подія.
    /// </summary>
    public TimeSpan Duration { get; set; }

    /// <summary>
    /// Місце, де відбудеться подія.
    /// </summary>
    public string Place { get; set; } = "";

    /// <summary>
    /// Короткий опис події.
    /// </summary>
    public string Description { get; set; } = "";

    /// <summary>
    /// Дата і час, коли подія закінчиться.
    /// </summary>
    public DateTime End => Start + Duration;
}
