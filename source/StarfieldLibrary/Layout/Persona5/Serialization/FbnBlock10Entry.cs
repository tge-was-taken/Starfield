using Starfield.IO.Serialization;

namespace Starfield.Layout.Persona5.Serialization
{
    public class FbnBlock10Entry : IFbnListEntry
    {
        BinarySourceInfo IBinarySerializable.SourceInfo { get; set; }

        public int     Field00 { get; set; }
        public int     Field04 { get; set; }
        public float[] Field08 { get; set; }
        public int     Field68 { get; set; }
        public int     Field6C { get; set; }
        public int     Field70 { get; set; }
        public int     Field74 { get; set; }
        public ushort  Field78 { get; set; }
        public ushort  Field7A { get; set; }
        public float[] Field7C { get; set; }

        void IBinarySerializable.Read( ObjectBinaryReader reader, object context )
        {
            Field00 = reader.ReadInt32();
            Field04 = reader.ReadInt32();
            Field08 = reader.ReadSingles( 24 );
            Field68 = reader.ReadInt32();
            Field6C = reader.ReadInt32();
            Field70 = reader.ReadInt32();
            Field74 = reader.ReadInt32();
            Field78 = reader.ReadUInt16();
            Field7A = reader.ReadUInt16();
            Field7C = reader.ReadSingles( 6 );
        }

        void IBinarySerializable.Write( ObjectBinaryWriter writer, object context )
        {
            writer.Write( Field00 );
            writer.Write( Field04 );
            writer.Write( Field08 );
            writer.Write( Field68 );
            writer.Write( Field6C );
            writer.Write( Field70 );
            writer.Write( Field74 );
            writer.Write( Field78 );
            writer.Write( Field7A );
            writer.Write( Field7C );
        }
    }
}