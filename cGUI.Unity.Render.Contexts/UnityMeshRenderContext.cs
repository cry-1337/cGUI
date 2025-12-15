using cGUI.Render.Abstraction;
using cGUI.Unity.Render.Abstraction;
using System.Collections.Generic;

namespace cGUI.Unity.Render.Contexts;

public struct UnityMeshRenderContext() : IUnityMeshRenderContext
{
    private List<IUnityMeshData> m_Meshes = new(2);
    private List<Vertex> m_Vertices = new(12);
    private List<int> m_Indices = new(16);

    public List<IUnityMeshData> Meshes
    {
        get => m_Meshes;
        set => m_Meshes = value;
    }

    public readonly int MeshCount => m_Meshes.Count;

    public int VerticiesCount => m_Vertices.Count;

    public int IndiciesCount => m_Indices.Count;

    public List<Vertex> Vertices => m_Vertices;

    public List<int> Indicies => m_Indices;

    public void AddIndex(int index) => m_Indices.Add(index);

    public void AddVertex(Vertex vertex) => m_Vertices.Add(vertex);

    public void Clear()
    {
        m_Meshes.Clear();
        m_Vertices.Clear();
        m_Indices.Clear();
    }
}