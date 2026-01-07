using cGUI.Render.Abstraction;
using System.Collections.Generic;

namespace cGUI.Render.Contexts;

public readonly struct MeshRenderContext() : IMeshRenderContext
{
    private readonly List<Vertex> m_Vertices = new(12);
    private readonly List<int> m_Indices = new(16);

    public readonly List<Vertex> Vertices => m_Vertices;
    public readonly List<int> Indicies => m_Indices;

    public readonly int VerticiesCount => m_Vertices.Count;
    public readonly int IndiciesCount => m_Indices.Count;

    public readonly void AddIndex(int index) => m_Indices.Add(index);
    public readonly void AddVertex(Vertex vertex) => m_Vertices.Add(vertex);
    public readonly void Clear()
    {
        m_Indices.Clear();
        m_Vertices.Clear();
    }
}