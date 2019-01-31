using Starfield.IO;
using Starfield.IO.Serialization;

namespace Starfield.Layout.Persona5.Serialization
{
    public class FbnBlock11SubEntry : IFbnListEntry
    {
        BinarySourceInfo IBinarySerializable.SourceInfo { get; set; }

        public IntFloat Field00 { get; set; }
        public IntFloat Field04 { get; set; }
        public IntFloat Field08 { get; set; }
        public IntFloat Field0C { get; set; }

        void IBinarySerializable.Read( ObjectBinaryReader reader, object context )
        {
            Field00 = reader.ReadInt32();
            Field04 = reader.ReadInt32();
            Field08 = reader.ReadInt32();
            Field0C = reader.ReadInt32();
        }

        void IBinarySerializable.Write( ObjectBinaryWriter writer, object context )
        {
            writer.Write( Field00.IntValue );
            writer.Write( Field04.IntValue );
            writer.Write( Field08.IntValue );
            writer.Write( Field0C.IntValue );
        }
    }
}