using cGUI.Abstraction.Structs;
using System;

namespace cGUI.Render.Abstraction;

public interface IRenderGraphics : IDisposable
{
    void Process(IRenderContext ctx);
    void SetViewProjection(in GUIRectangle rect);
    void ExecuteBuffer();
}

public interface IRenderGraphics<in TContext> : IRenderGraphics where TContext : IRenderContext
{
    void Process(TContext ctx);
}