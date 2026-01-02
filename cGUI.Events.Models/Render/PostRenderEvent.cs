using cGUI.Event.Abstraction;
using cGUI.Render.Abstraction;
using cGUI.Unity.Render.Abstraction;

namespace cGUI.Events.Models.Render;

public readonly struct PostRenderEvent(IRender<IMeshRenderContext<IUnityMeshData>> render, float deltaTime) : IEvent
{
    public readonly IRender<IMeshRenderContext<IUnityMeshData>> Render = render;
    public readonly float DeltaTime = deltaTime;
}