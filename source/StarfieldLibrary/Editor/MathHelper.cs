using System;
using System.Numerics;

namespace Starfield.Editor
{
    public static class MathHelper
    {
        public const double HalfPI = Math.PI / 2;
        public const double QuarterPI = Math.PI / 4;

        public static Vector3 CalculateDirection( float yaw, float pitch )
        {
            // https://stackoverflow.com/a/10569719/4755778
            var xzLen = Math.Cos( pitch );
            return new Vector3( ( float )( xzLen * Math.Cos( yaw ) ),
                                ( float )( Math.Sin( pitch ) ),
                                ( float )( xzLen * Math.Sin( -yaw ) ) );
        }

        public static Vector3 CalculateRight( float yaw )
        {
            // https://github.com/arukibree/PrimeWorldEditor/blob/9eb3d1d0110e2c28df0c9ebf29b905982a697f2d/src/Core/Render/CCamera.cpp#L222
            return new Vector3( ( float ) Math.Cos( yaw - ( Math.PI / 2 ) ),
                                ( float ) Math.Sin( yaw - ( Math.PI / 2 ) ),
                                0 );
        }

        public static Vector3 CalculateUp( Vector3 right, Vector3 direction )
        {
            // https://github.com/arukibree/PrimeWorldEditor/blob/9eb3d1d0110e2c28df0c9ebf29b905982a697f2d/src/Core/Render/CCamera.cpp#L228
            return Vector3.Cross( right, direction );
        }

        public static Matrix4x4 CalculateView( Vector3 position, Vector3 direction, Vector3 up, Vector3 right )
        {
            // fix z axis being reversed only for the camera
            position.Z = -position.Z;

            // https://github.com/arukibree/PrimeWorldEditor/blob/9eb3d1d0110e2c28df0c9ebf29b905982a697f2d/src/Core/Render/CCamera.cpp#L249
            return new Matrix4x4( right.X, up.X, -direction.X, 0,
                                  right.Y, up.Y, -direction.Y, 0,
                                  right.Z, up.Z, -direction.Z, 0,
                                  -Vector3.Dot( right, position ), -Vector3.Dot( up, position ), -Vector3.Dot( direction, position ), 1f );
        }

        public static Matrix4x4 CalculateScreenToViewMatrix( Matrix4x4 view, Matrix4x4 projection )
        {
            //Matrix4x4.Invert( Matrix4x4.Transpose( view ) * Matrix4x4.Transpose( projection ), out var screenToViewMatrix );
            Matrix4x4.Invert( view * projection, out var screenToViewMatrix );
            //screenToViewMatrix = Matrix4x4.Transpose( screenToViewMatrix );
            return screenToViewMatrix;
        }

        public static void CalculateLookAt( Vector3 position, Vector3 target, out float yaw, out float pitch )
        {
            // https://gamedev.stackexchange.com/a/112572
            var direction = Vector3.Normalize( target - position );
            yaw = ( float )Math.Atan2( direction.X, direction.Z );
            pitch = ( float )Math.Asin( direction.Y );
        }

        /// <summary>
        /// Convert degrees to radians
        /// </summary>
        /// <param name="degrees">An angle in degrees</param>
        /// <returns>The angle expressed in radians</returns>
        public static float DegreesToRadians( float degrees )
        {
            const float degToRad = ( float )Math.PI / 180.0f;
            return degrees * degToRad;
        }
    }
}