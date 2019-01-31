using System;
using System.Numerics;
using Starfield.Editor;

namespace Starfield.Graphics
{
    public struct Ray
    {
        public readonly Vector3 Origin;
        public readonly Vector3 Direction;

        public Ray( Vector3 origin, Vector3 direction )
        {
            Origin    = origin;
            Direction = direction;
        }

        public Ray( Vector2 screen, Matrix4x4 view, Matrix4x4 projection )
            : this( screen, MathHelper.CalculateScreenToViewMatrix( view, projection ) )
        {
        }

        public Ray( Vector2 screen, Matrix4x4 screenToViewMatrix )
        {
            Origin    = Vector3.Transform( new Vector3( screen, -1f ), screenToViewMatrix );
            var target = Vector3.Transform( new Vector3( screen, 0f ),  screenToViewMatrix );
            Direction = Vector3.Normalize( target - Origin );
        }

        public Vector3 IntersectPlane( Vector3 planePoint )
        {
            var planeNormal = new Vector3( 0, 0, 1 ); // forward
            var diff  = Origin - planePoint;
            var prod1 = Vector3.Dot( diff, planeNormal );
            var prod2 = Vector3.Dot( Direction, planeNormal );
            var prod3 = prod1 / prod2;
            return Origin - Direction * prod3;
        }
    }
}