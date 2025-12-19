using cGUI.Abstraction.Structs;
using cGUI.Unity.Render.Abstraction;
using UnityEngine;

namespace cGUI.Unity.Render;

public struct UnityMeshData(Material material) : IUnityMeshData
{
    public Texture? MainTexture { get; set; } = null;

    public Material Material { get; set; } = material;

    public MaterialPropertyBlock? MaterialProperties { get; set; } = null;

    public Quaternion Rotation { get; set; } = Quaternion.identity;

    public MeshTopology Topology { get; set; } = MeshTopology.Triangles;

    public GUIRectangle? MaskRect { get; set; } = null;

    public GUIRectangle? CornerRoundRadius { get; set; } = null;

    public int IndiciesOffset { get; set; } = 0;

    public int VerticesOffset { get; set; } = 0;

    public int VerticiesCount { get; set; } = 0;

    public int IndicesCount { get; set; } = 0;

    public int Order { get; set; } = 0;
}