using cGUI.Event.Abstraction;
using System.Drawing;

namespace cGUI.Events.Models;

public struct RenderEvent(Rectangle bounds) : IEvent
{
    Rectangle Bounds => bounds;
}