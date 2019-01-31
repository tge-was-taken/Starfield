using AtlusScriptLibrary.FlowScriptLanguage.BinaryModel;
using Starfield.Layout.Persona5.Serialization;

namespace Starfield.Layout.Persona5
{
    public class FieldLayer
    {
        public FbnBinary ObjectPlacement { get; set; }

        public HtbBinary HitTable { get; set; }

        public FlowScriptBinary HitScript { get; set; }

        public FlowScriptBinary NpcScript { get; set; }

        public FntBinary Fnt { get; set; }

        public FptBinary Fpt { get; set; }

        public bool IsEmpty =>
            ObjectPlacement == null &&
            HitTable == null &&
            HitScript == null &&
            NpcScript == null &&
            Fnt == null &&
            Fpt == null;
    }
}
