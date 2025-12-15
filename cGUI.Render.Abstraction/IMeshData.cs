namespace cGUI.Render.Abstraction;

public interface IMeshData
{
    int IndicesOffset { get; }
    int VerticesOffset { get; }
    int VerticiesCount { get; }
    int IndicesCount { get; }
    int Order { get; }
    float ColorAlphaMultiplier { get; }
}