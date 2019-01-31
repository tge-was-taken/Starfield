using System;
using OpenTK.Graphics.OpenGL;

namespace Starfield.Graphics
{
    public static class GLEnumConverter
    {
        public static OpenTK.Graphics.OpenGL.BufferUsageHint ToGL( this BufferUsageHint value )
        {
            switch ( value )
            {
                case BufferUsageHint.StaticDraw: return OpenTK.Graphics.OpenGL.BufferUsageHint.StaticDraw;
                default:
                    throw new ArgumentOutOfRangeException( nameof( value ), value, null );
            }
        }

        public static OpenTK.Graphics.OpenGL.VertexAttribPointerType ToGL( this VertexAttributeDataType value )
        {
            switch ( value )
            {
                case VertexAttributeDataType.Float:
                    return VertexAttribPointerType.Float;
                default:
                    throw new ArgumentOutOfRangeException( nameof( value ), value, null );
            }
        }
    }
}