using System.Diagnostics;
using System.IO;
using System.Text;
using Starfield.IO;
using Starfield.IO.Serialization;

namespace Starfield.Layout.Persona5.Serialization
{
    public abstract class SimpleBinary : IBinarySerializable
    {
        BinarySourceInfo IBinarySerializable.SourceInfo { get; set; }

        protected SimpleBinary()
        {
        }

        protected SimpleBinary( string filepath ) : this( File.OpenRead( filepath ), false ) { }

        protected SimpleBinary( Stream stream, bool leaveOpen ) => Read( stream, leaveOpen );

        public void Save( Stream stream, bool leaveOpen ) => Write( stream, leaveOpen );

        public void Save( string filepath ) => Save( File.Create( filepath ), false );

        public MemoryStream Save()
        {
            var stream = new MemoryStream();
            Save( stream, true );
            Debug.Assert( stream.CanRead );
            stream.Position = 0;
            return stream;
        }

        protected virtual void Read( Stream stream, bool leaveOpen )
        {
            using ( var reader = new ObjectBinaryReader( stream, Encoding.Default, leaveOpen, Endianness.Big ) )
                Read( reader );
        }

        protected virtual void Read( ObjectBinaryReader reader ) { }

        protected virtual void Write( Stream stream, bool leaveOpen )
        {
            using ( var writer = new ObjectBinaryWriter( stream, Encoding.Default, leaveOpen, Endianness.Big ) )
                Write( writer );
        }

        protected virtual void Write( ObjectBinaryWriter writer ) { }

        void IBinarySerializable.Read( ObjectBinaryReader reader, object context )
            => Read( reader );

        void IBinarySerializable.Write( ObjectBinaryWriter writer, object context )
            => Write( writer );
    }
}