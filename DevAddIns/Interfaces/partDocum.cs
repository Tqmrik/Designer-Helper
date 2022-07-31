using Inventor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevAddIns
{
    class partDocum : IDocument
    {
        private PartDocument partDoc;
        private Document accessDoc;


        public string PartNumber { get => partDoc.PropertySets[3][2].Value.ToString(); }
        public stdole.IPictureDisp Thumbnail { get => partDoc.Thumbnail;}
        public string BOMStructure { get => partDoc.ComponentDefinition.BOMStructure.ToString();}
        public string Material { get => partDoc.ActiveMaterial.DisplayName.ToString(); }
        public string Revision { get => partDoc.PropertySets[1][7].Value.ToString();}
        public int Quantity
        {
            get
            {
                if (accessDoc == partDoc)
                {
                    return 1;
                }
                else if(accessDoc.isAssemblyDocument() || accessDoc.isWeldmentDocument())
                {
                    if(accessDoc is AssemblyDocument)
                    {
                        AssemblyDocument tempDOc= (AssemblyDocument)accessDoc;
                        BOMView sd = tempDOc.ComponentDefinition.BOM.BOMViews[this.BOMStructure];
                        return 2;
                    }
                }
                return 3;
            }
        }

        public partDocum(Document partDocument, Document accessDocument)
        {
            this.partDoc = (PartDocument)partDocument;
            this.accessDoc = accessDocument;
        }

        
    }
}
