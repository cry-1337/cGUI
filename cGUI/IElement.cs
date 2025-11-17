namespace cGUI;

public readonly struct Rectangle
{
    public readonly float m_X, m_Y, m_Width, m_Height;

    public Point Point => new();

    public readonly bool Contains(Point point) => point.m_X >= m_X && point.m_Y >= m_Y && point.m_X <= m_Width && point.m_Y <= m_Height;
}

public readonly struct Vector2
{
    public readonly float m_X, m_Y;
}

public readonly struct Point(float x, float y)
{
    public readonly float m_X = x, m_Y = y;

    public Point ConvertToLocalPoint(Rectangle bounds) => new(m_X - bounds.m_X, m_Y - bounds.m_Y);
}

public readonly struct HitTestResult(IElement element, Point point)
{
    public readonly IElement m_Element = element;
    public readonly Point m_Point = point;
}

public interface IElement
{
    string Id { get; }

    bool IsActive { get; }

    bool IsHittable { get; }

    Rectangle Bounds { get; }

    IContainer Parent { get; }

    bool HitTest(Point point, out HitTestResult result);
}

public interface IContainer : IElement
{
    int Count { get; }

    IElement Find(string id);

    IElement Find(int index);

    int FindIndex(string id);

    bool Has(string id);

    bool Has(int index);

    void Add(IElement element);

    void Add(IElement element, int index);

    void Remove(string id);

    void Remove(int index);
}

public interface IContainer<TElement> : IContainer where TElement : IElement
{
    new TElement Find(string id);

    new TElement Find(int index);

    void Add(TElement element);

    void Add(TElement element, int index);
}

public interface IEvent;

public interface IEventHandler<in TEvent> where TEvent : IEvent
{
    void Handle(TEvent e);
}

public readonly struct RenderEvent : IEvent;

public interface IVisualElement : IElement, IEventHandler<RenderEvent>
{
    bool IsMaskable { get; }
}

public interface IEventDispatcher
{
    void Dispatch<TEvent>(IElement root, TEvent e);
}