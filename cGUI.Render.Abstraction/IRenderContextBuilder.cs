using cGUI.Abstraction.Structs;

namespace cGUI.Render.Abstraction;

public interface IRenderContextBuilder<TValue> where TValue : IRenderContext
{
    IRenderContextBuilder<TValue> AddRect(in GUIRectangle rect, in GUIColor color);
    IRenderContextBuilder<TValue> AddRect(in GUIRectangle rect, in GUIColor color, in GUIRectangle radiusRect);
    IRenderContextBuilder<TValue> AddRect(in GUIRectangle rect,
        in GUIColor colTopLeft, in GUIColor colTopRight,
        in GUIColor colBotLeft, in GUIColor colBotRight);
    IRenderContextBuilder<TValue> AddRect(in GUIRectangle rect,
        in GUIColor colTopLeft, in GUIColor colTopRight,
        in GUIColor colBotLeft, in GUIColor colBotRight,
        in GUIRectangle radiusRect);

    IRenderContextBuilder<TValue> AddLine(GUIVector2 a, GUIVector2 b,
        GUIColor color,
        float thickness,
        float aaSize);

    TValue Build();
}