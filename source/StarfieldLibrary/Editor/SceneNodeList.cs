using System;
using System.Collections;
using System.Collections.Generic;

namespace Starfield.Editor
{
    public class SceneNodeList : IList<SceneNode>
    {
        private readonly List<SceneNode> mList;
        private readonly SceneNode       mParent;

        public SceneNodeList( SceneNode parent )
        {
            mList   = new List<SceneNode>();
            mParent = parent;
        }

        public IEnumerator<SceneNode> GetEnumerator()
        {
            return mList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ( ( IEnumerable ) mList ).GetEnumerator();
        }

        public void Add( SceneNode item )
        {
            if ( item == null )
                throw new ArgumentNullException( nameof( item ) );

            mList.Add( item );
            UpdateParent( item );
        }

        public void Clear()
        {
            foreach ( var node in mList )
                node.Parent = null;

            mList.Clear();
        }

        public bool Contains( SceneNode item )
        {
            return mList.Contains( item );
        }

        public void CopyTo( SceneNode[] array, int arrayIndex )
        {
            mList.CopyTo( array, arrayIndex );

            foreach ( var node in array )
                node.Parent = mParent;
        }

        public bool Remove( SceneNode item )
        {
            if ( item == null )
                throw new ArgumentNullException( nameof( item ) );

            var removed = mList.Remove( item );
            if ( removed )
                item.Parent = null;

            return removed;
        }

        public int Count => mList.Count;

        public bool IsReadOnly => false;

        public int IndexOf( SceneNode item )
        {
            return mList.IndexOf( item );
        }

        public void Insert( int index, SceneNode item )
        {
            if ( item == null )
                throw new ArgumentNullException( nameof( item ) );

            mList.Insert( index, item );
            item.Parent = mParent;
        }

        public void RemoveAt( int index )
        {
            var node = mList[ index ];
            mList.RemoveAt( index );
            node.Parent = null;
        }

        public SceneNode this[ int index ]
        {
            get => mList[ index ];
            set
            {
                mList[index] = value;
                UpdateParent( value );
            }
        }

        private void UpdateParent( SceneNode node )
        {
            if ( node.Parent != mParent )
                node.Parent?.Children.Remove( node );

            node.Parent = mParent;
        }
    }
}