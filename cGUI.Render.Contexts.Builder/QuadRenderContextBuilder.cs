using cGUI.Abstraction.Structs;
using cGUI.Render.Abstraction;

namespace cGUI.Render.Contexts.Builder;

public class QuadRenderContextBuilder : IRenderContextBuilder<IQuadRenderContext>
{
    private IQuadRenderContext m_QuadRenderContext = new QuadRenderContext();

    public IRenderContextBuilder<IQuadRenderContext> AddRect(in GUIRectangle rect)
    {
        return this;
    }
    public IQuadRenderContext Build() => m_QuadRenderContext;
}
