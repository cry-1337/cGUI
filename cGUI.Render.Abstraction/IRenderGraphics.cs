using cGUI.Abstraction.Structs;
using System;

namespace cGUI.Render.Abstraction;

public interface IRenderGraphics<in TContext> : IDisposable where TContext : IRenderContext
{
    void SetViewProjection(in GUIRectangle rect);
    void ExecuteBuffer();
    void Process(TContext ctx);
}