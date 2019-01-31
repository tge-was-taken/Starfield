using System.Runtime.InteropServices;

namespace Starfield.IO
{
    [StructLayout( LayoutKind.Explicit )]
    public struct IntFloat
    {
        [FieldOffset( 0)]
        public int IntValue;

        [FieldOffset( 0)]
        public float FloatValue;

        public IntFloat( int value )
        {
            FloatValue = 0;
            IntValue   = value;
        }

        public IntFloat( float value )
        {
            IntValue   = 0;
            FloatValue = value;
        }

        public static implicit operator IntFloat( int   value ) => new IntFloat( value );
        public static implicit operator IntFloat( float value ) => new IntFloat( value );

        public override string ToString()
        {
            return $"{IntValue} ({FloatValue}f)";
        }
    }
}