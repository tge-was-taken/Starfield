using System;

namespace Starfield.Graphics
{
    public struct VertexAttributeSource
    {
        public readonly Array                     Source;
        public readonly Type                      ElementType;
        public readonly BufferUsageHint           UsageHint;
        public readonly VertexAttributeDescriptor Descriptor;

        public VertexAttributeSource( Array source, Type elementType, BufferUsageHint hint, VertexAttributeDescriptor descriptor )
        {
            Source      = source;
            ElementType = elementType;
            UsageHint   = hint;
            Descriptor  = descriptor;
        }
    }
}