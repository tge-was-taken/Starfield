using System;
using System.IO;

namespace Starfield.IO
{
    public class FileManager
    {
        private static readonly string sBasePath = Path.GetFullPath( Path.Combine( AppDomain.CurrentDomain.BaseDirectory, "res" ) );

        private static FileManager sInstance;
        public static FileManager Instance => sInstance ?? ( sInstance = new FileManager() );

        public Stream OpenFile( string path, FileMode mode )
        {
            return new FileStream( MakeFullPath( path ), mode );
        }

        public Stream OpenFile( string path ) => OpenFile( path, FileMode.Open );

        public StreamReader OpenText( string path ) => new StreamReader( OpenFile( path ) );

        public Stream CreateFile( string path ) => OpenFile( path, FileMode.Create );

        public StreamWriter CreateText( string path ) => new StreamWriter( CreateFile( path ) );

        public string ReadAllText( string path )
        {
            using ( var reader = OpenText( path ) )
                return reader.ReadToEnd();
        }

        public void WriteAllText( string path, string text )
        {
            using ( var writer = CreateText( path ) )
                writer.Write( text );
        }

        public bool Exists( string path )
        {
            var fullPath = MakeFullPath( path );
            return File.Exists( fullPath ) || Directory.Exists( path );
        }

        private string MakeFullPath( string path )
            => Path.Combine( sBasePath, path );
    }
}
