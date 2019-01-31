using System.Numerics;
using GFDLibrary;
using GFDLibrary.Common;
using GFDLibrary.Models;
using GFDLibrary.Rendering.OpenGL;
using OpenTK;
using Starfield.Editor;

namespace Starfield.Graphics
{
    public interface IDrawable
    {
        void Draw( SceneViewport viewport, Matrix4x4 parentTransform );
    }

    public class ModelPackDrawable : IDrawable
    {
        public ModelPack ModelPack { get; }

        public GLModel Model { get; }

        public ModelPackDrawable( ModelPack model )
        {
            ModelPack = model;
            Model = new GLModel( model, ( material, textureName ) => new GLTexture( model.Textures[ textureName ] ) );
        }

        public ModelPackDrawable( ModelPack model, MaterialTextureCreator textureCreator )
        {
            Model = new GLModel( model, textureCreator );
        }

        public void Draw( SceneViewport viewport, Matrix4x4 parentTransform )
        {
            var view = viewport.Camera.View.ToOpenTK();
            var proj = viewport.Camera.Projection.ToOpenTK();
            Model.Draw( ShaderStore.Instance.Get( "default" ), ref view, ref proj, 0 );
        }
    }
}
