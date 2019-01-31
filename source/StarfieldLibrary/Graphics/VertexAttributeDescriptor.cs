namespace Starfield.Graphics
{
    public struct VertexAttributeDescriptor
    {
        public readonly VertexAttributeType     Type;
        public readonly int                     Size;
        public readonly VertexAttributeDataType DataType;
        public readonly int                     Stride;
        public readonly int                     Offset;

        public VertexAttributeDescriptor( VertexAttributeType type, int size, VertexAttributeDataType dataType, int stride, int offset )
        {
            Type     = type;
            Size     = size;
            DataType = dataType;
            Stride   = stride;
            Offset   = offset;
        }

        public static implicit operator VertexAttributeDescriptor(
            (VertexAttributeType type, int size, VertexAttributeDataType dataType, int stride, int offset) tuple )
        {
            return new VertexAttributeDescriptor( tuple.type, tuple.size, tuple.dataType, tuple.stride, tuple.offset );
        }
    }
}