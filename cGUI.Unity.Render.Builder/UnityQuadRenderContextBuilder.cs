using cGUI.Render.Contexts.Builder;
using cGUI.Unity.Render.Abstraction;

namespace cGUI.Unity.Render.Builder;

public class UnityQuadRenderContextBuilder(IUnityQuadRenderContext ctx) : QuadRenderContextBuilder<IUnityQuadRenderContext>(ctx)
{
    public override IUnityQuadRenderContext Build() => RenderContext;
}