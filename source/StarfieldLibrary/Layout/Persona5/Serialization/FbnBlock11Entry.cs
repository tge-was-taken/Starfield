using System.Collections.Generic;
using Starfield.IO.Serialization;

namespace Starfield.Layout.Persona5.Serialization
{
    public class FbnBlock11Entry : IFbnListEntry
    {
        BinarySourceInfo IBinarySerializable.SourceInfo { get; set; }

        public int   Field00    { get; set; }
        public float Field04    { get; set; }
        public int   Field08    { get; set; }
        public int   Field0C    { get; set; }
        public int   Field10    { get; set; }
        public int   Field14    { get; set; }
        public short Field1A    { get; set; }
        public List<FbnBlock11SubEntry> Entries { get; private set; }

        public FbnBlock11Entry()
        {
            Entries = new List<FbnBlock11SubEntry>();
        }

        void IBinarySerializable.Read( ObjectBinaryReader reader, object context )
        {
            Field00    = reader.ReadInt32();
            Field04    = reader.ReadSingle();
            Field08    = reader.ReadInt32();
            Field0C    = reader.ReadInt32();
            Field10    = reader.ReadInt32();
            Field14    = reader.ReadInt32();
            var entryCount = reader.ReadInt16();
            Field1A    = reader.ReadInt16();
            Entries = reader.ReadObjectList<FbnBlock11SubEntry>( entryCount );
        }

        void IBinarySerializable.Write( ObjectBinaryWriter writer, object context )
        {
            writer.Write( Field00 );
            writer.Write( Field04 );
            writer.Write( Field08 );
            writer.Write( Field0C );
            writer.Write( Field10 );
            writer.Write( Field14 );
            writer.Write( ( short ) Entries.Count );
            writer.Write( Field1A );
            writer.WriteObjects( Entries );
        }
    }
}