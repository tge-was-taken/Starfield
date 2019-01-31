using System.Numerics;
using GFDLibrary.Rendering.OpenGL;
using OpenTK.Graphics.OpenGL;
using Starfield.Editor;
using Vector4 = OpenTK.Vector4;

namespace Starfield.Graphics.Primitives
{
    public class CubePrimitive : IDrawable
    {
        private static bool sInitialized;
        private static GLShaderProgram sShader;

        private static readonly float[] sVertices = new[]
        {
            // Vertex positions
		    -1.0f,-1.0f,-1.0f, -1.0f,-1.0f, 1.0f, -1.0f, 1.0f, 1.0f,  // Left Side
		    -1.0f,-1.0f,-1.0f, -1.0f, 1.0f, 1.0f, -1.0f, 1.0f,-1.0f,  // Left Side
		     1.0f, 1.0f,-1.0f, -1.0f,-1.0f,-1.0f, -1.0f, 1.0f,-1.0f,  // Back Side
		     1.0f,-1.0f, 1.0f, -1.0f,-1.0f,-1.0f,  1.0f,-1.0f,-1.0f,  // Bottom Side
		     1.0f, 1.0f,-1.0f,  1.0f,-1.0f,-1.0f, -1.0f,-1.0f,-1.0f,  // Back Side
		     1.0f,-1.0f, 1.0f, -1.0f,-1.0f, 1.0f, -1.0f,-1.0f,-1.0f,  // Bottom Side
		    -1.0f, 1.0f, 1.0f, -1.0f,-1.0f, 1.0f,  1.0f,-1.0f, 1.0f,  // Front Side
		     1.0f, 1.0f, 1.0f,  1.0f,-1.0f,-1.0f,  1.0f, 1.0f,-1.0f,  // Right Side
		     1.0f,-1.0f,-1.0f,  1.0f, 1.0f, 1.0f,  1.0f,-1.0f, 1.0f,  // Right Side
		     1.0f, 1.0f, 1.0f,  1.0f, 1.0f,-1.0f, -1.0f, 1.0f,-1.0f,  // Top Side
		     1.0f, 1.0f, 1.0f, -1.0f, 1.0f,-1.0f, -1.0f, 1.0f, 1.0f,  // Top Side
		     1.0f, 1.0f, 1.0f, -1.0f, 1.0f, 1.0f,  1.0f,-1.0f, 1.0f   // Front Side

        };

        private static readonly float[] sNormals = new[]
        {
            // Normals
            -1.0f, 0.0f, 0.0f, // Left Side
		     0.0f, 0.0f, -1.0f, // Back Side
		     0.0f,-1.0f, 0.0f, // Bottom Side
		     0.0f, 0.0f, -1.0f, // Back Side
		    -1.0f, 0.0f, 0.0f, // Left Side
		     0.0f, -1.0f, 0.0f, // Bottom Side
		     0.0f, 0.0f, 1.0f, // front Side
		     1.0f, 0.0f, 0.0f, // right Side
		     1.0f, 0.0f, 0.0f, // right Side
		     0.0f, 1.0f, 0.0f, // top Side
		     0.0f, 1.0f, 0.0f, // top Side
		     0.0f, 0.0f, 1.0f, // front Side
        };

        private static VertexBuffer sVertexBuffer;

        static CubePrimitive()
        {
            for ( int i = 0; i < sVertices.Length; i++ )
            {
                sVertices[ i ] += 1;
                sVertices[ i ] *= 10f;
            }
        }

        public Vector4 Color { get; set; } = new Vector4( 0, 0, 0, 1f );

        public CubePrimitive()
        {
            InitializeOnce();
        }

        public CubePrimitive( Vector4 color ) : this()
        {
            Color = color;
        }

        private void InitializeOnce()
        {
            if ( !sInitialized )
            { 
                sVertexBuffer = new VertexBufferBuilder()
                            .AddAttributeBuffer( sVertices, BufferUsageHint.StaticDraw, new VertexAttributeDescriptor( VertexAttributeType.Position, 3, VertexAttributeDataType.Float, 0, 0 ) )
                            .AddAttributeBuffer( sNormals,  BufferUsageHint.StaticDraw, new VertexAttributeDescriptor( VertexAttributeType.Normal,   3, VertexAttributeDataType.Float, 0, 0 ) )
                            .Build();

                sShader      = ShaderStore.Instance.Get( "shaded_uniform_color" );
                sInitialized = true;
            }
        }

        public void Draw( SceneViewport viewport, Matrix4x4 parentTransform )
        {
            sShader.Use();
            viewport.Camera.Bind( sShader );
            sShader.SetUniform( "uColor", Color );
            sShader.SetUniform( "uModel", parentTransform.ToOpenTK() );

            sVertexBuffer.Bind();
            GL.DrawArrays( PrimitiveType.Triangles, 0, 36 );
        }
    }

    //public abstract class Mesh
    //{
    //    public readonly Material Material;

    //    public abstract void Draw();
    //}

    //public class Material
    //{

    //}

    //public class Shader
    //{

    //}
}
