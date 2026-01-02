using cGUI.Event.Abstraction;
using cGUI.Render.Abstraction;
using cGUI.Unity.Render.Abstraction;

namespace cGUI.Events.Models.Render;

public readonly struct RenderEvent(IRender<IMeshRenderContext<IUnityMeshData>> render) : IEvent
{
    public readonly IRender<IMeshRenderContext<IUnityMeshData>> Render = render;
}