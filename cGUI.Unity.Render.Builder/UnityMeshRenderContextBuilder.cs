using cGUI.Render.Contexts.Builder;
using cGUI.Unity.Render.Abstraction;

namespace cGUI.Unity.Render.Builder;

public class UnityMeshRenderContextBuilder(IUnityMeshRenderContext ctx) : MeshRenderContextBuilder<IUnityMeshRenderContext>(ctx)
{
    public override IUnityMeshRenderContext Build() => RenderContext;
}