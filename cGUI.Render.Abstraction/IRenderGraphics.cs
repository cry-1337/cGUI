using cGUI.Abstraction.Structs;
using System;

namespace cGUI.Render.Abstraction;

public interface IRenderGraphics : IDisposable
{
    void Process(in IRenderContext ctx);
    void SetViewProjection(in GUIRectangle rect);
    void ExecuteBuffer();
}

public interface IRenderGraphics<TContext> : IRenderGraphics where TContext : IRenderContext
{
    void Process(in TContext ctx);
}