using Starfield.IO.Serialization;

namespace Starfield.Layout.Persona5.Serialization
{
    public class FbnBlock8Entry : IFbnListEntry
    {
        BinarySourceInfo IBinarySerializable.SourceInfo { get; set; }

        public int     Field00 { get; set; }
        public ushort  Field04 { get; set; }
        public ushort  Field06 { get; set; }
        public float[] Field08 { get; set; }

        void IBinarySerializable.Read( ObjectBinaryReader reader, object context )
        {
            Field00 = reader.ReadInt32();
            Field04 = reader.ReadUInt16();
            Field06 = reader.ReadUInt16();
            Field08 = reader.ReadSingles( 8 );
        }

        void IBinarySerializable.Write( ObjectBinaryWriter writer, object context )
        {
            writer.Write( Field00 );
            writer.Write( Field04 );
            writer.Write( Field06 );
            writer.Write( Field08 );
        }
    }
}