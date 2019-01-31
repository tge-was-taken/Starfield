using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using GFDLibrary.Graphics;
using Starfield.IO.Utilities;
using Starfield.Text.Encodings;

namespace Starfield.IO
{
    public partial class EndianBinaryReader : BinaryReader
    {
        private Endianness mEndianness;

        public Endianness Endianness
        {
            get => mEndianness;
            set
            {
                SwapBytes = value != EndiannessHelper.SystemEndianness;
                mEndianness = value;
            }
        }

        public string FileName { get; set; }

        public bool SwapBytes { get; private set; }

        public long Position
        {
            get => BaseStream.Position;
            set => BaseStream.Position = value;
        }

        public long Length => BaseStream.Length;

        public Encoding Encoding { get; set; }

        public EndianBinaryReader( Stream input, Endianness endianness )
            : this( input, GetFileName( input ), ShiftJisEncoding.Instance, false, endianness )
        {
        }

        public EndianBinaryReader( Stream input, string fileName, Endianness endianness )
            : this( input, fileName, ShiftJisEncoding.Instance, false, endianness )
        {
        }

        public EndianBinaryReader( string filepath, Endianness endianness )
            : this( File.OpenRead( filepath ), filepath, ShiftJisEncoding.Instance, false, endianness )
        {
        }

        public EndianBinaryReader( Stream input, Encoding encoding, Endianness endianness )
            : this( input, GetFileName( input ), encoding, false, endianness )
        {
        }

        public EndianBinaryReader( Stream input, bool leaveOpen, Endianness endianness )
            : this( input, GetFileName( input ), ShiftJisEncoding.Instance, leaveOpen, endianness )
        {
        }

        public EndianBinaryReader( Stream input, Encoding encoding, bool leaveOpen, Endianness endianness )
            : this( input, GetFileName( input ), encoding, leaveOpen, endianness )
        {
        }

        public EndianBinaryReader( Stream input, string fileName, Encoding encoding, bool leaveOpen, Endianness endianness )
            : base( input, encoding, leaveOpen )
        {
            FileName = fileName;
            Encoding   = encoding;
            Endianness = endianness;
        }

        private static string GetFileName( Stream stream ) => stream is FileStream fs ? fs.Name : null;

        public void Seek( long offset, SeekOrigin origin )
        {
            BaseStream.Seek( offset, origin );
        }

        public void SeekBegin( long offset )
        {
            BaseStream.Seek( offset, SeekOrigin.Begin );
        }

        public void SeekCurrent( long offset )
        {
            BaseStream.Seek( offset, SeekOrigin.Current );
        }

        public void SeekEnd( long offset )
        {
            BaseStream.Seek( offset, SeekOrigin.End );
        }

        public byte ReadByteExpects( byte expected, string message )
        {
            var actual = ReadByte();
            if ( actual != expected )
                throw new InvalidDataException( message );

            return actual;
        }

        public List<byte> ReadByteList( int count )
        {
            var list = new List<byte>( count );
            for ( var i = 0; i < list.Capacity; i++ )
            {
                list.Add( ReadByte() );
            }

            return list;
        }

        public sbyte[] ReadSBytes( int count )
        {
            var array = new sbyte[count];
            for ( var i = 0; i < array.Length; i++ )
                array[i] = ReadSByte();

            return array;
        }

        public bool[] ReadBooleans( int count )
        {
            var array = new bool[count];
            for ( var i = 0; i < array.Length; i++ )
                array[i] = ReadBoolean();

            return array;
        }

        public override short ReadInt16()
        {
            return SwapBytes ? EndiannessHelper.Swap( base.ReadInt16() ) : base.ReadInt16();
        }

        public short ReadInt16Expects( short expected, string message )
        {
            var actual = ReadInt16();
            if ( actual != expected )
                throw new InvalidDataException( message );

            return actual;
        }

        public short[] ReadInt16Array( int count )
        {
            var array = new short[count];
            for ( var i = 0; i < array.Length; i++ )
            {
                array[i] = ReadInt16();
            }

            return array;
        }

        public List<short> ReadInt16List( int count )
        {
            var list = new List<short>( count );
            for ( var i = 0; i < list.Capacity; i++ )
            {
                list.Add( ReadInt16() );
            }

            return list;
        }

        public override ushort ReadUInt16()
        {
            return SwapBytes ? EndiannessHelper.Swap( base.ReadUInt16() ) : base.ReadUInt16();
        }

        public ushort ReadUInt16( ushort expected, string message )
        {
            var actual = ReadUInt16();
            if ( actual != expected )
                throw new InvalidDataException( message );

            return actual;
        }

        public ushort[] ReadUInt16s( int count )
        {
            var array = new ushort[count];
            for ( var i = 0; i < array.Length; i++ )
            {
                array[i] = ReadUInt16();
            }

            return array;
        }

        public override decimal ReadDecimal()
        {
            return SwapBytes ? EndiannessHelper.Swap( base.ReadDecimal() ) : base.ReadDecimal();
        }

        public decimal[] ReadDecimals( int count )
        {
            var array = new decimal[count];
            for ( var i = 0; i < array.Length; i++ )
            {
                array[i] = ReadDecimal();
            }

            return array;
        }

        public override double ReadDouble()
        {
            return SwapBytes ? EndiannessHelper.Swap( base.ReadDouble() ) : base.ReadDouble();
        }

        public double[] ReadDoubles( int count )
        {
            var array = new double[count];
            for ( var i = 0; i < array.Length; i++ )
            {
                array[i] = ReadDouble();
            }

            return array;
        }

        public override int ReadInt32()
        {
            return SwapBytes ? EndiannessHelper.Swap( base.ReadInt32() ) : base.ReadInt32();
        }

        public int ReadInt32Expects( int expected, string message = "Unexpected value" )
        {
            var actual = ReadInt32();
            if ( actual != expected )
                throw new InvalidDataException( message );

            return actual;
        }

        public int[] ReadInt32s( int count )
        {
            var array = new int[count];
            for ( var i = 0; i < array.Length; i++ )
            {
                array[i] = ReadInt32();
            }

            return array;
        }

        public override long ReadInt64()
        {
            return SwapBytes ? EndiannessHelper.Swap( base.ReadInt64() ) : base.ReadInt64();
        }

        public long[] ReadInt64s( int count )
        {
            var array = new long[count];
            for ( var i = 0; i < array.Length; i++ )
            {
                array[i] = ReadInt64();
            }

            return array;
        }

        public override float ReadSingle()
        {
            return SwapBytes ? EndiannessHelper.Swap( base.ReadSingle() ) : base.ReadSingle();
        }

        public float ReadSingleExpects( float expected, string message )
        {
            var actual = ReadSingle();
            if ( actual != expected )
                throw new InvalidDataException( message );

            return actual;
        }

        public float[] ReadSingles( int count )
        {
            var array = new float[count];
            for ( var i = 0; i < array.Length; i++ )
            {
                array[i] = ReadSingle();
            }

            return array;
        }

        public override uint ReadUInt32()
        {
            return SwapBytes ? EndiannessHelper.Swap( base.ReadUInt32() ) : base.ReadUInt32();
        }

        public uint ReadUInt32Expects( uint expected, string message )
        {
            var actual = ReadUInt32();
            if ( actual != expected )
                throw new InvalidDataException( message );

            return actual;
        }

        public uint[] ReadUInt32s( int count )
        {
            var array = new uint[count];
            for ( var i = 0; i < array.Length; i++ )
            {
                array[i] = ReadUInt32();
            }

            return array;
        }

        public Color ReadColor()
        {
            Color color;
            color.R = ReadByte();
            color.G = ReadByte();
            color.B = ReadByte();
            color.A = ReadByte();

            return color;
        }

        public Color[] ReadColors( int count )
        {
            var array = new Color[count];
            for ( var i = 0; i < array.Length; i++ )
                array[i] = ReadColor();

            return array;
        }

        public override ulong ReadUInt64()
        {
            return SwapBytes ? EndiannessHelper.Swap( base.ReadUInt64() ) : base.ReadUInt64();
        }

        public ulong[] ReadUInt64s( int count )
        {
            var array = new ulong[count];
            for ( var i = 0; i < array.Length; i++ )
            {
                array[i] = ReadUInt64();
            }

            return array;
        }

        public string ReadString( StringBinaryFormat format, int fixedLength = -1 )
        {
            var bytes = new List<byte>();

            switch ( format )
            {
                case StringBinaryFormat.NullTerminated:
                    {
                        byte b;
                        while ( ( b = ReadByte() ) != 0 )
                            bytes.Add( b );
                    }
                    break;

                case StringBinaryFormat.FixedLength:
                    {
                        if ( fixedLength == -1 )
                            throw new ArgumentException( "Invalid fixed length specified" );

                        byte b;
                        var terminated = false;
                        for ( var i = 0; i < fixedLength; i++ )
                        {
                            b = ReadByte();
                            if ( b == 0 )
                                terminated = true;

                            if ( !terminated )
                                bytes.Add( b );
                        }
                    }
                    break;

                case StringBinaryFormat.PrefixedLength8:
                    {
                        byte length = ReadByte();
                        for ( var i = 0; i < length; i++ )
                            bytes.Add( ReadByte() );
                    }
                    break;

                case StringBinaryFormat.PrefixedLength16:
                    {
                        ushort length = ReadUInt16();
                        for ( var i = 0; i < length; i++ )
                            bytes.Add( ReadByte() );
                    }
                    break;

                case StringBinaryFormat.PrefixedLength32:
                    {
                        uint length = ReadUInt32();
                        for ( var i = 0; i < length; i++ )
                            bytes.Add( ReadByte() );
                    }
                    break;

                default:
                    throw new ArgumentException( "Unknown string format", nameof( format ) );
            }

            return Encoding.GetString( bytes.ToArray() );
        }

        public string[] ReadStrings( int count, StringBinaryFormat format, int fixedLength = -1 )
        {
            var value = new string[count];
            for ( var i = 0; i < value.Length; i++ )
                value[i] = ReadString( format, fixedLength );

            return value;
        }

        public void Align( int i )
        {
            SeekBegin( AlignmentHelper.Align( Position, i ) );
        }
    }
}
