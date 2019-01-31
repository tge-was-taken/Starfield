using System;
using System.ComponentModel;
using System.Globalization;
using System.Numerics;

namespace Starfield.GUI.TypeConverters
{
    public class Vector2TypeConverter : TypeConverter
    {
        public override bool CanConvertFrom( ITypeDescriptorContext context, Type sourceType )
        {
            if ( sourceType == typeof( string ) )
            {
                return true;
            }
            else
            {
                return base.CanConvertFrom( context, sourceType );
            }
        }

        public override bool CanConvertTo( ITypeDescriptorContext context, Type destinationType )
        {
            if ( destinationType == typeof( string ) )
            {
                return true;
            }
            else
            {
                return base.CanConvertTo( context, destinationType );
            }
        }

        public override object ConvertTo( ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType )
        {
            if ( destinationType == typeof( string ) && value is Vector2 vector )
            {
                return
                    $"[{vector.X.ToString( CultureInfo.InvariantCulture )}, {vector.Y.ToString( CultureInfo.InvariantCulture )}]";
            }
            else
            {
                return base.ConvertTo( context, culture, value, destinationType );
            }
        }

        public override object ConvertFrom( ITypeDescriptorContext context, CultureInfo culture, object value )
        {
            if ( value is string input )
            {
                var floatValueStrings = input.Trim( '[', ']' )
                                             .Split( new[] { "," }, StringSplitOptions.RemoveEmptyEntries );

                var x = float.Parse( floatValueStrings[0], CultureInfo.InvariantCulture );
                var y = float.Parse( floatValueStrings[1], CultureInfo.InvariantCulture );
                return new Vector2 ( x, y );
            }
            else
            {
                return base.ConvertFrom( context, culture, value );
            }
        }
    }
}