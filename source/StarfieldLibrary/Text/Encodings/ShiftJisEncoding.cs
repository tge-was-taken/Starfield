using System.Text;

namespace Starfield.Text.Encodings
{
    public static class ShiftJisEncoding
    {
        public static Encoding Instance { get; } = Encoding.GetEncoding( 932 );
    }
}
