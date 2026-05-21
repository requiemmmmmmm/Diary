namespace Diary;

/// <summary>
/// Працює зі сховищем подій: шукає, перевіряє накладки,
/// видаляє або переносить старі, шукає найближчу подію.
/// </summary>
public class EventManager
{
    private readonly EventStorage storage;

    /// <summary>
    /// Створює керівника подій над уже існуючим сховищем.
    /// </summary>
    public EventManager(EventStorage storage)
    {
        this.storage = storage;
    }

    /// <summary>
    /// Шукає події, у яких в місці або описі трапляється введений текст.
    /// Великі і малі літери не мають значення.
    /// </summary>
    public List<Event> SearchByText(string text)
    {
        var result = new List<Event>();
        if (string.IsNullOrWhiteSpace(text))
        {
            return result;
        }

        string lower = text.ToLower();
        foreach (var e in storage.All)
        {
            if (e.Place.ToLower().Contains(lower) || e.Description.ToLower().Contains(lower))
            {
                result.Add(e);
            }
        }
        return result;
    }

    /// <summary>
    /// Повертає всі події на конкретну дату (час дня не важливий).
    /// </summary>
    public List<Event> OnDate(DateTime date)
    {
        var result = new List<Event>();
        foreach (var e in storage.All)
        {
            if (e.Start.Date == date.Date)
            {
                result.Add(e);
            }
        }
        return result;
    }

    /// <summary>
    /// Знаходить найближчу подію, що ще не почалася.
    /// Якщо таких подій немає — повертає null.
    /// </summary>
    public Event? GetNearest()
    {
        Event? nearest = null;
        DateTime now = DateTime.Now;
        foreach (var e in storage.All)
        {
            if (e.Start >= now)
            {
                if (nearest == null || e.Start < nearest.Start)
                {
                    nearest = e;
                }
            }
        }
        return nearest;
    }

    /// <summary>
    /// Шукає пари подій, які накладаються в часі.
    /// </summary>
    public List<(Event First, Event Second)> FindOverlaps()
    {
        var result = new List<(Event, Event)>();
        var all = storage.All;
        for (int i = 0; i < all.Count; i++)
        {
            for (int j = i + 1; j < all.Count; j++)
            {
                if (all[i].Start < all[j].End && all[j].Start < all[i].End)
                {
                    result.Add((all[i], all[j]));
                }
            }
        }
        return result;
    }

    /// <summary>
    /// Видаляє всі події, що відбулися раніше за сьогодні.
    /// Повертає кількість видалених.
    /// </summary>
    public int RemoveOld()
    {
        DateTime today = DateTime.Today;
        int count = 0;
        for (int i = storage.All.Count - 1; i >= 0; i--)
        {
            if (storage.All[i].Start.Date < today)
            {
                storage.Remove(storage.All[i]);
                count++;
            }
        }
        return count;
    }

    /// <summary>
    /// Переносить всі вчорашні і давніші події на нову дату.
    /// Час дня в кожній події лишається той самий.
    /// Повертає кількість перенесених.
    /// </summary>
    public int MoveOldTo(DateTime newDate)
    {
        DateTime today = DateTime.Today;
        int count = 0;
        foreach (var e in storage.All)
        {
            if (e.Start.Date < today)
            {
                e.Start = newDate.Date + e.Start.TimeOfDay;
                count++;
            }
        }
        return count;
    }
}
