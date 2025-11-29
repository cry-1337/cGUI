namespace cGUI.Abstraction.Interfaces;

public interface IContainer<TElement> : IContainer where TElement : IElement
{
    new TElement Find(string id);

    new TElement Find(int index);

    void Add(TElement element);

    void Add(TElement element, int index);
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