using cGUI.Abstraction.Structs;
using cGUI.Render.Abstraction;
using System.Collections.Generic;

namespace cGUI.Render.Contexts;

public struct QuadRenderContext() : IQuadRenderContext
{
    private readonly List<int> m_Indicies = new(16);

    public List<Vertex> Verticies = new(12);

    public readonly int VerticiesCount => Verticies.Count;

    public readonly int IndiciesCount => m_Indicies.Count;

    public GUIRectangle CornerRoundRadius { get; set; } = new();

    public readonly void AddIndex(int index) => m_Indicies.Add(index);

    public readonly void AddVertex(Vertex vertex) => Verticies.Add(vertex);

    public readonly void Clear()
    {
        m_Indicies?.Clear();
        Verticies.Clear();
    }

    public readonly void CopyIndicies(int[] array) => m_Indicies.CopyTo(array);

    public readonly void CopyVerticices(Vertex[] array) => Verticies.CopyTo(array);
}