using System.Numerics;
using GFDLibrary.Rendering.OpenGL;
using OpenTK.Graphics.OpenGL;
using Starfield.Graphics;
using Starfield.Graphics.Primitives;
using Quaternion = OpenTK.Quaternion;
using Vector3 = OpenTK.Vector3;

namespace Starfield.Editor
{
    public class SceneViewport : Viewport
    {
        private readonly GridPrimitive mGrid = new GridPrimitive( 2000, 16 );

        //public GLPerspectiveFreeCamera Camera { get; set; }

        public Camera Camera { get; set; }

        public SceneViewport( int width, int height, float fov ) : base( width, height )
        {
            //Camera = new GLPerspectiveFreeCamera( new Vector3( 0, 0, -100f ), 1f, 100000f, fov, ( float )width / height, Quaternion.Identity );
            Camera = new Camera( ( float )width / height, fov );
        }

        public override void Resize( int width, int height )
        {
            base.Resize( width, height );
            Camera.AspectRatio = ( float )width / height;
        }

        public virtual void Draw( Scene scene )
        {
            mGrid.Draw( this, Matrix4x4.Identity );
            scene.Root.Draw( this, Matrix4x4.Identity );
        }
    }
}