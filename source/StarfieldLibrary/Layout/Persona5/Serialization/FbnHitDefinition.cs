using Starfield.IO.Serialization;

namespace Starfield.Layout.Persona5.Serialization
{
    public class FbnHitDefinition : IFbnListEntry
    {
        BinarySourceInfo IBinarySerializable.SourceInfo { get; set; }

        public byte[] Field00     { get; set; }
        public ushort Field18     { get; set; }
        public byte   Field1A     { get; set; }
        public ushort ProcedureId { get; set; }
        public ushort Type        { get; set; }
        public byte[] Field1F     { get; set; }

        void IBinarySerializable.Read( ObjectBinaryReader reader, object context )
        {
            Field00     = reader.ReadBytes( 24 );
            Field18     = reader.ReadUInt16();
            Field1A     = reader.ReadByte();
            ProcedureId = reader.ReadUInt16();
            Type        = reader.ReadUInt16();
            Field1F     = reader.ReadBytes( 29 );
        }

        void IBinarySerializable.Write( ObjectBinaryWriter writer, object context )
        {
            writer.Write( Field00 );
            writer.Write( Field18 );
            writer.Write( Field1A );
            writer.Write( ProcedureId );
            writer.Write( Type );
            writer.Write( Field1F );
        }
    }
}