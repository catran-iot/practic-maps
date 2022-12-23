namespace PracticMaps
{
    /// <summary>
    /// Перечисление возможных действий над картографическими объектами
    /// </summary>
    public enum EPMapAction
    {
        None,
        MoveObject
    }

    /// <summary>
    /// Тип объекта карты
    /// </summary>
    public enum EPMapObjectType
    {
        None,
        Point,
        Track,
        Session,
        Place
    }
}