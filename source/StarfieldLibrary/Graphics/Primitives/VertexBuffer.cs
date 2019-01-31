using System;
using System.Collections.Generic;

namespace Starfield.Graphics.Primitives
{
    public abstract class VertexBuffer : IDisposable
    {
        protected List<VertexAttributeSource> Attributes;

        protected VertexBuffer( List<VertexAttributeSource> attributes )
        {
            Initialize( attributes );
            Attributes = attributes;
        }

        public abstract void Initialize( List<VertexAttributeSource> attributes );

        public abstract void Dispose();

        public abstract void Bind();
    }
}