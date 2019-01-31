using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using GFDLibrary.Rendering.OpenGL;
using OpenTK.Graphics.OpenGL;
using Starfield.Editor;
using Vector3 = OpenTK.Vector3;
using Vector4 = OpenTK.Vector4;

namespace Starfield.Graphics.Primitives
{
    public class GridPrimitive : IDrawable
    {
        private readonly float mMinZ;
        private readonly int mVertexArray;
        private readonly GLBuffer<Vector3> mVertexBuffer;
        private readonly GLShaderProgram mShader;

        public Vector4 Color { get; set; } = new Vector4( 0.15f, 0.15f, 0.15f, 1f );

        public Vector4 BlendColor { get; set; } = new Vector4( 0.5f, 0.5f, 0.5f, 1f );

        public GridPrimitive( int gridSize, int gridSpacing )
        {
            // thanks Skyth
            var vertices = new List<Vector3>();
            for ( int i = -gridSize; i <= gridSize; i += gridSpacing )
            {
                vertices.Add( new Vector3( i,          0, -gridSize ) );
                vertices.Add( new Vector3( i,          0, gridSize ) );
                vertices.Add( new Vector3( -gridSize, 0, i ) );
                vertices.Add( new Vector3( gridSize,  0, i ) );
            }

            mMinZ = vertices.Min( x => x.Z );
            mVertexArray = GL.GenVertexArray();
            GL.BindVertexArray( mVertexArray );

            mVertexBuffer = new GLBuffer<Vector3>( BufferTarget.ArrayBuffer, vertices.ToArray() );

            GL.VertexAttribPointer( 0, 3, VertexAttribPointerType.Float, false, mVertexBuffer.Stride, 0 );
            GL.EnableVertexAttribArray( 0 );

            mShader = ShaderStore.Instance.Get( "line" );
        }

        public void Draw( SceneViewport viewport, Matrix4x4 parentTransform )
        {
            mShader.Use();
            viewport.Camera.Bind( mShader );
            mShader.SetUniform( "uColor", Color );
            mShader.SetUniform( "uBlendColor", BlendColor );
            mShader.SetUniform( "uMinZ",       mMinZ );

            GL.BindVertexArray( mVertexArray );
            GL.DrawArrays( PrimitiveType.Lines, 0, mVertexBuffer.Count );
        }
    }
}
