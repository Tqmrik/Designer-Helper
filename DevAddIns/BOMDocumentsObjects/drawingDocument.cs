using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventor;

namespace DevAddIns
{
    class drawingDocument : _documentObject
    {
        public override string BOMStructure => "Drawing";

        public override string Material => "";

        public override int Quantity => 1;

        public drawingDocument(Document drawingDoc, Document accessDocument)
        {
            this.currentDocument = drawingDoc;
            //this.partDoc = (PartDocument)currentDocument;
            this.accessDocument = accessDocument;
        }
    }
}
