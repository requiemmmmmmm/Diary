using System.Text.Json;

namespace Diary;

/// <summary>
/// Сховище всіх подій щоденника.
/// Тримає список і вміє зберігати або завантажувати його з файлу.
/// </summary>
public class EventStorage
{
    private const string FilePath = "data.json";

    private List<Event> events = new List<Event>();

    /// <summary>
    /// Усі події, які зараз є в щоденнику.
    /// </summary>
    public List<Event> All => events;

    /// <summary>
    /// Додати нову подію.
    /// </summary>
    public void Add(Event item)
    {
        events.Add(item);
    }

    /// <summary>
    /// Видалити подію.
    /// </summary>
    public void Remove(Event item)
    {
        events.Remove(item);
    }

    /// <summary>
    /// Зберегти всі події у файл на диску.
    /// </summary>
    public void Save()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(events, options);
        File.WriteAllText(FilePath, json);
    }

    /// <summary>
    /// Завантажити події з файлу. Якщо файлу немає — нічого не робимо.
    /// </summary>
    public void Load()
    {
        if (!File.Exists(FilePath))
        {
            return;
        }

        string json = File.ReadAllText(FilePath);
        var loaded = JsonSerializer.Deserialize<List<Event>>(json);
        if (loaded != null)
        {
            events = loaded;
        }
    }
}
