using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace Starfield.IO.Serialization
{
    public class ObjectBinaryWriter : EndianBinaryWriter
    {
        internal class ScheduledWrite
        {
            public long Position { get; }

            public long BaseOffset { get; }

            public Func<long> Action { get; }

            public int Priority { get; }

            public object Object { get; }

            public ScheduledWrite( long position, long baseOffset, Func<long> action, int priority, object obj )
            {
                Position   = position;
                BaseOffset = baseOffset;
                Action     = action;
                Priority   = priority;
                Object     = obj;
            }
        }

        private Stack<long> mBaseOffsetStack;
        private LinkedList<ScheduledWrite> mScheduledWrites;
        private LinkedList<long>           mScheduledFileSizeWrites;
        private List<long>                 mOffsetPositions;
        private Dictionary<object, long>   mObjectLookup;

        public long BaseOffset => mBaseOffsetStack.Peek();

        public bool WriteEmptyLists { get; set; } = true;

        public IList<long> OffsetPositions => mOffsetPositions;


        public ObjectBinaryWriter( Stream input, Endianness endianness ) : base( input, endianness )
        {
            Init();
        }

        public ObjectBinaryWriter( string filepath, Endianness endianness ) : base( filepath, endianness )
        {
            Init();
        }

        public ObjectBinaryWriter( string filepath, Encoding encoding, Endianness endianness ) : base( filepath, encoding, endianness )
        {
            Init();
        }

        public ObjectBinaryWriter( Stream input, Encoding encoding, Endianness endianness ) : base( input, encoding, endianness )
        {
            Init();
        }

        public ObjectBinaryWriter( Stream input, bool leaveOpen, Endianness endianness ) : base( input, leaveOpen, endianness )
        {
            Init();
        }

        public ObjectBinaryWriter( Stream input, Encoding encoding, bool leaveOpen, Endianness endianness ) : base( input, encoding, leaveOpen, endianness )
        {
            Init();
        }

        private void Init()
        {
            mBaseOffsetStack = new Stack<long>();
            mBaseOffsetStack.Push( 0 );
            mScheduledWrites         = new LinkedList<ScheduledWrite>();
            mScheduledFileSizeWrites = new LinkedList<long>();
            mOffsetPositions         = new List<long>();
            mObjectLookup            = new Dictionary<object, long>();
        }

        [DebuggerStepThrough, MethodImpl( MethodImplOptions.AggressiveInlining )]
        public void PushBaseOffset() => mBaseOffsetStack.Push( Position );

        [DebuggerStepThrough, MethodImpl( MethodImplOptions.AggressiveInlining )]
        public void PushBaseOffset( long position ) => mBaseOffsetStack.Push( position );

        [DebuggerStepThrough, MethodImpl( MethodImplOptions.AggressiveInlining )]
        public void PopBaseOffset() => mBaseOffsetStack.Pop();


        [DebuggerStepThrough, MethodImpl( MethodImplOptions.AggressiveInlining )]
        public void WriteObject<T>( T obj, object context = null ) where T : IBinarySerializable
        {
            obj.Write( this, context );
        }

        [DebuggerStepThrough, MethodImpl( MethodImplOptions.AggressiveInlining )]
        public void WriteObjects<T>( IEnumerable<T> collection, object context = null ) where T : IBinarySerializable
        {
            foreach ( var obj in collection )
                obj.Write( this, context );
        }


        public void ScheduleWriteOffset( Action action ) => ScheduleWriteOffsetAligned( 0, DefaultAlignment, action );

        public void ScheduleWriteOffset( int priority, Action action ) => ScheduleWriteOffsetAligned( priority, DefaultAlignment, action );

        public void ScheduleWriteOffsetAligned( int alignment, Action action ) => ScheduleWriteOffsetAligned( 0, alignment, action );

        public void ScheduleWriteOffsetAligned( int priority, int alignment, Action action )
        {
            ScheduleWriteOffset( priority, null, () =>
            {
                Align( alignment );
                long offset = BaseStream.Position;
                action();
                return offset;
            } );
        }

        public void ScheduleWriteObjectListOffset<T>( IEnumerable<T> list, object context = null ) where T : IBinarySerializable
            => ScheduleWriteObjectListOffsetAligned( list, DefaultAlignment, context );

        public void ScheduleWriteObjectListOffsetAligned<T>( IEnumerable<T> list, int alignment, object context = null ) where T : IBinarySerializable
        {
            if ( list == null )
            {
                Write( 0 );
            }
            else
            {
                ScheduleWriteOffset( 0, list, () =>
                {
                    Align( alignment );
                    long current = BaseStream.Position;
                    WriteObjects( list, context );
                    return current;
                } );
            }
        }

        public void ScheduleWriteObjectOffset<T>( T obj, object context = null ) where T : IBinarySerializable =>
            ScheduleWriteObjectOffsetAligned( obj, DefaultAlignment, context );

        public void ScheduleWriteObjectOffsetAligned<T>( T obj, int alignment, object context = null ) where T : IBinarySerializable
        {
            if ( obj == null )
            {
                Write( 0 );
            }
            else
            {
                ScheduleWriteOffset( 0, obj, () =>
                {
                    Align( alignment );
                    long current = BaseStream.Position;
                    obj.Write( this, context );
                    return current;
                } );
            }
        }

        public void ScheduleWriteStringOffset( string obj )
            => ScheduleWriteStringOffsetAligned( obj, DefaultAlignment );

        public void ScheduleWriteStringOffsetAligned( string obj, int alignment )
        {
            if ( obj == null )
            {
                Write( 0 );
            }
            else
            {
                ScheduleWriteOffset( 0, obj, () =>
                {
                    Align( alignment );
                    long current = BaseStream.Position;
                    Write( obj, StringBinaryFormat.NullTerminated );
                    return current;
                } );
            }
        }

        public void ScheduleWriteObjectOffset<T>( T obj, Action<T> action )
            => ScheduleWriteObjectOffsetAligned( obj, DefaultAlignment, action );

        public void ScheduleWriteObjectOffsetAligned<T>( T obj, int alignment, Action<T> action )
        {
            if ( obj == null )
            {
                Write( 0 );
            }
            else
            {
                ScheduleWriteOffset( 0, obj, () =>
                {
                    Align( alignment );
                    long current = BaseStream.Position;
                    action( obj );
                    return current;
                } );
            }
        }

        public int ScheduleWriteListOffset<T>( IList<T> list, Action<T> write )
            => ScheduleWriteListOffsetAligned( list, DefaultAlignment, write );

        public int ScheduleWriteListOffsetAligned<T>( IList<T> list, int alignment, Action<T> write )
        {
            var count = 0;
            if ( list != null && ( WriteEmptyLists || list.Count != 0 ) )
            {
                count = list.Count;
                ScheduleWriteOffset( 0, list, () =>
                {
                    Align( alignment );
                    var offset = BaseStream.Position;

                    for ( int i = 0; i < list.Count; i++ )
                        write( list[i] );

                    return offset;
                } );
            }
            else
            {
                Write( 0 );
            }

            return count;
        }

        public int ScheduleWriteListOffset<T>( IList<T> list, object context = null ) where T : IBinarySerializable
            => ScheduleWriteListOffsetAligned( list, DefaultAlignment, context );

        public int ScheduleWriteListOffsetAligned<T>( IList<T> list, int alignment, object context = null ) where T : IBinarySerializable
        {
            var count = 0;
            if ( list != null && ( WriteEmptyLists || list.Count != 0 ) )
            {
                count = list.Count;
                ScheduleWriteOffset( 0, list, () =>
                {
                    Align( alignment );
                    long offset = BaseStream.Position;
                    foreach ( var t in list )
                        t.Write( this, context );

                    return offset;
                } );
            }
            else
            {
                Write( 0 );
            }

            return count;
        }

        public void ScheduleWriteObjectOffsets<T>( IEnumerable<T> list, object context = null ) where T : IBinarySerializable
            => ScheduleWriteObjectOffsetsAligned( list, DefaultAlignment, context );

        public void ScheduleWriteObjectOffsetsAligned<T>( IEnumerable<T> list, int alignment, object context = null ) where T : IBinarySerializable
        {
            foreach ( var obj in list )
                ScheduleWriteObjectOffsetAligned( obj, alignment, context );
        }

        public void ScheduleWriteFileSize()
        {
            mScheduledFileSizeWrites.AddLast( Position );
            Write( 0 );
        }

        public void PerformScheduledWrites()
        {
            DoScheduledOffsetWrites();
            DoScheduledFileSizeWrites();
        }

        protected override void Dispose( bool disposing )
        {
            if ( disposing )
                PerformScheduledWrites();

            base.Dispose( disposing );
        }

        private void DoScheduledOffsetWrites()
        {
            int curPriority = 0;

            while ( mScheduledWrites.Count > 0 )
            {
                var anyWritesDone = false;
                var current = mScheduledWrites.First;

                while ( current != null )
                {
                    var next = current.Next;

                    if ( current.Value.Priority == curPriority )
                    {
                        DoScheduledWrite( current.Value );
                        mScheduledWrites.Remove( current );
                        anyWritesDone = true;
                    }

                    current = next;
                }

                if ( anyWritesDone )
                    ++curPriority;
                else
                    --curPriority;
            }
        }

        private void DoScheduledFileSizeWrites()
        {
            var current = mScheduledFileSizeWrites.First;
            while ( current != null )
            {
                SeekBegin( current.Value );
                Write( ( int )Length );
                current = current.Next;
            }

            mScheduledFileSizeWrites.Clear();
        }

        private void DoScheduledWrite( ScheduledWrite scheduledWrite )
        {
            long offsetPosition = scheduledWrite.Position;
            mOffsetPositions.Add( offsetPosition );

            long offset;
            if ( scheduledWrite.Object == null )
            {
                offset = scheduledWrite.Action();
            }
            else if ( !mObjectLookup.TryGetValue( scheduledWrite.Object, out offset ) ) // Try to fetch the object offset from the cache
            {
                // Object not in cache, so lets write it.

                // Write object
                offset = scheduledWrite.Action();

                // Add to lookup
                mObjectLookup[scheduledWrite.Object] = offset;
            }

            var relativeOffset = offset - scheduledWrite.BaseOffset;

            // Write offset
            long returnPos = BaseStream.Position;
            BaseStream.Seek( offsetPosition, SeekOrigin.Begin );
            Write( ( int )relativeOffset );

            // Seek back for next one
            BaseStream.Seek( returnPos, SeekOrigin.Begin );
        }

        private void ScheduleWriteOffset( int priority, object obj, Func<long> action )
        {
            mScheduledWrites.AddLast( new ScheduledWrite( BaseStream.Position, BaseOffset, action, priority, obj ) );
            Write( 0 );
        }

    }
}
