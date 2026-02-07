using cGUI.Animations;

namespace cGUI.Elements.Models;

public struct ElementTweenOptions()
{
    public float HoverInDuration = 0.15f;
    public float HoverOutDuration = 0.1f;
    public EaseType HoverEasing = EaseType.SmoothStep;
    public float PressInDuration = 0.05f;
    public float PressOutDuration = 0.1f;
    public EaseType PressEasing = EaseType.OutQuad;
    public float ToggleDuration = 0.2f;
    public EaseType ToggleEasing = EaseType.SmoothStep;
}
