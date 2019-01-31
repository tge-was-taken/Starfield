using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AtlusFileSystemLibrary;
using AtlusFileSystemLibrary.Common.IO;
using AtlusFileSystemLibrary.FileSystems.PAK;
using AtlusScriptLibrary.FlowScriptLanguage.BinaryModel;
using Starfield.Layout.Persona5.Serialization;

namespace Starfield.Layout.Persona5
{
    public class FieldLocalData
    {
        public CmrBinary DefaultCamera { get; set; }

        public List<FieldLayer> Layers { get; set; }

        public PcdBinary[] Pcds { get; set; } = new PcdBinary[10];

        public FpaBinary Fpa { get; set; }

        public ChdBinary Culling { get; set; }

        public FelBinary Enemies { get; set; }

        public FlowScriptBinary InitScript { get; set; }

        public MapBinary Map { get; set; }

        public OblBinary Objects { get; set; }

        public HtbBinary ObjectHitTable { get; set; }

        public FlowScriptBinary ObjectHitScript { get; set; }

        public ShtBinary Sht { get; set; }

        public TblBinary Tbl { get; set; }

        public Dictionary<string, Stream> UncategorizedFiles { get; }

        public Dictionary<string, Stream> UncategorizedNpcFiles { get; }

        private readonly HashSet<string> mLoadedFiles;

        public FieldLocalData()
        {
            Layers                = new List<FieldLayer>();
            UncategorizedFiles    = new Dictionary<string, Stream>();
            UncategorizedNpcFiles = new Dictionary<string, Stream>();
            mLoadedFiles          = new HashSet<string>();
        }

        public FieldLocalData( string directoryPath, int major, int minor )
            : this()
        {
            var fieldPak    = LoadFieldPak( directoryPath, major, minor, "f{0:D3}_{1:D3}.pac" );
            var fieldNpcPak = LoadFieldPak( directoryPath, major, minor, "fnpc{0:D3}_{1:D3}.pac" );

            DefaultCamera = TryLoadFile( fieldPak, $"data/f{major:D3}_{minor:D3}.CMR", stream => new CmrBinary( stream, true ) );

            // Load layers
            for ( int layerIndex = 0; true; layerIndex++ )
            {
                var layer = new FieldLayer();
                layer.ObjectPlacement = TryLoadFile( fieldPak,    $"data/f{major:D3}_{minor:D3}_{layerIndex:D2}.FBN",   stream => new FbnBinary( stream, true ) );
                layer.HitTable        = TryLoadFile( fieldPak,    $"hit/f{major:D3}_{minor:D3}_{layerIndex:D2}.HTB",    stream => new HtbBinary( stream, true ) );
                layer.HitScript       = TryLoadFile( fieldPak,    $"hit/fhit_{major:D3}_{minor:D3}_{layerIndex:D2}.bf", stream => FlowScriptBinary.FromStream( stream, true ) );
                layer.NpcScript       = TryLoadFile( fieldNpcPak, $"npc/fnpc{major:D3}_{minor:D3}_{layerIndex:D2}.bf",  stream => FlowScriptBinary.FromStream( stream, true ) );
                //layer.Fnt = TryLoadFile( fieldNpcPak, $"npc/fnt{major:D3}_{minor:D3}_{layerIndex:D2}.bf", stream => new FntBinary( stream, true ) );
                //layer.Fpt = TryLoadFile( fieldNpcPak, $"npc/fpt{major:D3}_{minor:D3}_{layerIndex:D2}.bf", stream => new FptBinary( stream, true ) );

                if ( layer.ObjectPlacement == null &&
                     layer.HitTable == null &&
                     layer.HitScript == null &&
                     layer.NpcScript == null &&
                     layer.Fnt == null &&
                     layer.Fpt == null )
                {
                    // Layer was empty, so assume there are no more layers
                    break;
                }

                Layers.Add( layer );
            }

            //for ( int i = 1; i < 9; i++ )
            //    Pcds[ i ] = TryLoadFile( fieldPak, $"data/f{major:D3}_{minor:D3}_{i:D3}.PCD", stream => new PcdBinary( stream, true ) );

            //Fpa = TryLoadFile( fieldPak, $"data/f{major:D3}_{minor:D3}.FPA", stream => new FpaBinary( stream, true ) );
            //Culling = TryLoadFile( fieldPak, $"culling/f{major:D3}_{minor:D3}.CHD", stream => new ChdBinary( stream, true ) );
            //Enemies = TryLoadFile( fieldPak, $"enemy/f{major:D3}_{minor:D3}.FEL", stream => new FelBinary( stream, true ) );
            InitScript = TryLoadFile( fieldPak, $"init/fini_{major:D3}_{minor:D3}.bf", stream => FlowScriptBinary.FromStream( stream, true ) );
            //Map = TryLoadFile( fieldPak, $"map/d{major:D3}_{minor:D3}.map", stream => new MapBinary( stream, true ) );
            //Objects = TryLoadFile( fieldPak, $"map/d{major:D3}_{minor:D3}.OBL", stream => new OblBinary( stream, true ) );
            ObjectHitTable  = TryLoadFile( fieldPak, $"object_hit/f{major:D3}_{minor:D3}.HTB", stream => new HtbBinary( stream, true ) );
            ObjectHitScript = TryLoadFile( fieldPak, $"object_hit/fhit{major:D3}_{minor:D3}.bf",               stream => FlowScriptBinary.FromStream( stream, true ) );
            //Sht = TryLoadFile( fieldPak, $"sht/f{major:D3}_{minor:D3}.SHT", stream => new ShtBinary( stream, true ) );
            //Tbl = TryLoadFile( fieldPak, $"fext{major:D3}_{minor:D3}.tbl", stream => new TblBinary( stream, true ) );

            AddUncategorizedFiles( fieldPak,    UncategorizedFiles );

            if ( fieldNpcPak != null )
                AddUncategorizedFiles( fieldNpcPak, UncategorizedNpcFiles );
        }

        public void Save( string directoryPath, int major, int minor )
        {
            Directory.CreateDirectory( directoryPath );

            using ( var fieldPak = new PAKFileSystem( FormatVersion.Version1 ) )
            {
                TryAddFile( fieldPak, DefaultCamera, $"data/f{major:D3}_{minor:D3}.CMR", o => o.Save() );

                // FBN
                for ( var i = 0; i < Layers.Count; i++ )
                    TryAddFile( fieldPak, Layers[i].ObjectPlacement, $"data/f{major:D3}_{minor:D3}_{i:D2}.FBN", o => o.Save() );

                // FPA
                //TryAddFile( fieldPak, Fpa, $"data/f{major:D3}_{minor:D3}.FPA", o => o.Save() );
                AddUncategorizedFilesWithExtension( fieldPak, "FPA" );

                // MDT
                //TryAddFile( fieldPak, Mdt, $"data/f{major:D3}_{minor:D3}.MDT", o => o.Save() );
                AddUncategorizedFilesWithExtension( fieldPak, "MDT" );

                // PCD
                //for ( int i = 1; i < 9; i++ )
                //    TryAddFile( fieldPak, Pcds[ i ], $"data/f{major:D3}_{minor:D3}_{i:D3}.PCD", o => o.Save() );
                AddUncategorizedFilesWithExtension( fieldPak, "PCD" );

                for ( int i = 0; i < Layers.Count; i++ )
                    TryAddFile( fieldPak, Layers[i].HitTable, $"hit/f{major:D3}_{minor:D3}_{i:D2}.HTB", o => o.Save() );

                for ( int i = 0; i < Layers.Count; i++ )
                    TryAddFile( fieldPak, Layers[i].HitScript, $"hit/fhit_{major:D3}_{minor:D3}_{i:D2}.bf", o => o.ToStream() );

                TryAddFile( fieldPak, InitScript, $"init/fini_{major:D3}_{minor:D3}.bf", o => o.ToStream() );

                // CLT
                AddUncategorizedFilesWithExtension( fieldPak, "CLT" );

                // SHT
                //TryAddFile( fieldPak, Sht, $"sht/f{major:D3}_{minor:D3}.SHT", o => o.Save() );
                AddUncategorizedFilesWithExtension( fieldPak, "SHT" );

                // TBL
                //TryAddFile( fieldPak, Tbl, $"fext{major:D3}_{minor:D3}.tbl", o => o.Save() );
                AddUncategorizedFilesWithExtension( fieldPak, "tbl" );

                // AWB
                AddUncategorizedFilesWithExtension( fieldPak, "AWB" );

                // Remaining uncategorized files
                foreach ( var file in UncategorizedFiles.Where( x => !fieldPak.Exists( x.Key ) ) )
                    fieldPak.AddFile( file.Key, file.Value, false, ConflictPolicy.Replace );

                fieldPak.Save( Path.Combine( directoryPath, $"f{major:D3}_{minor:D3}.pac" ) );
            }

            using ( var fieldNpcPak = new PAKFileSystem( FormatVersion.Version1 ) )
            {
                // fnpc BF
                for ( var i = 0; i < Layers.Count; i++ )
                    TryAddFile( fieldNpcPak, Layers[ i ].NpcScript, $"npc/fnpc{major:D3}_{minor:D3}_{i:D2}.bf", o => o.ToStream() );

                // FNT
                //for ( var i = 0; i < Layers.Count; i++ )
                //    TryAddFile( fieldNpcPak, Layers[i].Fnt, $"npc/fnt{major:D3}_{minor:D3}_{i:D2}.FNT", o => o.Save() );
                AddUncategorizedFilesWithExtension( fieldNpcPak, "FNT" );

                // FPC
                //for ( var i = 0; i < Layers.Count; i++ )
                //    TryAddFile( fieldNpcPak, Layers[i].Fpc, $"npc/fpc{major:D3}_{minor:D3}_{i:D2}.FPC", o => o.Save() );
                AddUncategorizedFilesWithExtension( fieldNpcPak, "FPC" );

                foreach ( var file in UncategorizedNpcFiles.Where( x => !fieldNpcPak.Exists( x.Key ) ) )
                    fieldNpcPak.AddFile( file.Key, file.Value, false, ConflictPolicy.Replace );

                fieldNpcPak.Save( Path.Combine( directoryPath, $"fnpc{major:D3}_{minor:D3}.pac" ) );
            }
        }

        private void AddUncategorizedFilesWithExtension( PAKFileSystem fieldPak, string ext )
        {
            foreach ( var file in UncategorizedFiles.Where( x => x.Key.EndsWith( ext ) ) )
                fieldPak.AddFile( file.Key, file.Value, false, ConflictPolicy.Replace );
        }

        private void AddUncategorizedFiles( PAKFileSystem fieldPak, Dictionary<string, Stream> dictionary )
        {
            foreach ( var file in fieldPak.EnumerateFiles() )
            {
                if ( !mLoadedFiles.Contains( file ) )
                {
                    using ( var stream = fieldPak.OpenFile( file ) )
                    {
                        var memoryStream = new MemoryStream();
                        stream.FullyCopyTo( memoryStream );
                        memoryStream.Position = 0;
                        dictionary[file] = memoryStream;
                    }
                }
            }
        }

        private static PAKFileSystem LoadFieldPak( string directoryPath, int major, int minor, string fileNameFormat )
        {
            PAKFileSystem pak = null;

            var pakPath = Path.Combine( directoryPath, string.Format( fileNameFormat, major, minor ) );
            if ( File.Exists( pakPath ) )
            {
                pak = new PAKFileSystem();
                pak.Load( pakPath );
            }

            return pak;
        }

        private bool TryOpenFile( PAKFileSystem pak, string path, out FileStream<string> stream )
        {
            if ( pak != null && pak.Exists( path ) )
            {
                mLoadedFiles.Add( path );
                stream = pak.OpenFile( path );
                return true;
            }

            stream = null;
            return false;
        }

        private T TryLoadFile<T>( PAKFileSystem pak, string path, Func<FileStream<string>, T> fileLoader )
        {
            if ( TryOpenFile( pak, path, out var stream ) )
            {
                return fileLoader( stream );
            }

            return default;
        }

        private void TryAddFile<T>( PAKFileSystem pak, T obj, string handle, Func<T, Stream> saveFunc )
        {
            if ( obj != null )
                pak.AddFile( handle, saveFunc( obj ), true, ConflictPolicy.Replace );
        }
    }
}