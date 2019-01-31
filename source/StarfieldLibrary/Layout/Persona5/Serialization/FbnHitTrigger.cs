using System.Numerics;
using Starfield.IO.Serialization;

namespace Starfield.Layout.Persona5.Serialization
{
    public class FbnTriggerVolume : IFbnListEntry
    {
        BinarySourceInfo IBinarySerializable.SourceInfo { get; set; }

        public short   Field00     { get; set; }
        public short   Field02     { get; set; }
        public short   Field04     { get; set; }
        public short   Field06     { get; set; }
        public Vector3 Center      { get; set; }
        public float   Field14     { get; set; }
        public float   Field18     { get; set; }
        public float   Field1C     { get; set; }
        public float   Field20     { get; set; }
        public float   Field24     { get; set; }
        public float   Field28     { get; set; }
        public Vector3 BottomRight { get; set; }
        public Vector3 TopRight    { get; set; }
        public Vector3 BottomLeft  { get; set; }
        public Vector3 TopLeft     { get; set; }
        public float   Field5C     { get; set; }
        public float   Field60     { get; set; }

        void IBinarySerializable.Read( ObjectBinaryReader reader, object context )
        {
            Field00     = reader.ReadInt16();
            Field02     = reader.ReadInt16();
            Field04     = reader.ReadInt16();
            Field06     = reader.ReadInt16();
            Center      = reader.ReadVector3();
            Field14     = reader.ReadSingle();
            Field18     = reader.ReadSingle();
            Field1C     = reader.ReadSingle();
            Field20     = reader.ReadSingle();
            Field24     = reader.ReadSingle();
            Field28     = reader.ReadSingle();
            BottomRight = reader.ReadVector3();
            TopRight    = reader.ReadVector3();
            BottomLeft  = reader.ReadVector3();
            TopLeft     = reader.ReadVector3();
            Field5C     = reader.ReadSingle();
            Field60     = reader.ReadSingle();
        }

        void IBinarySerializable.Write( ObjectBinaryWriter writer, object context )
        {
            writer.Write( Field00 );
            writer.Write( Field02 );
            writer.Write( Field04 );
            writer.Write( Field06 );
            writer.Write( Center );
            writer.Write( Field14 );
            writer.Write( Field18 );
            writer.Write( Field1C );
            writer.Write( Field20 );
            writer.Write( Field24 );
            writer.Write( Field28 );
            writer.Write( BottomRight );
            writer.Write( TopRight );
            writer.Write( BottomLeft );
            writer.Write( TopLeft );
            writer.Write( Field5C );
            writer.Write( Field60 );
        }
    }
}