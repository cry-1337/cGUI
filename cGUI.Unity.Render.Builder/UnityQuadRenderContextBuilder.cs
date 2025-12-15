using cGUI.Render.Contexts.Builder;
using cGUI.Unity.Render.Abstraction;

namespace cGUI.Unity.Render.Builder;

public class UnityQuadRenderContextBuilder(IUnityMeshRenderContext ctx) : QuadRenderContextBuilder<IUnityMeshRenderContext>(ctx)
{
    public override IUnityMeshRenderContext Build() => RenderContext;
}