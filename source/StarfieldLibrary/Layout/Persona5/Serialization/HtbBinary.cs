using System;
using System.IO;
using System.Text;
using Starfield.IO;

namespace Starfield.Layout.Persona5.Serialization
{
    public class HtbBinary : FbnBinaryList<FbnHitDefinition>
    {
        public HtbBinary() : base( FbnBinaryChunkType.HitDefinitionList, 0x12112600 )
        {
        }

        public HtbBinary( string filepath ) : this( File.OpenRead( filepath ), false ) { }

        public HtbBinary( Stream stream, bool leaveOpen ) : this()
        {
            using ( var reader = new FbnBinaryReader( stream, Encoding.Default, leaveOpen, Endianness.Big ) )
                Read( reader );
        }

        public void Save( Stream stream, bool leaveOpen )
        {
            using ( var writer = new FbnBinaryWriter( stream, Encoding.Default, leaveOpen, Endianness.Big ) )
                Write( writer );
        }

        public void Save( string filepath ) => Save( File.Create( filepath ), false );

        public MemoryStream Save()
        {
            var stream = new MemoryStream();
            Save( stream, true );
            return stream;
        }

        private void Read( FbnBinaryReader reader )
        {
            while ( reader.Position < reader.BaseStream.Length )
            {
                var startPosition = reader.Position;
                var type = ( FbnBinaryChunkType )reader.ReadInt32();
                var field04 = reader.ReadInt32();
                var size = reader.ReadInt32();
                /* var dataOffset = */ reader.ReadInt32();
                var endPosition = startPosition + size;

                switch ( type )
                {
                    case FbnBinaryChunkType.HitDefinitionList:
                        Field04 = field04;
                        reader.ReadList( this );
                        break;
                    default:
                        throw new NotImplementedException( $"Reading block type {type} is not yet implemented" );
                }

                reader.SeekBegin( endPosition );
            }
        }

        private void Write( FbnBinaryWriter writer )
        {
            writer.WriteList( this, 16 );
        }
    }
}
