using System;
using System.Collections.Generic;
using cGUI.Abstraction.Structs;

namespace cGUI.Abstraction;

public class FontAtlas
{
    private readonly FontCharacter[] m_AsciiLookup = new FontCharacter[256];
    private readonly Dictionary<char, FontCharacter> m_ExtendedLookup = new();

    public float LineHeight { get; set; } = 16f;
    public float FontSize { get; set; } = 14f;

    public void RegisterCharacter(in FontCharacter character)
    {
        if (character.Character < 256)
        {
            m_AsciiLookup[character.Character] = character;
        }
        else
        {
            m_ExtendedLookup[character.Character] = character;
        }
    }

    public bool TryGetCharacter(char ch, out FontCharacter character)
    {
        if (ch < 256)
        {
            character = m_AsciiLookup[ch];
            if (character.Character != '\0') return true;
        }
        
        return m_ExtendedLookup.TryGetValue(ch, out character);
    }

    /// <summary>
    /// Generates a standard 16x16 grid font atlas (e.g. 256 characters) for quick setup.
    /// </summary>
    public static FontAtlas CreateGridAtlas(float charWidth = 8f, float charHeight = 16f, int columns = 16, int rows = 16)
    {
        var atlas = new FontAtlas
        {
            FontSize = charHeight,
            LineHeight = charHeight
        };

        float cellU = 1f / columns;
        float cellV = 1f / rows;

        for (int i = 0; i < columns * rows; i++)
        {
            char ch = (char)i;
            int col = i % columns;
            int row = i / columns;

            // Texture UV (V inverted for standard GPU top-left vs bottom-left textures)
            float u = col * cellU;
            float v = 1f - ((row + 1) * cellV);

            var fontChar = new FontCharacter(
                ch,
                new GUIRectangle(u, v, cellU, cellV),
                charWidth,
                charHeight,
                charWidth,
                0f,
                0f
            );
            atlas.RegisterCharacter(fontChar);
        }

        return atlas;
    }
}
