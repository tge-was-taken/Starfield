using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Starfield.IO.Serialization
{
    public class ObjectBinaryReader : EndianBinaryReader
    {
        private Dictionary<long, object> mObjectLookup;
        private Stack<long>              mBaseOffsetStack;

        public long BaseOffset => mBaseOffsetStack.Peek();

        public ObjectBinaryReader( Stream input, Endianness endianness ) : 
            base( input, endianness )
            => Init();

        public ObjectBinaryReader( Stream input, string fileName, Endianness endianness ) : 
            base( input, fileName, endianness )
            => Init();

        public ObjectBinaryReader( string filepath, Endianness endianness ) : 
            base( filepath, endianness )
            => Init();

        public ObjectBinaryReader( Stream input, Encoding encoding, Endianness endianness ) : 
            base( input, encoding, endianness )
            => Init();

        public ObjectBinaryReader( Stream input, bool leaveOpen, Endianness endianness ) : 
            base( input, leaveOpen, endianness )
            => Init();

        public ObjectBinaryReader( Stream input, Encoding encoding, bool leaveOpen, Endianness endianness ) : 
            base( input, encoding, leaveOpen, endianness )
            => Init();

        public ObjectBinaryReader( Stream input, string fileName, Encoding encoding, bool leaveOpen, Endianness endianness ) :
            base( input, fileName, encoding, leaveOpen, endianness )
            => Init();

        private void Init()
        {
            mBaseOffsetStack = new Stack<long>();
            mBaseOffsetStack.Push( 0 );
            mObjectLookup = new Dictionary<long, object> { [0] = null };
        }

        public void PushBaseOffset() => mBaseOffsetStack.Push( Position );

        public void PushBaseOffset( long position ) => mBaseOffsetStack.Push( position );

        public void PopBaseOffset() => mBaseOffsetStack.Pop();

        public bool IsValidOffset( int offset )
        {
            if ( offset == 0 )
                return true;

            if ( ( offset % 4 ) != 0 )
                return false;

            var effectiveOffset = offset + BaseOffset;
            return offset >= 0 && effectiveOffset >= 0 && effectiveOffset <= Length;
        }

        public void ReadOffset( Action action )
        {
            var offset = ReadInt32();
            if ( offset != 0 )
            {
                long current = Position;
                SeekBegin( offset + BaseOffset );
                action();
                SeekBegin( current );
            }
        }

        public void ReadOffset( int count, Action<int> action )
        {
            ReadOffset( () =>
            {
                for ( var i = 0; i < count; ++i )
                    action( i );
            } );
        }

        public void ReadAtOffset( long offset, Action action )
        {
            if ( offset == 0 )
                return;

            long current = Position;
            SeekBegin( offset + BaseOffset );
            action();
            SeekBegin( current );
        }

        public void ReadAtOffset( long offset, int count, Action<int> action )
        {
            if ( offset == 0 )
                return;

            ReadAtOffset( offset, () =>
            {
                for ( var i = 0; i < count; ++i )
                    action( i );
            } );
        }

        public void ReadAtOffset<T>( long offset, int count, List<T> list, object context = null ) where T : IBinarySerializable, new()
        {
            if ( offset == 0 )
                return;

            ReadAtOffset( offset, () =>
            {
                for ( var i = 0; i < count; ++i )
                {
                    var item = new T();
                    item.Read( this, context );
                    list.Add( item );
                }
            } );
        }

        public string ReadStringAtOffset( long offset, StringBinaryFormat format, int fixedLength = -1 )
        {
            if ( offset == 0 )
                return null;

            string str = null;
            ReadAtOffset( offset, () => str = ReadString( format, fixedLength ) );
            return str;
        }

        public string ReadStringOffset( StringBinaryFormat format = StringBinaryFormat.NullTerminated, int fixedLength = -1 )
        {
            var offset = ReadInt32();
            if ( offset == 0 )
                return null;

            return ReadStringAtOffset( offset, format, fixedLength );
        }

        public string[] ReadStringListAtOffset( long offset, int count, StringBinaryFormat format, int fixedLength = -1 )
        {
            string[] str = null;
            ReadAtOffset( offset, () => str = ReadStrings( count, format, fixedLength ) );
            return str;
        }

        public T ReadObject<T>( object context = null ) where T : IBinarySerializable, new()
        {
            var obj = new T
            {
                SourceInfo = new BinarySourceInfo( FileName, Position, Endianness )
            };

            obj.Read( this, context );
            return obj;
        }

        public List<T> ReadObjectListOffset<T>( int count, object context = null ) where T : IBinarySerializable, new()
        {
            List<T> list = null;
            ReadOffset( () => { list = ReadObjectList<T>( count, context ); } );
            return list;
        }

        public List<T> ReadObjectList<T>( int count, object context = null ) where T : IBinarySerializable, new()
        {
            var list = new List<T>( count );
            for ( int i = 0; i < count; i++ )
            {
                list.Add( ReadObject<T>( context ) );
            }

            return list;
        }

        public List<T> ReadObjectOffsetList<T>( int count, object context = null ) where T : IBinarySerializable, new()
        {
            var list = new List<T>( count );
            for ( int i = 0; i < count; i++ )
            {
                list.Add( ReadObjectOffset<T>( context ) );
            }

            return list;
        }

        /// <summary>
        /// Reads an object of type <typeparamref name="T"/> from the given relative offset if it is not in the object cache.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="offset"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public T ReadObjectAtOffset<T>( long offset, object context = null ) where T : IBinarySerializable, new()
        {
            object obj = null;
            var effectiveOffset = offset + BaseOffset;

            if ( offset != 0 && !mObjectLookup.TryGetValue( effectiveOffset, out obj ) )
            {
                long current = Position;
                SeekBegin( effectiveOffset );
                obj = ReadObject<T>( context );
                SeekBegin( current );
                mObjectLookup[effectiveOffset] = obj;
            }

            return ( T )obj;
        }

        /// <summary>
        /// Reads an object offset from the current stream and reads the object of type <typeparamref name="T"/> at the given offset, if it is not in the object cache.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <returns></returns>
        public T ReadObjectOffset<T>( object context = null ) where T : IBinarySerializable, new()
        {
            var offset = ReadInt32();
            if ( offset == 0 )
                return default( T );

            return ReadObjectAtOffset<T>( offset, context );
        }
    }
}
