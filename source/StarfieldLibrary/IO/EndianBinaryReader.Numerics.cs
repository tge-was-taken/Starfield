using System.Collections.Generic;
using System.Numerics;

namespace Starfield.IO
{
    public partial class EndianBinaryReader
    {
        public Vector2 ReadVector2()
        {
            return new Vector2( ReadSingle(), ReadSingle() );
        }

        public Vector2[] ReadVector2s( int count )
        {
            var array = new Vector2[count];
            for ( var i = 0; i < array.Length; i++ )
                array[i] = ReadVector2();

            return array;
        }

        public Vector3 ReadVector3()
        {
            return new Vector3( ReadSingle(), ReadSingle(), ReadSingle() );
        }

        public Vector3[] ReadVector3s( int count )
        {
            var array = new Vector3[count];
            for ( var i = 0; i < array.Length; i++ )
                array[i] = ReadVector3();

            return array;
        }

        public Vector4 ReadVector4()
        {
            return new Vector4( ReadSingle(), ReadSingle(), ReadSingle(), ReadSingle() );
        }

        public Quaternion ReadQuaternion()
        {
            return new Quaternion( ReadSingle(), ReadSingle(), ReadSingle(), ReadSingle() );
        }

        public Vector4[] ReadVector4Array( int count )
        {
            var array = new Vector4[count];
            for ( var i = 0; i < array.Length; i++ )
                array[i] = ReadVector4();

            return array;
        }

        public List<Vector4> ReadVector4List( int count )
        {
            var list = new List<Vector4>( count );
            for ( var i = 0; i < count; i++ )
                list.Add( ReadVector4() );

            return list;
        }

        public Matrix4x4 ReadMatrix4x4()
        {
            return new Matrix4x4( ReadSingle(), ReadSingle(), ReadSingle(), ReadSingle(),
                                  ReadSingle(), ReadSingle(), ReadSingle(), ReadSingle(),
                                  ReadSingle(), ReadSingle(), ReadSingle(), ReadSingle(),
                                  ReadSingle(), ReadSingle(), ReadSingle(), ReadSingle() );
        }

        public List<Matrix4x4> ReadMatrix4x4List( int count )
        {
            var list = new List<Matrix4x4>( count );
            for ( var i = 0; i < count; i++ )
                list.Add( ReadMatrix4x4() );

            return list;
        }
    }
}
