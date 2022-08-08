using Inventor;
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
                string bomStruct = assemblyDoc.ComponentDefinition.BOMStructure.ToStringExt();
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


        public assemblyDocument(Document doc, Document accessDocument)
        {
            this.currentDocument = doc;
            this.assemblyDoc = (AssemblyDocument)currentDocument;
            this.accessDocument = accessDocument;
        }
    }
}
