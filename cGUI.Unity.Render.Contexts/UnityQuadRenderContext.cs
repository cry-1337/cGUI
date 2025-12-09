using cGUI.Abstraction.Structs;
using cGUI.Render.Abstraction;
using cGUI.Unity.Render.Abstraction;
using System.Collections.Generic;
using UnityEngine;

namespace cGUI.Unity.Render.Contexts;

public struct UnityQuadRenderContext() : IUnityQuadRenderContext
{
    private readonly List<int> m_Indicies = new(32);

    public List<Vertex> Verticies = new(32);

    public int VerticiesCount => Verticies.Count;

    public int IndiciesCount => m_Indicies.Count;

    public Texture Texture { get; set; }

    public GUIRectangle? ClipRectangle { get; set; }

    public GUIRectangle? MaskRectangle { get; set; }

    public float CornerRadius { get; set; }

    public float ColorAlphaMultiplier { get; set; }

    public void AddIndex(int index) => m_Indicies.Add(index);

    public void AddVertex(Vertex vertex) => Verticies.Add(vertex);

    public void Clear()
    {
        m_Indicies?.Clear();
        Verticies.Clear();
    }

    public void CopyIndicies(int[] array) => m_Indicies.CopyTo(array);

    public void CopyVerticices(Vertex[] array) => Verticies.CopyTo(array);
}