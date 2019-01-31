using System;
using OpenTK;
using Starfield.Graphics;

namespace Starfield.Editor
{
    public class Scene
    {
        public SceneNode Root { get; }

        public Scene()
        {
            Root = new SceneNode( "Root" );
        }

        public void Update()
        {
            Root.Update();
        }
    }
}
