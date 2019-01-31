using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ookii.Dialogs;

namespace Starfield.GUI.Forms
{
    public partial class SettingsDialog : Form
    {
        public string SelectedFieldModelDirectory
        {
            get => fieldModelDirectoryTextBox.Text;
            set => fieldModelDirectoryTextBox.Text = value;
        }

        public string SelectedFieldTextureDirectory
        {
            get => fieldTextureDirectoryTextBox.Text;
            set => fieldTextureDirectoryTextBox.Text = value;
        }

        public SettingsDialog( Settings currentSettings )
        {
            InitializeComponent();
            SelectedFieldModelDirectory = currentSettings.FieldModelDirectory;
            SelectedFieldTextureDirectory = currentSettings.FieldTextureDirectory;
        }

        private void fieldModelDirectorySelectButton_Click( object sender, EventArgs e )
        {
            SelectedFieldModelDirectory = SelectDirectory() ?? SelectedFieldModelDirectory;
        }

        private void fieldTextureDirectorySelectButton_Click( object sender, EventArgs e )
        {
            SelectedFieldTextureDirectory = SelectDirectory() ?? SelectedFieldTextureDirectory;
        }

        private static string SelectDirectory()
        {
            using ( var dialog = new VistaFolderBrowserDialog() )
            {
                if ( dialog.ShowDialog() != DialogResult.OK )
                    return null;

                return dialog.SelectedPath;
            }
        }
    }
}
