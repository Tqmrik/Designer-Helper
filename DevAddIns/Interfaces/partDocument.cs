using Inventor;
using Microsoft.VisualBasic.Compatibility.VB6;
using System;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DevAddIns
{
    class partDocument : _documentObject
    {
        private PartDocument partDoc;

        public override string BOMStructure
        {
            get
            {
                string bomStruct = partDoc.ComponentDefinition.BOMStructure.ToStringExt();
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
                string material = partDoc.ActiveMaterial.DisplayName.ToString();
                if (String.IsNullOrEmpty(material))
                {
                    return "NONE";
                }
                return material;

            }
        }
        public int Quantity
        {
            get
            {
                if (accessDocument == partDoc)
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

        public partDocument(Document partDocument, Document accessDocument)
        {
            this.currentDocument = partDocument;
            this.partDoc = (PartDocument)currentDocument;
            this.accessDocument = accessDocument;
        }
    }
}



//TODO: Qunatity for the partDocument