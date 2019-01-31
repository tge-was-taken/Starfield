namespace Starfield.Graphics
{
    /// <summary>
    /// WARNING: Corresponds to vertex attribute indices in shaders.
    /// </summary>
    public enum VertexAttributeType
    {
        Position    = 0,
        Normal      = 1,
        TexCoord    = 2,
        VertexColor = TexCoord + 8,
    }
}