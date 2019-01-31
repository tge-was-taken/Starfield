using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Starfield.Layout.Persona5.Serialization;

namespace Starfield.Playground
{
    class Program
    {
        static void Main( string[] args )
        {
            var fbn = new FbnBinary( @"D:\Modding\Persona 5 EU\Main game\ExtractedClean\ps3\field\f001_001\data\f001_001_00.FBN" );
            fbn.Save( @"D:\Modding\Persona 5 EU\Main game\ExtractedClean\ps3\field\f001_001\data\f001_001_00 (new).FBN" );

            var htb = new HtbBinary( @"D:\Programming\Repos\Mod-Compendium\Source\ModCompendium\bin\Debug\Mods\Persona5\FBN testing\Data\field\f001_001\hit\f001_001_00.HTB" );
            htb.Save( @"D:\Programming\Repos\Mod-Compendium\Source\ModCompendium\bin\Debug\Mods\Persona5\FBN testing\Data\field\f001_001\hit\f001_001_00 (new).HTB" );
        }
    }
}
