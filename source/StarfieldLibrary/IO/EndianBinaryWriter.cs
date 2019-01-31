using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using GFDLibrary.Graphics;
using Starfield.IO.Utilities;
using Starfield.Text.Encodings;

namespace Starfield.IO
{
    public partial class EndianBinaryWriter : BinaryWriter
    {
        private Endianness mEndianness;
        private Encoding mEncoding;
        private int mEncodingMinByteCount;

        public Endianness Endianness
        {
            get => mEndianness;
            set
            {
                SwapBytes = value != EndiannessHelper.SystemEndianness;
                mEndianness = value;
            }
        }

        public bool SwapBytes { get; private set; }

        public Encoding Encoding
        {
            get => mEncoding;
            set
            {
                mEncoding = value;
                mEncodingMinByteCount = mEncoding.GetMinByteCount();
            }
        }

        public long Position
        {
            get => BaseStream.Position;
            set => BaseStream.Position = value;
        }

        public long Length => BaseStream.Length;

        public int DefaultAlignment { get; set; } = 16;

        public EndianBinaryWriter( Stream input, Endianness endianness )
            : base( input )
        {
            Init( ShiftJisEncoding.Instance, endianness );
        }

        public EndianBinaryWriter( string filepath, Endianness endianness )
            : base( File.Create( filepath, 1024 * 1024 ) )
        {
            Init( ShiftJisEncoding.Instance, endianness );
        }

        public EndianBinaryWriter( string filepath, Encoding encoding, Endianness endianness )
            : base( File.Create( filepath, 1024 * 1024 ) )
        {
            Init( encoding, endianness );
        }

        public EndianBinaryWriter( Stream input, Encoding encoding, Endianness endianness )
            : base( input, encoding )
        {
            Init( encoding, endianness );
        }

        public EndianBinaryWriter( Stream input, bool leaveOpen, Endianness endianness ) : this( input, Encoding.Default, leaveOpen, endianness ) { }

        public EndianBinaryWriter( Stream input, Encoding encoding, bool leaveOpen, Endianness endianness )
            : base( input, encoding, leaveOpen )
        {
            Init( encoding, endianness );
        }

        private void Init( Encoding encoding, Endianness endianness )
        {
            Endianness = endianness;
            Encoding = encoding;
        }

        [DebuggerStepThrough, MethodImpl( MethodImplOptions.AggressiveInlining )]
        public void Seek( long offset, SeekOrigin origin )
        {
            BaseStream.Seek( offset, origin );
        }

        [DebuggerStepThrough, MethodImpl( MethodImplOptions.AggressiveInlining )]
        public void SeekBegin( long offset )
        {
            BaseStream.Seek( offset, SeekOrigin.Begin );
        }

        [DebuggerStepThrough, MethodImpl( MethodImplOptions.AggressiveInlining )]
        public void SeekCurrent( long offset )
        {
            BaseStream.Seek( offset, SeekOrigin.Current );
        }

        [DebuggerStepThrough, MethodImpl( MethodImplOptions.AggressiveInlining )]
        public void SeekEnd( long offset )
        {
            BaseStream.Seek( offset, SeekOrigin.End );
        }

        [DebuggerStepThrough, MethodImpl( MethodImplOptions.AggressiveInlining )]
        public void Align( int alignment )
        {
            WritePadding( AlignmentHelper.GetAlignedDifference( Position, alignment ) );
        }

        [DebuggerStepThrough, MethodImpl( MethodImplOptions.AggressiveInlining )]
        public void Align() => Align( DefaultAlignment );

        [DebuggerStepThrough, MethodImpl( MethodImplOptions.AggressiveInlining )]
        public void Write( IEnumerable<byte> values )
        {
            foreach ( byte t in values )
                Write( t );
        }

        [DebuggerStepThrough, MethodImpl( MethodImplOptions.AggressiveInlining )]
        public void Write( IEnumerable<sbyte> values )
        {
            foreach ( sbyte t in values )
                Write( t );
        }

        [DebuggerStepThrough, MethodImpl( MethodImplOptions.AggressiveInlining )]
        public void Write( IEnumerable<bool> values )
        {
            foreach ( bool t in values )
                Write( t );
        }

        [DebuggerStepThrough, MethodImpl( MethodImplOptions.AggressiveInlining )]
        public override void Write( short value )
        {
            base.Write( SwapBytes ? EndiannessHelper.Swap( value ) : value );
        }

        [DebuggerStepThrough, MethodImpl( MethodImplOptions.AggressiveInlining )]
        public void Write( IEnumerable<short> values )
        {
            foreach ( var value in values )
                Write( value );
        }

        [DebuggerStepThrough, MethodImpl( MethodImplOptions.AggressiveInlining )]
        public override void Write( ushort value )
        {
            base.Write( SwapBytes ? EndiannessHelper.Swap( value ) : value );
        }

        [DebuggerStepThrough, MethodImpl( MethodImplOptions.AggressiveInlining )]
        public void Write( IEnumerable<ushort> values )
        {
            foreach ( var value in values )
                Write( value );
        }

        [DebuggerStepThrough, MethodImpl( MethodImplOptions.AggressiveInlining )]
        public override void Write( int value )
        {
            base.Write( SwapBytes ? EndiannessHelper.Swap( value ) : value );
        }

        [DebuggerStepThrough, MethodImpl( MethodImplOptions.AggressiveInlining )]
        public void Write( IEnumerable<int> values )
        {
            foreach ( var value in values )
                Write( value );
        }

        [DebuggerStepThrough, MethodImpl( MethodImplOptions.AggressiveInlining )]
        public override void Write( uint value )
        {
            base.Write( SwapBytes ? EndiannessHelper.Swap( value ) : value );
        }

        [DebuggerStepThrough, MethodImpl( MethodImplOptions.AggressiveInlining )]
        public void Write( IEnumerable<uint> values )
        {
            foreach ( var value in values )
                Write( value );
        }

        [DebuggerStepThrough, MethodImpl( MethodImplOptions.AggressiveInlining )]
        public override void Write( long value )
        {
            base.Write( SwapBytes ? EndiannessHelper.Swap( value ) : value );
        }

        [DebuggerStepThrough, MethodImpl( MethodImplOptions.AggressiveInlining )]
        public void Write( IEnumerable<long> values )
        {
            foreach ( var value in values )
                Write( value );
        }

        [DebuggerStepThrough, MethodImpl( MethodImplOptions.AggressiveInlining )]
        public override void Write( ulong value )
        {
            base.Write( SwapBytes ? EndiannessHelper.Swap( value ) : value );
        }

        [DebuggerStepThrough, MethodImpl( MethodImplOptions.AggressiveInlining )]
        public void Write( IEnumerable<ulong> values )
        {
            foreach ( var value in values )
                Write( value );
        }

        [DebuggerStepThrough, MethodImpl( MethodImplOptions.AggressiveInlining )]
        public void WriteHalf( float value ) => Write( FloatHelper.ToHalf( value ) );

        [DebuggerStepThrough, MethodImpl( MethodImplOptions.AggressiveInlining )]
        public override void Write( float value )
        {
            base.Write( SwapBytes ? EndiannessHelper.Swap( value ) : value );
        }

        [DebuggerStepThrough, MethodImpl( MethodImplOptions.AggressiveInlining )]
        public void Write( IEnumerable<float> values )
        {
            foreach ( var value in values )
                Write( value );
        }

        [DebuggerStepThrough, MethodImpl( MethodImplOptions.AggressiveInlining )]
        public override void Write( decimal value )
        {
            base.Write( SwapBytes ? EndiannessHelper.Swap( value ) : value );
        }

        [DebuggerStepThrough, MethodImpl( MethodImplOptions.AggressiveInlining )]
        public void Write( IEnumerable<decimal> values )
        {
            foreach ( var value in values )
                Write( value );
        }

        [DebuggerStepThrough, MethodImpl( MethodImplOptions.AggressiveInlining )]
        public void WriteColor( Color color )
        {
            Write( color.R );
            Write( color.G );
            Write( color.B );
            Write( color.A );
        }

        [DebuggerStepThrough, MethodImpl( MethodImplOptions.AggressiveInlining )]
        public void WriteColors( IEnumerable<Color> values )
        {
            foreach ( var value in values )
                WriteColor( value );
        }

        [DebuggerStepThrough, MethodImpl( MethodImplOptions.AggressiveInlining )]
        public void Write( string value, StringBinaryFormat format, int fixedLength = -1 )
        {
            if ( value == null )
                value = string.Empty;

            switch ( format )
            {
                case StringBinaryFormat.NullTerminated:
                    {
                        Write( Encoding.GetBytes( value ) );

                        for ( int i = 0; i < mEncodingMinByteCount; i++ )
                            Write( ( byte )0 );
                    }
                    break;
                case StringBinaryFormat.FixedLength:
                    {
                        if ( fixedLength == -1 )
                        {
                            throw new ArgumentException( "Fixed length must be provided if format is set to fixed length", nameof( fixedLength ) );
                        }

                        var bytes = Encoding.GetBytes( value );
                        var bytesToWrite = Math.Min( bytes.Length, fixedLength );
                        for ( int i = 0; i < bytesToWrite; i++ )
                            Write( bytes[ i ] );

                        fixedLength -= bytesToWrite;

                        while ( fixedLength-- > 0 )
                            Write( ( byte )0 );
                    }
                    break;

                case StringBinaryFormat.PrefixedLength8:
                    {
                        Write( ( byte )value.Length );
                        Write( Encoding.GetBytes( value ) );
                    }
                    break;

                case StringBinaryFormat.PrefixedLength16:
                    {
                        Write( ( ushort )value.Length );
                        Write( Encoding.GetBytes( value ) );
                    }
                    break;

                case StringBinaryFormat.PrefixedLength32:
                    {
                        Write( ( uint )value.Length );
                        Write( Encoding.GetBytes( value ) );
                    }
                    break;

                default:
                    throw new ArgumentException( "Invalid format specified", nameof( format ) );
            }
        }

        public void WritePadding( int count )
        {
            for ( int i = 0; i < count / 8; i++ )
                Write( 0L );

            for ( int i = 0; i < count % 8; i++ )
                Write( ( byte )0 );
        }
    }
}
