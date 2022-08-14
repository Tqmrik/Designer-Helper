using Inventor;
using System.Collections.Generic;
using Microsoft.VisualBasic.Compatibility.VB6;
using System;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DevAddIns
{
    class assemblyDocument : _documentObject
    {
        private AssemblyDocument assemblyDoc;

        public override string BOMStructure
        {
            get
            {
                string bomStruct = StringConverters.ToStringExt(assemblyDoc.ComponentDefinition.BOMStructure);
                if (string.IsNullOrEmpty(bomStruct))
                {
                    return "NONE";
                }
                return bomStruct;
            }
        }

        public string Material
        {
            get
            {
                return "";
            }
        }

        public int Quantity
        {
            get
            {
                if (accessDocument == assemblyDoc)
                {
                    return 1;
                }
                else if (accessDocument.isAssemblyDocument() || accessDocument.isWeldmentDocument())
                {
                    if (accessDocument is AssemblyDocument)
                    {
                        AssemblyDocument tempDOc = (AssemblyDocument)accessDocument;
                        //BOMView sd = tempDOc.ComponentDefinition.BOM.BOMViews[this.BOMStructure];
                        return 2;
                    }
                }
                return 3;
            }
        }

        public List<_documentObject> referencedParts = new List<_documentObject>();

        public assemblyDocument(Document doc, Document accessDocument)
        {
            this.currentDocument = doc;
            this.assemblyDoc = (AssemblyDocument)currentDocument;
            this.accessDocument = accessDocument;

            foreach(Document refDoc in doc.ReferencedDocuments)
            {
                if(refDoc.isAssemblyDocument())
                {
                    referencedParts.Add(new assemblyDocument(refDoc, doc));
                }
                else if(refDoc.isPartDocument())
                {
                    referencedParts.Add(new partDocument(refDoc, doc));
                }
                else if(refDoc.isSheetMetalDocument())
                {
                    referencedParts.Add(new sheetMetalDocument(refDoc, doc));
                }
                else if(refDoc.isWeldmentDocument())
                {
                    referencedParts.Add(new weldmentDocument(refDoc, doc));
                }
            }
        }
    }
}
