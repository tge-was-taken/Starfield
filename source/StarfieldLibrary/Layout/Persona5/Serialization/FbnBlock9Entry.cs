using System.Numerics;
using Starfield.IO.Serialization;

namespace Starfield.Layout.Persona5.Serialization
{
    public class FbnBlock9Entry : IFbnListEntry
    {
        BinarySourceInfo IBinarySerializable.SourceInfo { get; set; }

        public int     Field00  { get; set; }
        public Vector3 Position { get; set; }
        public float[] Field10  { get; set; }
        public ushort  Field20  { get; set; }
        public ushort  Field22  { get; set; }
        public ushort  Field24  { get; set; }
        public ushort  Field26  { get; set; }

        void IBinarySerializable.Read( ObjectBinaryReader reader, object context )
        {
            Field00  = reader.ReadInt32();
            Position = reader.ReadVector3();
            Field10  = reader.ReadSingles( 4 );
            Field20  = reader.ReadUInt16();
            Field22  = reader.ReadUInt16();
            Field24  = reader.ReadUInt16();
            Field26  = reader.ReadUInt16();
        }

        void IBinarySerializable.Write( ObjectBinaryWriter writer, object context )
        {
            writer.Write( Field00 );
            writer.Write( Position );
            writer.Write( Field10 );
            writer.Write( Field20 );
            writer.Write( Field22 );
            writer.Write( Field24 );
            writer.Write( Field26 );
        }
    }
}