using cGUI.Abstraction.Structs;
using cGUI.Render.Abstraction;
using UnityEngine;

namespace cGUI.Unity.Render.Abstraction;

public interface IUnityQuadRenderContext : IQuadRenderContext 
{
    Texture Texture { get; set; }
    GUIRectangle? ClipRectangle { get; set; }
    GUIRectangle? MaskRectangle { get; set; }
    float ColorAlphaMultiplier { get; set; }
}