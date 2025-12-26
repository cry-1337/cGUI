using cGUI.Render.Abstraction;
using System.Collections.Generic;

namespace cGUI.Unity.Render.Contexts;

public readonly struct UnityMeshRenderContext() : IMeshRenderContext<UnityMeshData>
{
    private readonly List<UnityMeshData> m_Meshes = new(2);
    private readonly List<Vertex> m_Vertices = new(12);
    private readonly List<int> m_Indices = new(16);

    public List<UnityMeshData> Meshes => m_Meshes;

    public readonly int MeshCount => m_Meshes.Count;

    public readonly int VerticiesCount => m_Vertices.Count;

    public readonly int IndiciesCount => m_Indices.Count;

    public List<Vertex> Vertices => m_Vertices;

    public List<int> Indicies => m_Indices;

    public void Clear()
    {
        m_Meshes.Clear();
        m_Vertices.Clear();
        m_Indices.Clear();
    }
}