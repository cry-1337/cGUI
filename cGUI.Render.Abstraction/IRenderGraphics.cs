namespace cGUI.Render.Abstraction;

public interface IRenderGraphics
{
    void Process(IRenderContext ctx);
}

public interface IRenderGraphics<in TContext> : IRenderGraphics where TContext : class, IRenderContext
{
    void Process(TContext ctx);
}