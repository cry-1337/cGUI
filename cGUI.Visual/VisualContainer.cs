using cGUI.Abstraction.Interfaces;
using cGUI.Abstraction.Structs;
using cGUI.Event.Abstraction;
using cGUI.Events.Models;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace cGUI.Visual;

public abstract class VisualContainer<TVisualElement>(string id) : VisualElement(id), IEventsHandler, IContainer<TVisualElement> where TVisualElement : VisualElement
{
    private readonly List<TVisualElement> m_Elements = [];

    public int Count => m_Elements.Count;

    public void Add(TVisualElement element)
    {
        m_Elements.Add(element);
        element.OnParentChanged(this);
    }

    public void Add(TVisualElement element, int index)
    {
        m_Elements.Insert(index, element);
        element.OnParentChanged(this);
    }

    void IContainer.Add(IElement element)
    {
        if (element is not TVisualElement visualElement) return;
        Add(visualElement);
    }

    void IContainer.Add(IElement element, int index)
    {
        if (element is not TVisualElement visualElement) return;
        Add(visualElement, index);
    }

    public bool Has(string id) => FindIndex(id) is not -1;

    public bool Has(int index) => index >= 0 && index < Count;

    public void Remove(string id) => Remove(FindIndex(id));

    public void Remove(int index)
    {
        Find(index).OnParentChanged(null);
        m_Elements.RemoveAt(index);
    }

    public int FindIndex(string id) => m_Elements.FindIndex(e => e.Id == id);

    public TVisualElement Find(string id) => Find(FindIndex(id));

    public TVisualElement Find(int index) => m_Elements[index];

    public sealed override bool HitTest(GUIPoint point, out HitTestResult result)
    {
        if (!base.HitTest(point, out result)) return false;

        var localPoint = point.ConvertToLocalPoint(Bounds);

        foreach (var element in m_Elements)
        {
            if (!element.HitTest(localPoint, out var localHit)) continue;
            result = localHit;
            break;
        }

        return true;
    }

    IElement IContainer.Find(string id) => Find(id);

    IElement IContainer.Find(int index) => Find(index);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void HandleEvents<TEvent>(in TEvent reason) where TEvent : IEvent
    {
        if (this is IEventHandler<TEvent> containerHandler) containerHandler.Handle(reason);

        foreach (var element in m_Elements)
        {
            if (element is not IEventHandler<TEvent> elementHandler) continue;
            if (element is IEventMicroController<TEvent> microController && microController.GetEvent(reason)) elementHandler.Handle(reason);
            else if (element is not IEventMicroController<TEvent>) elementHandler.Handle(reason);
        }
    }
}