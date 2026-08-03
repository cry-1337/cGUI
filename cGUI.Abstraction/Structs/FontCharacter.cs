namespace cGUI.Abstraction.Structs;

public readonly struct FontCharacter
{
    public char Character { get; }
    public GUIRectangle UVRect { get; }
    public float Width { get; }
    public float Height { get; }
    public float AdvanceX { get; }
    public float OffsetX { get; }
    public float OffsetY { get; }

    public FontCharacter(char character, GUIRectangle uvRect, float width, float height, float advanceX, float offsetX = 0f, float offsetY = 0f)
    {
        Character = character;
        UVRect = uvRect;
        Width = width;
        Height = height;
        AdvanceX = advanceX;
        OffsetX = offsetX;
        OffsetY = offsetY;
    }
}
