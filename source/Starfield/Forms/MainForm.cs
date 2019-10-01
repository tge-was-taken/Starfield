using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using AtlusFileSystemLibrary.FileSystems.PAK;
using GFDLibrary;
using GFDLibrary.Rendering.OpenGL;
using GFDLibrary.Textures;
using Ookii.Dialogs;
using Starfield.Editor;
using Starfield.Graphics;
using Starfield.Graphics.Primitives;
using Starfield.GUI.Controls;
using Starfield.GUI.TypeConverters;
using Starfield.Layout.Persona5;
using Starfield.Layout.Persona5.Serialization;
using Starfield.IO;

namespace Starfield.GUI.Forms
{
    public partial class MainForm : Form
    {
        private static readonly Regex  sFieldPakNameRegex   = new Regex( @"(f|fnpc)(\d{3})_(\d{3})", RegexOptions.Compiled );
        private static readonly Color  sHitTriggerColor     = Color.Yellow;
        private static readonly Color  sEntranceColor       = Color.Blue;
        private static readonly Color  sBlock9EntryColor    = Color.Red;
        private static readonly Color  sMessageTriggerColor = Color.Green;
        private static readonly Color  sBlock18EntryColor   = Color.Purple;
        private static readonly Color  sBlock19EntryColor   = Color.Cyan;
        private static readonly Color  sBlock22EntryColor   = Color.HotPink;
        private static readonly string sSettingsPath        = "settings.json";

        private ListBoxItem mLastSelectedItem;

        public Settings Settings { get; private set; }

        public SceneEditorControl Editor { get; private set; }

        public int FieldMajorId { get; private set; }

        public int FieldMinorId { get; private set; }

        public FieldLocalData FieldLocalData { get; private set; }

        public FieldGlobalData FieldGlobalData { get; private set; }

        public SceneNode FieldModelRootNode { get; private set; }

        public SceneNode ObjectRootNode { get; private set; }

        public MainForm()
        {
            InitializeComponent();
            Text = Program.DisplayName;
            InitializeSettings();
            AssignDefaultTypeConverters();
            InitializeSceneViewport();

            RegisterEvents();
        }

        protected override void OnFormClosed( FormClosedEventArgs e )
        {
            Settings.Save( sSettingsPath );
            base.OnFormClosed( e );
        }

        private void InitializeSettings()
        {
            if ( FileManager.Instance.Exists( sSettingsPath ) )
            {
                Settings = Settings.FromFile( sSettingsPath );
            }
            else
            {
                Settings = new Settings();
            }

#if DEBUG
            if ( Directory.Exists( @"D:\Modding\Persona 5 EU\Main game\ExtractedClean" ) )
            {
                Settings.FieldModelDirectory   = @"D:\Modding\Persona 5 EU\Main game\ExtractedClean\data\model\field_tex\";
                Settings.FieldTextureDirectory = @"D:\Modding\Persona 5 EU\Main game\ExtractedClean\ps3\model\field_tex\textures\";
            }
#endif
        }

        private void AssignDefaultTypeConverters()
        {
            // Assign default type converters for commonly used types
            TypeDescriptor.AddAttributes( typeof( Vector2 ),    new TypeConverterAttribute( typeof( Vector2TypeConverter ) ) );
            TypeDescriptor.AddAttributes( typeof( Vector3 ),    new TypeConverterAttribute( typeof( Vector3TypeConverter ) ) );
            TypeDescriptor.AddAttributes( typeof( Vector4 ),    new TypeConverterAttribute( typeof( Vector4TypeConverter ) ) );
            TypeDescriptor.AddAttributes( typeof( Quaternion ), new TypeConverterAttribute( typeof( QuaternionTypeConverter ) ) );
        }

        private void InitializeSceneViewport()
        {
            Editor = new SceneEditorControl();
            sceneViewportPanel.Controls.Add( Editor );

            Editor.Scene.Root.Children.Add( new SceneNode( "Center" )
            {
                Translation = new Vector3(),
                Drawable    = new CubePrimitive()
            } );

            FieldModelRootNode = new SceneNode( "FieldModelRootNode" );
            ObjectRootNode     = new SceneNode( "ObjectRootNode" );
            Editor.Scene.Root.Children.Add( FieldModelRootNode );
            Editor.Scene.Root.Children.Add( ObjectRootNode );
        }

        private void RegisterEvents()
        {
            // hitTriggersListBox
            hitTriggersListBox.SelectedIndexChanged += FbnObjectListSelectionChanged;
            hitTriggersListBox.DoubleClick          += Fbn3DObjectListDoubleClickHandler;

            // entrancesListBox
            entrancesListBox.SelectedIndexChanged += FbnObjectListSelectionChanged;
            entrancesListBox.DoubleClick          += Fbn3DObjectListDoubleClickHandler;

            // hitDefinitionsListBox
            hitDefinitionsListBox.SelectedIndexChanged += FbnObjectListSelectionChanged;

            // type9EntriesListBox
            type9EntriesListBox.DoubleClick          += Fbn3DObjectListDoubleClickHandler;
            type9EntriesListBox.SelectedIndexChanged += FbnObjectListSelectionChanged;

            // type10EntriesListBox
            type10EntriesListBox.DoubleClick          += Fbn3DObjectListDoubleClickHandler;
            type10EntriesListBox.SelectedIndexChanged += FbnObjectListSelectionChanged;

            // type11EntriesListBox
            type11EntriesListBox.DoubleClick          += Fbn3DObjectListDoubleClickHandler;
            type11EntriesListBox.SelectedIndexChanged += FbnObjectListSelectionChanged;

            // msgTriggersListBox
            msgTriggersListBox.SelectedIndexChanged += FbnObjectListSelectionChanged;
            msgTriggersListBox.DoubleClick          += Fbn3DObjectListDoubleClickHandler;

            // type18EntriesListBox
            type18EntriesListBox.DoubleClick          += Fbn3DObjectListDoubleClickHandler;
            type18EntriesListBox.SelectedIndexChanged += FbnObjectListSelectionChanged;

            // type19EntriesListBox
            type19EntriesListBox.DoubleClick          += Fbn3DObjectListDoubleClickHandler;
            type19EntriesListBox.SelectedIndexChanged += FbnObjectListSelectionChanged;

            // type22EntriesListBox
            type22EntriesListBox.DoubleClick          += Fbn3DObjectListDoubleClickHandler;
            type22EntriesListBox.SelectedIndexChanged += FbnObjectListSelectionChanged;

            // layerIndexInput
            layerIndexInput.ValueChanged += LayerIndexInput_ValueChanged;

            // objPropertyGrid
            objPropertyGrid.PropertyValueChanged += ObjPropertyGrid_PropertyValueChanged;

            // view control
            Editor.OnNodeSelected += OnSceneNodeSelected;
        }

        private void OnSceneNodeSelected( object sender, SceneNode e )
        {
            if ( e.Tag != null && e.Tag is ListBoxItem listBoxItem )
            {
                TabPage selectedTab = null;
                foreach ( TabPage tabPage in objListTabControl.TabPages )
                {
                    if ( tabPage.Controls.Count > 0 && tabPage.Controls[ 0 ] == listBoxItem.ListBox )
                    {
                        selectedTab = tabPage;
                        break;
                    }
                }

                if ( selectedTab != null )
                {
                    objListTabControl.SelectedTab = selectedTab;
                    listBoxItem.ListBox.SelectedItem = listBoxItem;
                    mLastSelectedItem = listBoxItem;
                }
            }
        }

        private void ObjPropertyGrid_PropertyValueChanged( object s, PropertyValueChangedEventArgs e )
        {
            if ( mLastSelectedItem != null && mLastSelectedItem.SceneNode != null )
            {
                // TODO: this sucks
                mLastSelectedItem.SyncTransform( mLastSelectedItem.Value, mLastSelectedItem.SceneNode );
                Editor.Refresh();
            }
        }

        public void LoadField( string directoryPath, int majorId, int minorId )
        {
            UnloadField();

            var fieldData = new FieldLocalData( directoryPath, majorId, minorId );
            LoadFieldLocalData( fieldData, majorId, minorId );

            LoadFieldModel( majorId, minorId );
        }

        private void LoadFieldModel( int majorId, int minorId )
        {
            var modelMajorId = majorId;
            var modelMinorId = minorId;
            if ( majorId >= 150 )
            {
                modelMajorId -= 100;
                modelMinorId *= 10;
            }

            var modelPath = Path.Combine( Settings.FieldModelDirectory, $"f{modelMajorId:D3}_{modelMinorId:D3}_0.GFS" );
            if ( File.Exists( modelPath ) )
            {
                var modelPack =
                    GFDLibrary.Resource.Load<ModelPack>( modelPath );

                var texturePakPath    = Path.Combine( Settings.FieldTextureDirectory, $"tex{modelMajorId:D3}_{modelMinorId:D3}_00_00.bin" );
                var textureDictionary = new Dictionary<string, GLTexture>();
                if ( File.Exists( texturePakPath ) )
                {
                    using ( var texturePak = new PAKFileSystem() )
                    {
                        texturePak.Load( texturePakPath );
                        foreach ( string file in texturePak.EnumerateFiles().Where( x => x.EndsWith( "dds" ) ) )
                            textureDictionary[ file ] = new GLTexture( new FieldTexturePS3( texturePak.OpenFile( file ) ) );
                    }
                }
                else if ( modelPack.Textures != null )
                {
                    foreach ( var texture in modelPack.Textures )
                        textureDictionary[ texture.Key ] = new GLTexture( texture.Value );
                }

                var modelPackDrawable = new ModelPackDrawable( modelPack, ( material, textureName ) => textureDictionary[ textureName ] );
                FieldModelRootNode.Drawable = modelPackDrawable;

                SetCollisionVisible( false );
            }
        }

        public void SetCollisionVisible( bool visible )
        {
            var modelPackDrawable = ( ModelPackDrawable ) FieldModelRootNode.Drawable;
            foreach ( var collisionNode in modelPackDrawable.Model.Nodes.Where( x => x.Node.Name.StartsWith( "atari" ) ) )
                collisionNode.IsVisible = visible;
        }

        public void LoadFieldLocalData( FieldLocalData field, int majorId, int minorId )
        {
            FieldLocalData = field;
            FieldMajorId   = majorId;
            FieldMinorId   = minorId;

            LoadDefaultCamera( FieldLocalData.DefaultCamera );

            // Set up layers
            layerIndexInput.Minimum = 0;

            if ( FieldLocalData.Layers.Count > 0 )
            {
                LoadLayer( 0 );
            }
            else
            {
                UnloadLayer();
            }
        }

        private void UnloadField()
        {
            UnloadLayer();
            FieldModelRootNode.Drawable = null;
        }

        private void LoadLayer( int i )
        {
            UnloadLayer();

            var layer = FieldLocalData.Layers[ i ];

            if ( layer.ObjectPlacement != null )
            {
                LoadObjectPlacement( layer.ObjectPlacement );
            }

            if ( layer.HitTable != null )
            {
                LoadHitTable( layer.HitTable );
            }

            // Redraw viewport
            Editor.Refresh();
        }

        private void UnloadLayer()
        {
            hitTriggersListBox.DataSource    = null;
            entrancesListBox.DataSource      = null;
            hitDefinitionsListBox.DataSource = null;
            type8EntriesListBox.DataSource   = null;
            type9EntriesListBox.DataSource   = null;
            type10EntriesListBox.DataSource  = null;
            type11EntriesListBox.DataSource  = null;
            msgTriggersListBox.DataSource    = null;
            type18EntriesListBox.DataSource  = null;
            type19EntriesListBox.DataSource  = null;
            type22EntriesListBox.DataSource  = null;
            ObjectRootNode.Children.Clear();

            // Redraw viewport
            Editor.Refresh();
        }

        public void LoadObjectPlacement( FbnBinary fbn )
        {
            if ( fbn.HitTriggers != null )
            {
                LoadFieldItems( hitTriggersListBox, fbn.HitTriggers, "Hit trigger {0}",
                                ( entry, name ) => new SceneNode( name )
                                {
                                    Translation = entry.Center,
                                    Drawable    = new CubePrimitive( sHitTriggerColor.ToVector4() ),
                                },
                                ( entry, node ) => node.Translation = entry.Center );
            }

            if ( fbn.Entrances != null )
            {
                LoadFieldItems( entrancesListBox, fbn.Entrances, "Entrance {0}",
                                ( entry, name ) => new SceneNode( name )
                                {
                                    Translation = entry.Position,
                                    Drawable    = new CubePrimitive( sEntranceColor.ToVector4() )
                                },
                                ( entry, node ) => node.Translation = entry.Position );
            }

            if ( fbn.Block8Entries != null )
            {
                LoadFieldItems( type8EntriesListBox, fbn.Block8Entries, "Entry {0}", null, null );
            }

            if ( fbn.Block9Entries != null )
            {
                LoadFieldItems( type9EntriesListBox, fbn.Block9Entries, "Entry {0}",
                                ( entry, name ) => new SceneNode( name )
                                {
                                    Translation = entry.Position,
                                    Drawable    = new CubePrimitive( sBlock9EntryColor.ToVector4() )
                                },
                                ( entry, node ) => node.Translation = entry.Position );
            }

            if ( fbn.Block10Entries != null )
                LoadFieldItems( type10EntriesListBox, fbn.Block10Entries, "Entry {0}", null, null );

            if ( fbn.Block11Entries != null )
                LoadFieldItems( type11EntriesListBox, fbn.Block11Entries, "Entry {0}", null, null );

            if ( fbn.MessageTriggers != null )
            {
                // TODO: handle multiple positions
                LoadFieldItems( msgTriggersListBox, fbn.MessageTriggers, "Message trigger {0}",
                                ( entry, name ) => new SceneNode( name )
                                {
                                    Translation = entry.Positions[ 0 ],
                                    Drawable    = new CubePrimitive( sMessageTriggerColor.ToVector4() )
                                },
                                ( entry, node ) => node.Translation = entry.Positions[ 0 ] );
            }

            if ( fbn.Block18Entries != null )
            {
                LoadFieldItems( type18EntriesListBox, fbn.Block18Entries, "Entry {0}",
                                ( entry, name ) => new SceneNode( name )
                                {
                                    Translation = entry.Position,
                                    Drawable    = new CubePrimitive( sBlock18EntryColor.ToVector4() )
                                },
                                ( entry, node ) => node.Translation = entry.Position );
            }

            if ( fbn.Block19Entries != null )
            {
                LoadFieldItems( type19EntriesListBox, fbn.Block19Entries, "Entry {0}",
                                ( entry, name ) => new SceneNode( name )
                                {
                                    Translation = entry.Center,
                                    Drawable    = new CubePrimitive( sBlock19EntryColor.ToVector4() )
                                },
                                ( entry, node ) => node.Translation = entry.Center );
            }

            if ( fbn.Block22Entries != null )
            {
                LoadFieldItems( type22EntriesListBox, fbn.Block22Entries, "Entry {0}",
                                ( entry, name ) => new SceneNode( name )
                                {
                                    Translation = entry.Center,
                                    Drawable    = new CubePrimitive( sBlock22EntryColor.ToVector4() )
                                },
                                ( entry, node ) => node.Translation = entry.Center );
            }
        }

        public void LoadHitTable( HtbBinary htb )
        {
            LoadFieldItems( hitDefinitionsListBox, htb, "Hit entry {0}", null, null );
        }

        public void LoadDefaultCamera( CmrBinary cmr )
        {
            cameraPropertyGrid.SelectedObject = cmr;
        }

        public void LoadFieldItems<T>( ListBox                    listBox, IList<T> items, string itemNameFormat,
                                       Func<T, string, SceneNode> sceneNodeFactoryFunc,
                                       Action<T, SceneNode>       transformSyncAction )
        {
            // Wrap the objects in an object that can be displayed with a user friendly name
            var displayObjects = new List<ListBoxItem>();
            for ( var i = 0; i < items.Count; i++ )
            {
                var       item = items[ i ];
                var       name = string.Format( itemNameFormat, i );
                SceneNode node = null;
                if ( sceneNodeFactoryFunc != null )
                {
                    node = sceneNodeFactoryFunc( item, name );
                    ObjectRootNode.Children.Add( node );
                }

                Action<object, SceneNode> transformSyncActionGeneric = null;
                if ( transformSyncAction != null )
                    transformSyncActionGeneric = ( o, n ) => transformSyncAction( ( T ) o, n );

                var listBoxItem = new ListBoxItem( listBox, item, name, node, transformSyncActionGeneric );
                if ( node != null )
                    node.Tag = listBoxItem;

                displayObjects.Add( listBoxItem );
            }

            // Use a BindingList here to ensure the ListBox updates when we modify the collection
            listBox.DataSource    = new BindingList<ListBoxItem>( displayObjects );
            listBox.DisplayMember = "Name";
            listBox.ValueMember   = "Value";
        }

        private void LayerIndexInput_ValueChanged( object sender, EventArgs e )
        {
            var layerIndex = ( int ) layerIndexInput.Value;
            if ( layerIndex >= FieldLocalData.Layers.Count )
                FieldLocalData.Layers.Add( new FieldLayer() );

            LoadLayer( ( int ) layerIndexInput.Value );
        }

        private void FbnObjectListSelectionChanged( object sender, EventArgs e )
        {
            var listBox = ( ListBox ) sender;
            var item    = ( ListBoxItem ) listBox.SelectedItem;

            if ( item != null && item != mLastSelectedItem )
            {
                objPropertyGrid.SelectedObject = listBox.SelectedValue;

                mLastSelectedItem = item;

                // Make drawable object appear brighter
                Editor.SelectedNode = item.SceneNode;

                // Redraw viewport
                Editor.Refresh();
            }
        }

        private void Fbn3DObjectListDoubleClickHandler( object sender, EventArgs e )
        {
            // TODO: move camera to the object
            // requires a link between the object and the scene object representation
            // either store a reference to the scene node in the display object, or instead store
            // the object in the scene object, and have use that directly as a display object

            var listBox = ( ListBox ) sender;
            var item    = ( ListBoxItem ) listBox.SelectedItem;
            if ( item != null && item.SceneNode != null )
            {
                Editor.Viewport.Camera.Position = item.SceneNode.WorldTransform.Translation + new Vector3( 0, 30f, 150f );
                Editor.Refresh();
            }
        }

        private void MainForm_Load( object sender, EventArgs e )
        {

        }

        private void fieldToolStripMenuItem_Click( object sender, EventArgs e )
        {

        }

        public string SelectFileToOpen( string filter )
        {
            using ( var dialog = new OpenFileDialog() )
            {
                dialog.Filter = filter;
                if ( dialog.ShowDialog() != DialogResult.OK )
                    return null;

                return dialog.FileName;
            }
        }

        private void fieldToolStripMenuItem1_Click( object sender, EventArgs e )
        {
            var filePath = SelectFileToOpen( "Field local data (*.pac)|*.pac" );
            if ( filePath == null )
                return;

            var fileName = Path.GetFileNameWithoutExtension( filePath );
            var match    = sFieldPakNameRegex.Match( fileName );
            if ( !match.Success )
                return;

            var majorId       = int.Parse( match.Groups[ 2 ].Value );
            var minorId       = int.Parse( match.Groups[ 3 ].Value );
            var directoryPath = Path.GetDirectoryName( filePath );
            LoadField( directoryPath, majorId, minorId );
        }

        private void saveToolStripMenuItem_Click( object sender, EventArgs e )
        {
            using ( var dialog = new VistaFolderBrowserDialog() )
            {
                if ( dialog.ShowDialog() != DialogResult.OK )
                    return;

                var directoryPath = dialog.SelectedPath;
                FieldLocalData.Save( directoryPath, FieldMajorId, FieldMinorId );
            }
        }

        private void settingsToolStripMenuItem_Click( object sender, EventArgs e )
        {
            using ( var dialog = new SettingsDialog( Settings ) )
            {
                if ( dialog.ShowDialog() != DialogResult.OK )
                    return;

                Settings.FieldModelDirectory   = dialog.SelectedFieldModelDirectory;
                Settings.FieldTextureDirectory = dialog.SelectedFieldTextureDirectory;
            }
        }

        private void layerObjectDeleteButton_Click( object sender, EventArgs e )
        {
            var listBox = ( ListBox ) objListTabControl.SelectedTab.Controls[ 0 ];
            var list    = ( IBindingList ) listBox.DataSource;
            list.RemoveAt( listBox.SelectedIndex );
        }

        private void layerObjectCloneButton_Click( object sender, EventArgs e )
        {
            // TODO make this not refer to the same instance
            var listBox = ( ListBox ) objListTabControl.SelectedTab.Controls[ 0 ];
            var list    = ( IBindingList ) listBox.DataSource;
            list.Add( list[ listBox.SelectedIndex ] );
        }
    }

    public class ListBoxInfo
    {
        public string ItemNameFormat { get; }

        public Func<object> CreateNewItem { get; }

        public Func<object, string, SceneNode> CreateSceneNodeForItem;

        public Action<object, SceneNode> SyncTransform;

        public ListBoxInfo( string                          itemNameFormat, Func<object> createNewItem,
                            Func<object, string, SceneNode> createSceneNodeForItem,
                            Action<object, SceneNode>       syncTransform )
        {
            ItemNameFormat         = itemNameFormat;
            CreateNewItem          = createNewItem;
            CreateSceneNodeForItem = createSceneNodeForItem;
            SyncTransform          = syncTransform;
        }
    }

    public class ListBoxItem
    {
        public ListBox ListBox { get; }

        public object Value { get; }

        public string Name { get; }

        public SceneNode SceneNode { get; }

        public Action<object, SceneNode> SyncTransform { get; }

        public ListBoxItem( ListBox listBox, object value, string name, SceneNode sceneNode, Action<object, SceneNode> syncTransformAction )
        {
            ListBox = listBox;
            Value = value;
            Name = name;
            SceneNode = sceneNode;
            SyncTransform = syncTransformAction;
        }
    }

    public static class ColorExtensions
    {
        public static OpenTK.Vector4 ToVector4( this Color color )
        {
            return new OpenTK.Vector4( color.R / 255f, color.G / 255f, color.B / 255f, color.A / 255f );
        }
    }
}
