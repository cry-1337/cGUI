namespace cGUI.Render.Abstraction;

public interface IMeshData
{
    int IndiciesOffset { get; set; }
    int VerticesOffset { get; set; }
    int VerticiesCount { get; set; }
    int IndicesCount { get; set; }
    int Order { get; set; }
    float ColorAlphaMultiplier { get; set; }
}