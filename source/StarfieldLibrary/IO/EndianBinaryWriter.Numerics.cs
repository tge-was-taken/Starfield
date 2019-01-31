using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Starfield.IO
{
    public partial class EndianBinaryWriter
    {
        [DebuggerStepThrough, MethodImpl( MethodImplOptions.AggressiveInlining )]
        public void Write( Vector2 value )
        {
            Write( value.X );
            Write( value.Y );
        }

        [DebuggerStepThrough, MethodImpl( MethodImplOptions.AggressiveInlining )]
        public void WriteVector2Half( Vector2 value )
        {
            WriteHalf( value.X );
            WriteHalf( value.Y );
        }

        [DebuggerStepThrough, MethodImpl( MethodImplOptions.AggressiveInlining )]
        public void Write( IEnumerable<Vector2> values )
        {
            foreach ( var item in values )
                Write( item );
        }

        [DebuggerStepThrough, MethodImpl( MethodImplOptions.AggressiveInlining )]
        public void Write( Vector3 value )
        {
            Write( value.X );
            Write( value.Y );
            Write( value.Z );
        }

        [DebuggerStepThrough, MethodImpl( MethodImplOptions.AggressiveInlining )]
        public void Write( IEnumerable<Vector3> values )
        {
            foreach ( var item in values )
                Write( item );
        }

        [DebuggerStepThrough, MethodImpl( MethodImplOptions.AggressiveInlining )]
        public void Write( Vector4 value )
        {
            Write( value.X );
            Write( value.Y );
            Write( value.Z );
            Write( value.W );
        }

        [DebuggerStepThrough, MethodImpl( MethodImplOptions.AggressiveInlining )]
        public void Write( Quaternion value )
        {
            Write( value.X );
            Write( value.Y );
            Write( value.Z );
            Write( value.W );
        }

        [DebuggerStepThrough, MethodImpl( MethodImplOptions.AggressiveInlining )]
        public void Write( IEnumerable<Vector4> values )
        {
            foreach ( var item in values )
                Write( item );
        }

        [DebuggerStepThrough, MethodImpl( MethodImplOptions.AggressiveInlining )]
        public void WriteVector2( Vector2 value ) => Write( value );

        [DebuggerStepThrough, MethodImpl( MethodImplOptions.AggressiveInlining )]
        public void WriteVector2s( IEnumerable<Vector2> values ) => Write( values );

        [DebuggerStepThrough, MethodImpl( MethodImplOptions.AggressiveInlining )]
        public void WriteVector3( Vector3 value ) => Write( value );

        [DebuggerStepThrough, MethodImpl( MethodImplOptions.AggressiveInlining )]
        public void WriteVector3s( IEnumerable<Vector3> values ) => Write( values );

        [DebuggerStepThrough, MethodImpl( MethodImplOptions.AggressiveInlining )]
        public void WriteVector4( Vector4 value ) => Write( value );

        [DebuggerStepThrough, MethodImpl( MethodImplOptions.AggressiveInlining )]
        public void WriteVector4s( IEnumerable<Vector4> values ) => Write( values );
    }
}
