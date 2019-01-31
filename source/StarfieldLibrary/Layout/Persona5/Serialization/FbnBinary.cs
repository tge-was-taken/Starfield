using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using Starfield.IO;

namespace Starfield.Layout.Persona5.Serialization
{
    public class FbnBinary : FbnBinaryChunk
    {
        public FbnBinaryList<FbnTriggerVolume> HitTriggers { get; set; }

        public FbnBinaryList<FbnEntrance> Entrances { get; set; }

        public FbnBinaryList<FbnBlock8Entry> Block8Entries { get; set; }

        public FbnBinaryList<FbnBlock9Entry> Block9Entries { get; set; }

        public FbnBinaryList<FbnBlock10Entry> Block10Entries { get; set; }

        public FbnBinaryList<FbnBlock11Entry> Block11Entries { get; set; }

        public FbnBinaryList<FbnMessageTrigger> MessageTriggers { get; set; }

        public FbnBinaryList<FbnBlock18Entry> Block18Entries { get; set; }

        public FbnBinaryList<FbnTriggerVolume> Block19Entries { get; set; }

        public FbnBinaryList<FbnTriggerVolume> Block22Entries { get; set; }

        public FbnBinary() : base( FbnBinaryChunkType.Header, 0x11121200 )
        {

        }

        public FbnBinary( string filepath ) : this( File.OpenRead( filepath ), false ) { }

        public FbnBinary( Stream stream, bool leaveOpen ) : this()
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
            Debug.Assert( stream.CanRead );
            stream.Position = 0;
            return stream;
        }

        private void Read( FbnBinaryReader reader )
        {
            // TODO: make code less fragile (too many switch cases)
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
                    case FbnBinaryChunkType.Header:
                        Field04 = field04;
                        break;

                    case FbnBinaryChunkType.HitTriggerList: HitTriggers = reader.ReadList<FbnTriggerVolume>( type, field04 ); break;
                    case FbnBinaryChunkType.EntranceList: Entrances = reader.ReadList<FbnEntrance>( type, field04 ); break;
                    case FbnBinaryChunkType.Type8: Block8Entries = reader.ReadList<FbnBlock8Entry>( type, field04 ); break;
                    case FbnBinaryChunkType.Type9: Block9Entries = reader.ReadList<FbnBlock9Entry>( type, field04 ); break;
                    case FbnBinaryChunkType.Type10: Block10Entries = reader.ReadList<FbnBlock10Entry>( type, field04 ); break;
                    case FbnBinaryChunkType.Type11: Block11Entries = reader.ReadList<FbnBlock11Entry>( type, field04 ); break;
                    case FbnBinaryChunkType.MessageTriggerList: MessageTriggers = reader.ReadList<FbnMessageTrigger>( type, field04 ); break;
                    case FbnBinaryChunkType.Type18: Block18Entries = reader.ReadList<FbnBlock18Entry>( type, field04 ); break;
                    case FbnBinaryChunkType.Type19: Block19Entries = reader.ReadList<FbnTriggerVolume>( type, field04 ); break;
                    case FbnBinaryChunkType.Type22: Block22Entries = reader.ReadList<FbnTriggerVolume>( type, field04 ); break;
                    default:
                        throw new NotImplementedException( $"Reading block type {type} is not yet implemented" );
                }

                reader.SeekBegin( endPosition );
            }
        }

        private void Write( FbnBinaryWriter writer )
        {
            // Header
            writer.Write( ( int ) Type );
            writer.Write( Field04 );
            writer.Write( 16 );
            writer.Write( 0 );

            // Write lists
            writer.WriteList( HitTriggers );
            writer.WriteList( Entrances );
            writer.WriteList( Block8Entries );
            writer.WriteList( Block9Entries );
            writer.WriteList( Block10Entries );
            writer.WriteList( Block11Entries );
            writer.WriteList( MessageTriggers );
            writer.WriteList( Block18Entries );
            writer.WriteList( Block19Entries );
            writer.WriteList( Block22Entries );
        }
    }
}
