using cGUI.Abstraction.Structs;
using cGUI.Render.Abstraction;
using UnityEngine;

namespace cGUI.Unity.Render.Abstraction;

public interface IUnityMeshData : IMeshData
{
    Texture? MainTexture { get; set; }
    Material Material { get; set; }
    MaterialPropertyBlock? MaterialProperties { get; set; }
    Quaternion Rotation { get; set; }
    MeshTopology Topology { get; set; }
    GUIRectangle? MaskRect { get; set; }
    GUIRectangle? CornerRoundRadius { get; set; }
}