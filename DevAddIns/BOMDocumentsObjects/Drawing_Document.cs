using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventor;

namespace DevAddIns
{
    class Drawing_Document : Document_Object
    {
        public override string BOMStructure => "Drawing";

        public override string Material => "";

        public override int Quantity => 1;

        public Drawing_Document(Document drawingDoc, Document accessDocument)
        {
            this.currentDocument = drawingDoc;
            //this.partDoc = (PartDocument)currentDocument;
            this.accessDocument = accessDocument;
        }
    }
}
