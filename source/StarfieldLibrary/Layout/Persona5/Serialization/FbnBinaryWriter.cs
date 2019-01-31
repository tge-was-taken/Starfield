using System;
using System.IO;
using System.Text;
using Starfield.IO;
using Starfield.IO.Serialization;

namespace Starfield.Layout.Persona5.Serialization
{
    public class FbnBinaryWriter : ObjectBinaryWriter
    {
        public FbnBinaryWriter( Stream input, Endianness endianness ) : base( input, endianness )
        {
        }

        public FbnBinaryWriter( string filepath, Endianness endianness ) : base( filepath, endianness )
        {
        }

        public FbnBinaryWriter( string filepath, Encoding encoding, Endianness endianness ) : base( filepath, encoding, endianness )
        {
        }

        public FbnBinaryWriter( Stream input, Encoding encoding, Endianness endianness ) : base( input, encoding, endianness )
        {
        }

        public FbnBinaryWriter( Stream input, bool leaveOpen, Endianness endianness ) : base( input, leaveOpen, endianness )
        {
        }

        public FbnBinaryWriter( Stream input, Encoding encoding, bool leaveOpen, Endianness endianness ) : base( input, encoding, leaveOpen, endianness )
        {
        }

        public void WriteChunk( FbnBinaryChunk obj, Action writeFunc )
        {
            var startPosition = Position;
            Write( ( int )obj.Type );
            Write( obj.Field04 );
            var sizePosition = Position;
            Write( 0 );
            Write( obj.Type == FbnBinaryChunkType.Header ? 0 : 16 );
            writeFunc();
            var endPosition = Position;
            SeekBegin( sizePosition );
            Write( ( int )( endPosition - startPosition ) );
            SeekBegin( endPosition );
        }

        public void WriteList<T>( FbnBinaryList<T> list, int alignment = -1 ) where T : IFbnListEntry
        {
            if ( list == null )
                return;

            WriteChunk( list, () =>
            {
                // List header
                Write( list.Count );
                Write( 0 );
                Write( 0 );
                Write( 0 );

                // Write entries
                WriteObjects( list );

                if ( alignment > 0 )
                    Align( alignment );
            } );
        }
    }
}