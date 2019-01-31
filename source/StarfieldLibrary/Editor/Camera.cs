using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using GFDLibrary.Rendering.OpenGL;
using Starfield.Graphics;

namespace Starfield.Editor
{
    public class Camera
    {
        private float mYaw;
        private float mPitch;
        private Vector3 mUp;
        private Vector3 mRight;
        private Vector3 mDirection;
        private Vector3 mPosition;
        private Matrix4x4 mProjection;
        private Matrix4x4 mView;

        private bool mTransformDirty;
        private bool mViewDirty;
        private bool mProjectionDirty;
        private float mOrbitDistance;
        private Vector3 mOrbitTarget;
        private float mAspectRatio;
        private float mFov;
        private CameraMoveMode mMode;

        public CameraMoveMode Mode
        {
            get => mMode;
            set
            {
                mMode = value;
                mTransformDirty = mViewDirty = true;
            }
        }

        public Vector3 Position
        {
            get => mPosition;
            set
            {
                mPosition = value;
                mTransformDirty = true;
                mViewDirty = true;
            }
        }

        public Vector3 Direction
        {
            get
            {
                if ( mTransformDirty )
                    UpdateTransform();

                return mDirection;
            }
        }

        public Vector3 Right
        {
            get
            {
                if ( mTransformDirty )
                    UpdateTransform();

                return mRight;
            }
        }

        public Vector3 Up
        {
            get
            {
                if ( mTransformDirty )
                    UpdateTransform();

                return mUp;
            }
        }

        public float Yaw
        {
            get => mYaw;
            set
            {
                mYaw = value;
                mTransformDirty = true;
                mViewDirty = true;
            }
        }

        public float Pitch
        {
            get => mPitch;
            set
            {
                mPitch = value;

                if ( mPitch > MathHelper.HalfPI )
                    mPitch = ( float ) MathHelper.HalfPI;
                else if ( mPitch < -MathHelper.HalfPI )
                    mPitch = ( float ) -MathHelper.HalfPI;

                mTransformDirty = true;
                mViewDirty = true;
            }
        }

        public float Fov
        {
            get => mFov;
            set
            {
                mFov = value;
                mProjectionDirty = true;
            }
        }

        public float AspectRatio
        {
            get => mAspectRatio;
            set
            {
                mAspectRatio = value;
                mProjectionDirty = true;
            }
        }

        public float ZNear { get; set; }

        public float ZFar { get; set; }

        public Vector3 OrbitTarget
        {
            get => mOrbitTarget;
            set
            {
                mOrbitTarget = value;

                if ( Mode == CameraMoveMode.Orbit )
                    mTransformDirty = mViewDirty = true;
            }
        }

        public float OrbitDistance
        {
            get => mOrbitDistance;
            set
            {
                mOrbitDistance = value;

                if ( Mode == CameraMoveMode.Orbit )
                    mTransformDirty = mViewDirty = true;
            }
        }

        public float MoveSpeed { get; set; }

        // - Matrices - 
        public Matrix4x4 View
        {
            get
            {
                if ( mViewDirty )
                    UpdateView();

                return mView;
            }
        }

        public Matrix4x4 Projection
        {
            get
            {
                if ( mProjectionDirty )
                    UpdateProjection();

                return mProjection;
            }
        }

        public Camera( float aspectRatio, float fov )
        {
            mTransformDirty = true;
            mViewDirty = true;
            mProjectionDirty = true;

            Mode = CameraMoveMode.Free;
            Yaw = MathHelper.DegreesToRadians( 90f );
            MoveSpeed = 1f;
            AspectRatio = aspectRatio;
            OrbitDistance = 10f;
            ZNear = 1f;
            ZFar = 100000f;
            Fov = fov;
            Position = new Vector3( 0, 20, -100f );
        }

        public void Zoom( float amount )
        {
            Console.WriteLine( $"Camera.Zoom( {amount} )" );
            switch ( Mode )
            {
                case CameraMoveMode.Free:
                    Position += ( amount * MoveSpeed ) * Direction;
                    break;
                case CameraMoveMode.Orbit:
                    OrbitDistance -= ( amount * MoveSpeed );
                    break;
            }
        }

        public void Pan( float x, float y )
        {
            Console.WriteLine( $"Camera.Pan( {x}, {y} )" );

            switch ( Mode )
            {
                case CameraMoveMode.Free:
                    Position += ( -x * MoveSpeed ) * Right;
                    Position += ( y * MoveSpeed ) * Up;
                    break;
                case CameraMoveMode.Orbit:
                    //Rotate( x * 0.01f, y * 0.01f );
                    //Position = mOrbitTarget + ( mDirection * -mOrbitDistance );
                    break;
            }
        }

        public void Rotate( float x, float y )
        {
            Yaw -= ( x * MoveSpeed );
            Pitch -= ( y * MoveSpeed );
        }

        public Ray CastRay( Vector2 screen )
        {
            return new Ray( screen, View, Projection );
        }

        public void Bind( GLShaderProgram shaderProgram )
        {
            shaderProgram.SetUniform( "uView",       View.ToOpenTK() );
            shaderProgram.SetUniform( "uProjection", Projection.ToOpenTK() );
        }

        private void UpdateTransform()
        {
            mDirection = MathHelper.CalculateDirection( mYaw, mPitch );
            mRight     = MathHelper.CalculateRight( mYaw );
            mUp        = MathHelper.CalculateUp( mRight, mDirection );

            if ( Mode == CameraMoveMode.Orbit )
            {
                if ( mOrbitDistance < 1f )
                    mOrbitDistance = 1f;

                mPosition = mOrbitTarget + ( mDirection * -mOrbitDistance );
            }

            mTransformDirty = false;
            mViewDirty = true;
        }

        private void UpdateView()
        {
            if ( Mode == CameraMoveMode.Free )
                mView = MathHelper.CalculateView( Position, Direction, Up, Right );
            else
                mView = MathHelper.CalculateView( Position, Vector3.Normalize( OrbitTarget - Position ), Up, Right );
            mViewDirty = false;
        }

        private void UpdateProjection()
        {
            mProjection = Matrix4x4.CreatePerspectiveFieldOfView( MathHelper.DegreesToRadians( Fov ), AspectRatio, ZNear, ZFar );
            mProjectionDirty = false;
        }
    }

    public enum CameraMoveMode
    {
        Free,
        Orbit
    }
}
