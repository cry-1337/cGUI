namespace cGUI.Abstraction.Interfaces;

public interface IScrollable
{
    bool SupportsScroll { get; }
    float ScrollY { get; }
    float MaxScroll { get; }
}
