using System.Numerics;
using Starfield.IO.Serialization;

namespace Starfield.Layout.Persona5.Serialization
{
    public class FbnBlock18Entry : IFbnListEntry
    {
        BinarySourceInfo IBinarySerializable.SourceInfo { get; set; }

        public int        Field00  { get; set; }
        public ushort     Field04  { get; set; }
        public ushort     Field06  { get; set; }
        public Vector3    Position { get; set; }
        public Quaternion Rotation { get; set; }
        public ushort     Field24  { get; set; }
        public ushort     Field26  { get; set; }
        public ushort     Field28  { get; set; }
        public ushort     Field2A  { get; set; }

        void IBinarySerializable.Read( ObjectBinaryReader reader, object context )
        {
            Field00  = reader.ReadInt32();
            Field04  = reader.ReadUInt16();
            Field06  = reader.ReadUInt16();
            Position = reader.ReadVector3();
            Rotation = reader.ReadQuaternion();
            Field24  = reader.ReadUInt16();
            Field26  = reader.ReadUInt16();
            Field28  = reader.ReadUInt16();
            Field2A  = reader.ReadUInt16();
        }

        void IBinarySerializable.Write( ObjectBinaryWriter writer, object context )
        {
            writer.Write( Field00 );
            writer.Write( Field04 );
            writer.Write( Field06 );
            writer.Write( Position );
            writer.Write( Rotation );
            writer.Write( Field24 );
            writer.Write( Field26 );
            writer.Write( Field28 );
            writer.Write( Field2A );
        }
    }
}