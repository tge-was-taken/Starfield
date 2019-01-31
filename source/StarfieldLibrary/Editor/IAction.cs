using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starfield.Editor
{
    public interface IAction
    {
        void Do();

        void Undo();
    }
}
