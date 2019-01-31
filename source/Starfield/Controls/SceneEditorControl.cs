using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using GFDLibrary.Rendering.OpenGL;
using MoreLinq;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using Starfield.Editor;
using Starfield.Graphics;
using Starfield.Graphics.Primitives;
using Vector3 = System.Numerics.Vector3;
using Vector4 = System.Numerics.Vector4;

namespace Starfield.GUI.Controls
{
    public partial class SceneEditorControl : GLControl
    {
        private bool mInitialized;
        private bool mCanRender;

        private GLShaderProgram mDefaultShader;
        private GridPrimitive mGrid;
        private Point mLastMouseLocation;
        private SceneNode mSelectedNode;

        public Scene Scene { get; }
        public SceneViewport Viewport { get; }

        public event EventHandler<SceneNode> OnNodeSelected;

        public event EventHandler<SceneNode> OnNodeDeselected;

        public SceneNode SelectedNode
        {
            get => mSelectedNode;
            set
            {
                if ( value == mSelectedNode )
                    return;

                if ( mSelectedNode != null && value != mSelectedNode )
                {
                    mSelectedNode.Deselect();
                    OnNodeDeselected?.Invoke( this, mSelectedNode );
                }

                mSelectedNode = value;

                if ( mSelectedNode != null )
                {
                    mSelectedNode.Select();
                    OnNodeSelected?.Invoke( this, value );
                }
            }
        }

        public SceneEditorControl() : base(
            new GraphicsMode( 32, 24, 0, 0),
            3, 3,
#if GL_DEBUG
            GraphicsContextFlags.Debug | GraphicsContextFlags.ForwardCompatible )
#else
            GraphicsContextFlags.ForwardCompatible )
#endif
        {
            InitializeComponent();

            // make the control fill up the space of the parent cotnrol
            Dock = DockStyle.Fill;
            if ( !InitializeGL() )
                return;

            Scene = new Scene();

            //for ( int i = 0; i < 100; i++ )
            //{
            //    Scene.Root.Children.Add( new SceneNode( "Cube" )
            //    {
            //        Drawable = new CubePrimitive(),
            //        Translation = new System.Numerics.Vector3( i * 10, 0, 0 )
            //    } );
            //}

            Viewport = new SceneViewport( Width, Height, 45f );
        }

        private bool InitializeGL()
        {
            // required to use GL in the context of this control
            MakeCurrent();
            LogGLInfo();

            if ( !InitializeShaders() )
            {
                Visible = false;
                mCanRender = false;
            }
            else
            {
                mCanRender = true;
                InitializeGLRenderState();
            }

            mInitialized = true;
            return mCanRender;
        }

        /// <summary>
        /// Log GL info for diagnostics.
        /// </summary>
        private void LogGLInfo()
        {
            // todo: log to file? would help with debugging crashes on clients
            Trace.TraceInformation( "GL Info:" );
            Trace.TraceInformation( $"     Vendor         {GL.GetString( StringName.Vendor )}" );
            Trace.TraceInformation( $"     Renderer       {GL.GetString( StringName.Renderer )}" );
            Trace.TraceInformation( $"     Version        {GL.GetString( StringName.Version )}" );
            Trace.TraceInformation( $"     Extensions     {GL.GetString( StringName.Extensions )}" );
            Trace.TraceInformation( $"     GLSL version   {GL.GetString( StringName.ShadingLanguageVersion )}" );
            Trace.TraceInformation( "" );
        }

        /// <summary>
        /// Initializes GL state before rendering starts.
        /// </summary>
        private void InitializeGLRenderState()
        {
            GL.ClearColor( Color.Gray );
            GL.FrontFace( FrontFaceDirection.Ccw );
            GL.CullFace( CullFaceMode.Back );
            GL.Enable( EnableCap.CullFace );
            GL.Enable( EnableCap.DepthTest );

#if GL_DEBUG
            GL.Enable( EnableCap.DebugOutputSynchronous );
            //GL.DebugMessageCallback( GLDebugMessageCallback, IntPtr.Zero );
#endif

            GL.Enable( EnableCap.Multisample );
        }

        /// <summary>
        /// Executed when control is resized.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResize( EventArgs e )
        {
            if ( !mInitialized || !mCanRender )
                return;

            Viewport.Resize( Width, Height );
        }

        private void GLDebugMessageCallback( DebugSource source, DebugType type, int id, DebugSeverity severity, int length, IntPtr message, IntPtr userParam )
        {
            // notication for buffer using VIDEO memory
            if ( id == 0x00020071 )
                return;

            var msg = Marshal.PtrToStringAnsi( message, length );
            Trace.TraceInformation( $"GL Debug: {severity} {type} {msg}" );
        }

        protected override void OnPaint( PaintEventArgs e )
        {
            GL.Clear( ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit );
            Viewport.Draw( Scene );
            SwapBuffers();
        }

        //
        // Mouse events
        //
        private Point GetMouseLocationDelta( Point location )
        {
            location.X -= mLastMouseLocation.X;
            location.Y -= mLastMouseLocation.Y;

            return location;
        }

        protected override void OnMouseMove( System.Windows.Forms.MouseEventArgs e )
        {
            if ( !mCanRender )
                return;

            if ( e.Button.HasFlag( MouseButtons.Middle ) )
            {
                float multiplier = 0.5f;
                var keyboardState = Keyboard.GetState();

                if ( keyboardState.IsKeyDown( Key.ShiftLeft ) )
                {
                    multiplier *= 10f;
                }
                else if ( keyboardState.IsKeyDown( Key.ControlLeft ) )
                {
                    multiplier /= 2f;
                }

                var locationDelta = GetMouseLocationDelta( e.Location );

                if ( keyboardState.IsKeyDown( Key.AltLeft ) )
                {
                    // Orbit around model

                    //if ( mModel.ModelPack.Model.BoundingSphere.HasValue )
                    //{
                    //    var bSphere = mModel.ModelPack.Model.BoundingSphere.Value;
                    //    var camera = new GLPerspectiveTargetCamera( mCamera.Translation, mCamera.ZNear, mCamera.ZFar, mCamera.FieldOfView, mCamera.AspectRatio, new Vector3( bSphere.Center.X, bSphere.Center.Y, bSphere.Center.Z ) );
                    //    camera.Rotate( -locationDelta.Y / 100f, -locationDelta.X / 100f );
                    //    mCamera = camera;
                    //}

                    //Viewport.Camera.Mode = CameraMoveMode.Orbit;
                    //Viewport.Camera.Rotate( 0.01f, 0 );
                }
                else
                {
                    // Move camera
                    //var translation = Viewport.Camera.Position;
                    //if ( !( mCamera is GLPerspectiveFreeCamera ) )
                    //    translation = CalculateCameraTranslation( mCamera.FieldOfView, mModel.ModelPack.Model.BoundingSphere.Value );

                    //Viewport.Camera = new GLPerspectiveFreeCamera( translation, Viewport.Camera.ZNear, Viewport.Camera.ZFar, Viewport.Camera.Fov, Viewport.Camera.AspectRatio, Quaternion.Identity );
                    //Viewport.Camera.Translation = new Vector3(
                    //     Viewport.Camera.Translation.X - ( ( locationDelta.X / 3f ) * multiplier ),
                    //     Viewport.Camera.Translation.Y + ( ( locationDelta.Y / 3f ) * multiplier ),
                    //     Viewport.Camera.Translation.Z );

                    Viewport.Camera.Mode = CameraMoveMode.Free;
                    //Viewport.Camera.Pan( locationDelta.X * multiplier, locationDelta.Y * multiplier );
                }

                Viewport.Camera.Pan( locationDelta.X * multiplier, locationDelta.Y * multiplier );

                Invalidate();
            }

            mLastMouseLocation = e.Location;
        }

        protected override void OnMouseClick( System.Windows.Forms.MouseEventArgs e )
        {
            if ( e.Button != MouseButtons.Left )
                return;

            Matrix4x4.Invert(  Viewport.Camera.View * Viewport.Camera.Projection, out var invViewProjection );


            var mouseX = ( ( ( 2f * e.X ) / Width ) - 1f );
            var mouseY = ( 1f - ( ( 2f * e.Y ) / Height ) );
            var rayOrigin = Vector4.Transform( new Vector4( mouseX, mouseY, 0f, 1f ), invViewProjection );
            rayOrigin *= 1f / rayOrigin.W;
            var rayEnd = Vector4.Transform( new Vector4( mouseX, mouseY, 1f, 1f ), invViewProjection );
            rayEnd *= 1f / rayEnd.W;
            var rayDirection = Vector4.Normalize( rayEnd - rayOrigin );

            var ray = new Ray( new Vector3( rayOrigin.X,    rayOrigin.Y,    rayOrigin.Z ),
                               new Vector3( rayDirection.X, rayDirection.Y, rayDirection.Z ) );

            Invalidate();

            var selectedNodes = new List<SceneNode>();

            void RecurseNodes( SceneNode node )
            {
                var intersection = ray.IntersectPlane( node.Translation );
                var distance = intersection - node.Translation;
                var x = distance.X;
                if (x < 0) x = -x;
                var y = distance.Y;
                if (y < 0) y = -y;

                if ( x <= 20f && y <= 20f )
                {
                    Debug.WriteLine( node.Name );
                    selectedNodes.Add( node );
                }

                foreach ( var child in node.Children )
                {
                    RecurseNodes( child );
                }
            }

            RecurseNodes( Scene.Root );

            if ( selectedNodes.Count > 0 )
            {
                var closestSelectedNode = selectedNodes.MinBy( x => Viewport.Camera.Position.Z - x.Translation.Z ).First();
                SelectedNode = closestSelectedNode;
            }


            //for ( int i = 0; i < 100; i++ )
            //{
            //    Scene.Root.Children.Add( new Editor.SceneNode( "test" )
            //    {
            //        //Translation = Viewport.Camera.Position + ( new System.Numerics.Vector3( ray.Direction.X, ray.Direction.Y, ray.Direction.Z ) *
            //        //                                           new System.Numerics.Vector3( 0, 0, 100f ) ),
            //        //Translation = ( new Vector3( ray.Origin.X * Width, ray.Origin.Y * Height, 0 ) + Viewport.Camera.Position ) + ( -Vector3.UnitZ * new System.Numerics.Vector3( 0, 0, i * 10 ) ),
            //        Translation = ( ray.Origin + ( ray.Direction * new Vector3( i * 10 ) ) ),
            //        Drawable    = new CubePrimitive()
            //    } );
            //}
        }

        protected override void OnMouseWheel( System.Windows.Forms.MouseEventArgs e )
        {
            if ( !mCanRender )
                return;

            float multiplier = 0.25f;
            var keyboardState = Keyboard.GetState();

            if ( keyboardState.IsKeyDown( Key.ShiftLeft ) )
            {
                multiplier *= 10f;
            }
            else if ( keyboardState.IsKeyDown( Key.ControlLeft ) )
            {
                multiplier /= 2f;
            }

            Viewport.Camera.Zoom( e.Delta * multiplier );

            Invalidate();
        }

        /// <summary>
        /// Initializes shaders and links the shader program.
        /// </summary>
        private bool InitializeShaders()
        {
            mDefaultShader = ShaderStore.Instance.Get( "default" ) ?? ShaderStore.Instance.Get( "basic" );
            if ( mDefaultShader == null )
                return false;

            return true;
        }

        protected override void OnKeyDown( KeyEventArgs e )
        {
            if ( SelectedNode != null )
            {
                const float MOVE_SPEED = 1f;
                switch ( e.KeyCode )
                {
                    case Keys.Left:
                        SelectedNode.Translation -= Vector3.UnitX * MOVE_SPEED;
                        break;

                    case Keys.Right:
                        SelectedNode.Translation += Vector3.UnitX * MOVE_SPEED;
                        break;

                    case Keys.Up:
                        SelectedNode.Translation += Vector3.UnitY * MOVE_SPEED;
                        break;

                    case Keys.Down:
                        SelectedNode.Translation -= Vector3.UnitY * MOVE_SPEED;
                        break;
                }

                Invalidate();
            }
        }
    }
}
