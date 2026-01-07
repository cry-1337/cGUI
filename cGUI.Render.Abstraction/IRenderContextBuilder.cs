using cGUI.Abstraction.Structs;

namespace cGUI.Render.Abstraction;

public interface IRenderContextBuilder<TContextValue, TMeshValue> 
    where TContextValue : IRenderContext 
    where TMeshValue : IMeshData
{
    IRenderContextBuilder<TContextValue, TMeshValue> AddRect(in GUIRectangle rect, in GUIColor color);
    IRenderContextBuilder<TContextValue, TMeshValue> AddRect(in GUIRectangle rect,
        in GUIColor colTopLeft, in GUIColor colTopRight,
        in GUIColor colBotLeft, in GUIColor colBotRight);
    IRenderContextBuilder<TContextValue, TMeshValue> AddRect(in GUIRectangle rect, in GUIColor color, TMeshValue meshData);
    IRenderContextBuilder<TContextValue, TMeshValue> AddRect(in GUIRectangle rect,
        in GUIColor colTopLeft, in GUIColor colTopRight,
        in GUIColor colBotLeft, in GUIColor colBotRight,
        TMeshValue meshData);

    IRenderContextBuilder<TContextValue, TMeshValue> AddLine(GUIVector2 a, GUIVector2 b,
        GUIColor color,
        TMeshValue meshData,
        float thickness,
        float aaSize);

    TContextValue Build();
}