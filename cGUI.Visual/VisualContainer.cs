using cGUI.Abstraction.Interfaces;
using cGUI.Abstraction.Structs;
using cGUI.Event.Abstraction;
using cGUI.Events.Models;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

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

    public override void OnRender(RenderEvent e) => RenderElements(e);

    protected virtual void RenderElements(RenderEvent e) => m_Elements.ForEach(element => RenderElement(e, element));

    protected virtual void RenderElement<TElement>(RenderEvent e, TElement element) where TElement : VisualElement, IEventHandler<RenderEvent> => element.Handle(e);

    public override void OnLayout(LayoutEvent e) => LayoutElements(e);

    protected virtual void LayoutElements(LayoutEvent e) => m_Elements.ForEach(element => LayoutElement(e, element));

    protected virtual void LayoutElement<TElement>(LayoutEvent e, TElement element) where TElement : VisualElement, IEventHandler<LayoutEvent> => element.Handle(e);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void HandleEvents<TEvent>(in TEvent reason) where TEvent : IEvent
    {
        foreach (var element in m_Elements)
        {
            if (element is not IEventHandler<TEvent> handler) continue;
            if (element is IEventMicroController<TEvent> microController && microController.GetEvent(reason)) handler.Handle(reason);
            else if (element is not IEventMicroController<TEvent>) handler.Handle(reason);
        }
    }
}