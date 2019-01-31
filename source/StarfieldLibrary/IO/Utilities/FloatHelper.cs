namespace Starfield.IO.Utilities
{
    public static class FloatHelper
    {
        public static ushort ToHalf( float value )
        {
            int i = Unsafe.ReinterpretCast<float, int>( value );
            int s = ( i >> 16 ) & 0x00008000;                    // sign
            int e = ( ( i >> 23 ) & 0x000000ff ) - ( 127 - 15 ); // exponent
            int f = i & 0x007fffff;                              // fraction

            // need to handle NaNs and Inf?
            if ( e <= 0 )
            {
                if ( e < -10 )
                {
                    if ( s > 0 ) // handle -0.0
                        return 0x8000;
                    return 0;
                }
                f = ( f | 0x00800000 ) >> ( 1 - e );
                return ( ushort )( s | ( f >> 13 ) );
            }

            if ( e == 0xff - ( 127 - 15 ) )
            {
                if ( f == 0 ) // Inf
                    return ( ushort )( s | 0x7c00 );
                // NAN
                f >>= 13;
                return ( ushort )( s | 0x7c00 | f | ( f == 0 ? 1 : 0 ) );
            }

            if ( e > 30 ) // Overflow
                return ( ushort )( s | 0x7c00 );
            return ( ushort )( s | ( e << 10 ) | ( f >> 13 ) );
        }
    }
}
