using Inventor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Media;

namespace DevAddIns
{
    class weldmentDocument : _documentObject
    {
        private AssemblyDocument assemblyDoc;
        public override string BOMStructure
        {
            get
            {
                string bomStruct = StringConverters.ToStringExt(assemblyDoc.ComponentDefinition.BOMStructure);

                if (string.IsNullOrEmpty(bomStruct))
                {
                    return "NONE"; //Is it even possbile or 
                }

                return bomStruct;
            }
        }
        public override string Material
        {
            get
            {
                return "";
            }
        }
        public override int Quantity
        {
            get
            {
                if (accessDocument == assemblyDoc)
                {
                    return 1;
                }
                else if (accessDocument.IsAssemblyDocument() || accessDocument.IsWeldmentDocument())
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

        public weldmentDocument(Document doc, Document accessDocument)
        {
            this.currentDocument = doc;
            this.assemblyDoc = (AssemblyDocument)currentDocument;
            this.accessDocument = accessDocument;
        }
    }
}
