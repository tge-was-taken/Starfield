using System;
using System.Reflection;
using System.Windows.Forms;
using Starfield.GUI.Forms;

namespace Starfield.GUI
{
    static class Program
    {
        public static Assembly     Assembly     { get; } = Assembly.GetExecutingAssembly();
        public static AssemblyName AssemblyName { get; } = Assembly.GetName();
        public static string       ShortName    { get; } = "Starfield";
        public static Version      Version      { get; } = AssemblyName.Version;

#if DEBUG
        public static string       DisplayName { get; } = $"{ShortName} {Version.Major}.{Version.Minor}.{Version.Revision} [DEBUG]";
#else
        public static string       DisplayName { get; } = $"{ShortName} {Version.Major}.{Version.Minor}.{Version.Revision}";
#endif

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault( false );
            Application.Run( new MainForm() );
        }
    }
}
