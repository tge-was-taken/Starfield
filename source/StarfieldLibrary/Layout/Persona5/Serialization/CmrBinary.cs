using System;
using System.IO;
using Starfield.IO.Serialization;

namespace Starfield.Layout.Persona5.Serialization
{
    public class CmrBinary : SimpleBinary
    {
        public const int MAGIC = 0x434D5230;
        public const int VERSION = 0x1000003;
        public const int FILE_SIZE = 108;

        public int Version { get; set; }
        public int Field08 { get; set; }
        public float Field10 { get; set; }
        public float Field14 { get; set; }
        public float Field18 { get; set; }
        public int Field1C { get; set; }
        public float Field20 { get; set; }
        public int Field24 { get; set; }
        public float Field28 { get; set; }
        public float Field2C { get; set; }
        public float Field30 { get; set; }
        public float Field34 { get; set; }
        public int Field38 { get; set; }
        public float Field3C { get; set; }
        public float Field40 { get; set; }
        public float Field44 { get; set; }
        public float Field48 { get; set; }
        public float Field4C { get; set; }
        public float Field50 { get; set; }
        public float Field54 { get; set; }
        public float Field58 { get; set; }
        public float Field5C { get; set; }
        public float Field60 { get; set; }
        public float Field64 { get; set; }
        public int Field68 { get; set; }

        public CmrBinary()
        {
            Version = VERSION;
            Field08 = 1;
            Field10 = Field14 = 450f;
            Field18 = 8;
            Field1C = 0;
            Field20 = 160f;
            Field24 = 0;
            Field28 = 15f;
            Field2C = 600f;
            Field30 = 500f;
            Field34 = 1500f;
            Field38 = 0;
            Field3C = 24f;
            Field40 = 2.2f;
            Field44 = Field48 = 0.5f;
            Field4C = 2.05f;
            Field50 = 0.35f;
            Field54 = Field58 = 0.5f;
            Field5C = 2.8f;
            Field60 = Field64 = 0.6f;
            Field68 = 0;
        }

        public CmrBinary( string filepath ) : base( filepath ) { }

        public CmrBinary( Stream stream, bool leaveOpen ) : base( stream, leaveOpen )
        {
        }

        protected override void Read( ObjectBinaryReader reader )
        {
            var magic = reader.ReadInt32();
            if ( magic != MAGIC )
                throw new InvalidDataException( "Invalid file magic" );

            Version = reader.ReadInt32();
            Field08 = reader.ReadInt32();
            var fileSize = reader.ReadInt32();
            if ( fileSize != FILE_SIZE )
                throw new NotImplementedException( "Unimplemented version of CMR" );

            Field10 = reader.ReadSingle();
            Field14 = reader.ReadSingle();
            Field18 = reader.ReadSingle();
            Field1C = reader.ReadInt32();
            Field20 = reader.ReadSingle();
            Field24 = reader.ReadInt32();
            Field28 = reader.ReadSingle();
            Field2C = reader.ReadSingle();
            Field30 = reader.ReadSingle();
            Field34 = reader.ReadSingle();
            Field38 = reader.ReadInt32();
            Field3C = reader.ReadSingle();
            Field40 = reader.ReadSingle();
            Field44 = reader.ReadSingle();
            Field48 = reader.ReadSingle();
            Field4C = reader.ReadSingle();
            Field50 = reader.ReadSingle();
            Field54 = reader.ReadSingle();
            Field58 = reader.ReadSingle();
            Field5C = reader.ReadSingle();
            Field60 = reader.ReadSingle();
            Field64 = reader.ReadSingle();
            Field68 = reader.ReadInt32();
        }

        protected override void Write( ObjectBinaryWriter writer )
        {
            writer.Write( MAGIC );
            writer.Write( Version );
            writer.Write( Field08 );
            writer.Write( FILE_SIZE );
            writer.Write( Field10 );
            writer.Write( Field14 );
            writer.Write( Field18 );
            writer.Write( Field1C );
            writer.Write( Field20 );
            writer.Write( Field24 );
            writer.Write( Field28 );
            writer.Write( Field2C );
            writer.Write( Field30 );
            writer.Write( Field34 );
            writer.Write( Field38 );
            writer.Write( Field3C );
            writer.Write( Field40 );
            writer.Write( Field44 );
            writer.Write( Field48 );
            writer.Write( Field4C );
            writer.Write( Field50 );
            writer.Write( Field54 );
            writer.Write( Field58 );
            writer.Write( Field5C );
            writer.Write( Field60 );
            writer.Write( Field64 );
            writer.Write( Field68 );
        }
    }
}
