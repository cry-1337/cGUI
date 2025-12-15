using cGUI.Abstraction.Structs;
using cGUI.Render.Abstraction;
using UnityEngine;

namespace cGUI.Unity.Render.Abstraction;

public interface IUnityMeshData : IMeshData
{
    Texture MainTexture { get; }
    Material Material { get; }
    MaterialPropertyBlock MaterialProperties { get; }
    Quaternion Rotation { get; }
    MeshTopology Topology { get; }
    GUIRectangle MaskRect { get; }
    GUIRectangle CornerRoundRadius { get; }
}