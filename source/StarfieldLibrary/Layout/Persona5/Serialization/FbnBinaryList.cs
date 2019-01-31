using System.Collections;
using System.Collections.Generic;

namespace Starfield.Layout.Persona5.Serialization
{
    public class FbnBinaryList<T> : FbnBinaryChunk, IList<T> where T : IFbnListEntry
    {
        private readonly List<T> mList;

        public FbnBinaryList( FbnBinaryChunkType type, int field04 ) : base( type, field04 )
        {
            mList = new List<T>();
        }

        public T this[int index] { get => mList[index]; set => mList[index] = value; }

        public int Count => mList.Count;

        public bool IsReadOnly => false;

        public void Add( T item )
        {
            mList.Add( item );
        }

        public void Clear()
        {
            mList.Clear();
        }

        public bool Contains( T item )
        {
            return mList.Contains( item );
        }

        public void CopyTo( T[] array, int arrayIndex )
        {
            mList.CopyTo( array, arrayIndex );
        }

        public IEnumerator<T> GetEnumerator()
        {
            return mList.GetEnumerator();
        }

        public int IndexOf( T item )
        {
            return mList.IndexOf( item );
        }

        public void Insert( int index, T item )
        {
            mList.Insert( index, item );
        }

        public bool Remove( T item )
        {
            return mList.Remove( item );
        }

        public void RemoveAt( int index )
        {
            mList.RemoveAt( index );
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return mList.GetEnumerator();
        }
    }
}