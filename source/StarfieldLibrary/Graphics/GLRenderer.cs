using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;
using Starfield.Graphics.Primitives;

namespace Starfield.Graphics
{
    public class GLRenderer : Renderer
    {
        public override VertexBuffer CreateVertexBuffer( List<VertexAttributeSource> attributes )
        {
            return new GLVertexBuffer( attributes );
        }

        public override void SetViewport( Viewport viewport )
        {
            GL.Viewport( 0, 0, viewport.Width, viewport.Height );
        }
    }

    public class GLVertexBuffer : VertexBuffer
    {
        private int       mVertexArrayHandle;
        private List<int> mBufferHandles;

        public GLVertexBuffer( List<VertexAttributeSource> attributes ) : base( attributes )
        {
        }

        public override void Initialize( List<VertexAttributeSource> attributes )
        {
            mVertexArrayHandle = GL.GenVertexArray();
            GL.BindVertexArray( mVertexArrayHandle );

            mBufferHandles = new List<int>();
            for ( var i = 0; i < attributes.Count; i++ )
            {
                var attribute = attributes[i];

                if ( attribute.Source != null )
                {
                    var bufferHandle = GL.GenBuffer();
                    GL.BindBuffer( BufferTarget.ArrayBuffer, bufferHandle );
                    GL.BufferData( BufferTarget.ArrayBuffer, attribute.Source.Length * sizeof( float ), ( float[] )attribute.Source,
                                   attribute.UsageHint.ToGL() );

                    mBufferHandles.Add( bufferHandle );
                }


                GL.VertexAttribPointer( ( int )attribute.Descriptor.Type, attribute.Descriptor.Size, VertexAttribPointerType.Float, false, 0, 0 );      
            }

            GL.EnableVertexAttribArray( 0 );
        }

        public override void Bind()
        {
            GL.BindVertexArray( mVertexArrayHandle );
        }

        public override void Dispose()
        {
            // TODO
        }
    }
}