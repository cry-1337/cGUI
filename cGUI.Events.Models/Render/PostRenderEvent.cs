using cGUI.Event.Abstraction;
using cGUI.Render.Abstraction;
using cGUI.Unity.Render.Abstraction;

namespace cGUI.Events.Models.Render;

public readonly struct PostRenderEvent(IRender<IMeshRenderContext<UnityMeshData>> render, float deltaTime) : IEvent
{
    public readonly IRender<IMeshRenderContext<UnityMeshData>> Render = render;
    public readonly float DeltaTime = deltaTime;
}