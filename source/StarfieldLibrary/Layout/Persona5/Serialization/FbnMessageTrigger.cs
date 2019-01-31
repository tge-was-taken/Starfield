using System.Collections.Generic;
using System.Numerics;
using Starfield.IO.Serialization;

namespace Starfield.Layout.Persona5.Serialization
{
    public class FbnMessageTrigger : IFbnListEntry
    {
        BinarySourceInfo IBinarySerializable.SourceInfo { get; set; }

        public int           Field00   { get; set; }
        public Vector3       Field04   { get; set; }
        public Vector3       Field10   { get; set; }
        public ushort        Field1C   { get; set; }
        public ushort        Field1E   { get; set; }
        public int           Field20   { get; set; }
        public int           Field24   { get; set; }
        public int           Field28   { get; set; }
        public ushort        Field2E   { get; set; }
        public List<Vector3> Positions { get; }

        public FbnMessageTrigger()
        {
            Positions = new List<Vector3>();
        }

        void IBinarySerializable.Read( ObjectBinaryReader reader, object context )
        {
            Field00 = reader.ReadInt32();
            Field04 = reader.ReadVector3();
            Field10 = reader.ReadVector3();
            Field1C = reader.ReadUInt16();
            Field1E = reader.ReadUInt16();
            Field20 = reader.ReadInt32();
            Field24 = reader.ReadInt32();
            Field28 = reader.ReadInt32();
            var positionCount = reader.ReadUInt16();
            Field2E = reader.ReadUInt16();
            for ( int i = 0; i < positionCount; i++ )
                Positions.Add( reader.ReadVector3() );
        }

        void IBinarySerializable.Write( ObjectBinaryWriter writer, object context )
        {
            writer.Write( Field00 );
            writer.Write( Field04 );
            writer.Write( Field10 );
            writer.Write( Field1C );
            writer.Write( Field1E );
            writer.Write( Field20 );
            writer.Write( Field24 );
            writer.Write( Field28 );
            writer.Write( ( short ) Positions.Count );
            writer.Write( Field2E );
            writer.Write( Positions );
        }
    }
}