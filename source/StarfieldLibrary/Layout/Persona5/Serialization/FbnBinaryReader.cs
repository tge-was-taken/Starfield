using System;
using System.IO;
using System.Text;
using Starfield.IO;
using Starfield.IO.Serialization;

namespace Starfield.Layout.Persona5.Serialization
{
    public class FbnBinaryReader : ObjectBinaryReader
    {
        public FbnBinaryReader( Stream input, Endianness endianness ) : base( input, endianness )
        {
        }

        public FbnBinaryReader( Stream input, string fileName, Endianness endianness ) : base( input, fileName, endianness )
        {
        }

        public FbnBinaryReader( string filepath, Endianness endianness ) : base( filepath, endianness )
        {
        }

        public FbnBinaryReader( Stream input, Encoding encoding, Endianness endianness ) : base( input, encoding, endianness )
        {
        }

        public FbnBinaryReader( Stream input, bool leaveOpen, Endianness endianness ) : base( input, leaveOpen, endianness )
        {
        }

        public FbnBinaryReader( Stream input, Encoding encoding, bool leaveOpen, Endianness endianness ) : base( input, encoding, leaveOpen, endianness )
        {
        }

        public FbnBinaryReader( Stream input, string fileName, Encoding encoding, bool leaveOpen, Endianness endianness ) : base( input, fileName, encoding, leaveOpen, endianness )
        {
        }


        public FbnBinaryList<T> ReadList<T>( FbnBinaryChunkType type, int field04 ) where T : class, IFbnListEntry
        {
            var list       = new FbnBinaryList<T>( type, field04 );
            ReadList( list );
            return list;
        }

        public void ReadList<T>( FbnBinaryList<T> list ) where T : class, IFbnListEntry
        {
            var entryCount = ReadInt32();
            SeekCurrent( 12 );

            for ( int i = 0; i < entryCount; i++ )
            {
                var entry = ReadListEntry( list.Type );
                list.Add( ( T )entry );
            }
        }

        public IFbnListEntry ReadListEntry( FbnBinaryChunkType type )
        {
            IFbnListEntry entry;
            switch ( type )
            {
                case FbnBinaryChunkType.HitTriggerList:
                case FbnBinaryChunkType.Type19:
                case FbnBinaryChunkType.Type22:
                    entry = ReadObject<FbnTriggerVolume>();
                    break;
                case FbnBinaryChunkType.EntranceList:
                    entry = ReadObject<FbnEntrance>();
                    break;
                case FbnBinaryChunkType.HitDefinitionList:
                    entry = ReadObject<FbnHitDefinition>();
                    break;
                case FbnBinaryChunkType.Type8:
                    entry = ReadObject<FbnBlock8Entry>();
                    break;
                case FbnBinaryChunkType.Type9:
                    entry = ReadObject<FbnBlock9Entry>();
                    break;
                case FbnBinaryChunkType.Type10:
                    entry = ReadObject<FbnBlock10Entry>();
                    break;
                case FbnBinaryChunkType.Type11:
                    entry = ReadObject<FbnBlock11Entry>();
                    break;
                case FbnBinaryChunkType.MessageTriggerList:
                    entry = ReadObject<FbnMessageTrigger>();
                    break;
                case FbnBinaryChunkType.Type18:
                    entry = ReadObject<FbnBlock18Entry>();
                    break;
                default:
                    throw new NotImplementedException( $"Reading block type {type} is not yet implemented" );
            }

            return entry;
        }
    }
}