using System.Numerics;
using Starfield.IO.Serialization;

namespace Starfield.Layout.Persona5.Serialization
{
    public class FbnEntrance : IFbnListEntry
    {
        BinarySourceInfo IBinarySerializable.SourceInfo { get; set; }

        public int     Field00  { get; set; }
        public int     Field04  { get; set; }
        public Vector3 Position { get; set; }
        public Vector3 Rotation { get; set; }
        public ushort  Id  { get; set; }
        public ushort  Field22  { get; set; }

        void IBinarySerializable.Read( ObjectBinaryReader reader, object context )
        {
            Field00  = reader.ReadInt32();
            Field04  = reader.ReadInt32();
            Position = reader.ReadVector3();
            Rotation = reader.ReadVector3();
            Id  = reader.ReadUInt16();
            Field22  = reader.ReadUInt16();
        }

        void IBinarySerializable.Write( ObjectBinaryWriter writer, object context )
        {
            writer.Write( Field00 );
            writer.Write( Field04 );
            writer.Write( Position );
            writer.Write( Rotation );
            writer.Write( Id );
            writer.Write( Field22 );
        }
    }
}