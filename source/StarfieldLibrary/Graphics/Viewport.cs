namespace Starfield.Graphics
{
    public class Viewport
    {
        public int Width { get; private set; }

        public int Height { get; private set; }

        public Viewport( int width, int height )
        {
            Width  = width;
            Height = height;
        }

        public virtual void Resize( int width, int height )
        {
            Width  = width;
            Height = height;
            Renderer.Instance.SetViewport( this );
        }
    }
}