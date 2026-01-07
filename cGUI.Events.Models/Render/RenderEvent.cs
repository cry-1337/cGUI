using cGUI.Event.Abstraction;
using cGUI.Render.Abstraction;
using cGUI.Unity.Render.Abstraction;

namespace cGUI.Events.Models.Render;

public readonly struct RenderEvent(IRender<IMeshRenderContext<UnityMeshData>> render) : IEvent
{
    public readonly IRender<IMeshRenderContext<UnityMeshData>> Render = render;
}