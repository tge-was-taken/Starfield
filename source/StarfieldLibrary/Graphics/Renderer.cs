using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using Starfield.Graphics.Primitives;

namespace Starfield.Graphics
{
    public abstract class Renderer
    {
        private static Renderer sInstance;

        public static Renderer Instance => sInstance ?? ( sInstance = new GLRenderer() );

        public abstract VertexBuffer CreateVertexBuffer( List<VertexAttributeSource> attributes );

        public abstract void SetViewport( Viewport viewport );
    }
}
