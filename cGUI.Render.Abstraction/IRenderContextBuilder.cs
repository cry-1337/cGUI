namespace cGUI.Render.Abstraction;

public interface IRenderContextBuilder<TValue> where TValue : IRenderContext
{
    TValue Build();
}