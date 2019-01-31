namespace Starfield.Layout.Persona5.Serialization
{
    public abstract class FbnBinaryChunk
    {
        public FbnBinaryChunkType Type { get; }

        public int Field04 { get; set; }

        protected FbnBinaryChunk( FbnBinaryChunkType type, int field04 )
        {
            Type    = type;
            Field04 = field04;
        }
    }
}