using System.Numerics;
using GFDLibrary.Rendering.OpenGL;
using OpenTK;
using Starfield.Graphics;
using Starfield.Graphics.Primitives;
using Quaternion = System.Numerics.Quaternion;
using Vector3 = System.Numerics.Vector3;

namespace Starfield.Editor
{
    public class SceneNode : IUpdateable, IDrawable
    {
        public string Name { get; set; }

        public Vector3 Translation { get; set; }

        public Quaternion Rotation { get; set; }

        public Vector3 Scale { get; set; }

        public SceneNode Parent { get; set; }

        public SceneNodeList Children { get; set; }

        public IDrawable Drawable { get; set; }

        public Matrix4x4 LocalTransform
        {
            get
            {
                var transform  = Matrix4x4.CreateFromQuaternion( Rotation ) * Matrix4x4.CreateScale( Scale );
                transform.Translation = Translation;
                return transform;
            }
        }

        public Matrix4x4 WorldTransform
        {
            get
            {
                if ( Parent == null )
                    return LocalTransform;

                return Parent.WorldTransform * LocalTransform;
            }
        }

        public object Tag { get; set; }

        public SceneNode( string name )
        {
            Name = name;
            Translation = Vector3.Zero;
            Rotation = Quaternion.Identity;
            Scale = new Vector3( 1f );
            Children = new SceneNodeList( this );
        }

        public virtual void Update()
        {
            foreach ( var child in Children )
            {
                child.Update();
            }
        }

        public virtual void Draw( SceneViewport viewport, Matrix4x4 parentTransform )
        {
            var transform = WorldTransform;

            Drawable?.Draw( viewport, transform );

            foreach ( var child in Children )
                child.Draw( viewport, transform );
        }

        public virtual void OnMouseOver() { }

        public virtual void Select()
        {
            // TODO handle this better
            if ( Drawable is CubePrimitive cube )
            {
                cube.Color += new OpenTK.Vector4( 0.6f, 0.6f, 0.6f, 0f );
            }
        }

        public virtual void Deselect()
        {
            // TODO handle this better
            if ( Drawable is CubePrimitive cube )
            {
                cube.Color -= new OpenTK.Vector4( 0.6f, 0.6f, 0.6f, 0f );
            }
        }
    }
}
