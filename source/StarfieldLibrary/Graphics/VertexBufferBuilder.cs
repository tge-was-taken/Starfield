using System.Collections.Generic;
using Starfield.Graphics.Primitives;

namespace Starfield.Graphics
{
    public class VertexBufferBuilder
    {
        private readonly List<VertexAttributeSource> mBufferList;

        public VertexBufferBuilder()
        {
            mBufferList = new List<VertexAttributeSource>();
        }

        public VertexBufferBuilder AddAttribute<T>( VertexAttributeDescriptor attributeDescriptor )
        {
            mBufferList.Add( new VertexAttributeSource( null, typeof( T ), 0, attributeDescriptor ) );
            return this;
        }

        public VertexBufferBuilder AddAttributeBuffer<T>(T[] bufferSource, BufferUsageHint usageHint, VertexAttributeDescriptor attributeDescriptor)
        {
            mBufferList.Add( new VertexAttributeSource( bufferSource, typeof( T ), usageHint, attributeDescriptor ) );
            return this;
        }

        public VertexBuffer Build()
        {
            return Renderer.Instance.CreateVertexBuffer( mBufferList );
        }
    }
}