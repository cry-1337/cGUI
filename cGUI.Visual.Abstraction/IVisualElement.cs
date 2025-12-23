using cGUI.Abstraction.Interfaces;

namespace cGUI.Visual.Abstraction;

public interface IVisualElement : IElement
{
    bool IsMaskable { get; }
}