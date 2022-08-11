﻿using Inventor;
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

        public weldmentDocument(Document doc, Document accessDocument)
        {
            this.currentDocument = doc;
            this.assemblyDoc = (AssemblyDocument)currentDocument;
            this.accessDocument = accessDocument;
        }
    }
}