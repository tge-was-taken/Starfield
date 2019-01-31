using System.IO;
using Newtonsoft.Json;
using Starfield.IO;

namespace Starfield.GUI
{
    public class Settings
    {
        public string FieldModelDirectory { get; set; }

        public string FieldTextureDirectory { get; set; }

        public Settings()
        {
        }

        public static Settings FromFile( string filePath )
        {
            var text = FileManager.Instance.ReadAllText( filePath );
            return JsonConvert.DeserializeObject<Settings>( text );
        }

        public void Save( string filePath )
        {
            var text = JsonConvert.SerializeObject( this );
            FileManager.Instance.WriteAllText( filePath, text );
        }
    }
}