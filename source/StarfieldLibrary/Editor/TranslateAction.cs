using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Starfield.Editor
{
    public class TranslateAction : IAction
    {
        private readonly SceneNode mNode;
        private readonly Vector3 mTranslation;
        private readonly Vector3 mOriginalTranslation;

        public TranslateAction( SceneNode node, Vector3 translation )
        {
            mNode = node;
            mTranslation = translation;
            mOriginalTranslation = mNode.Translation;
        }

        public void Do()
        {
            mNode.Translation = mTranslation;
        }

        public void Undo()
        {
            mNode.Translation = mOriginalTranslation;
        }
    }
}
