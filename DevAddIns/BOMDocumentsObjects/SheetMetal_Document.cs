﻿using Inventor;
using Microsoft.VisualBasic.Compatibility.VB6;
using System;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DevAddIns
{
    class SheetMetal_Document : Document_Object
    {
        private readonly PartDocument partDoc;
        private SheetMetalComponentDefinition oSMCD;

        public override string BOMStructure
        {
            get
            {
                string bomStruct = StringConverters.ToStringExt(((PartDocument)partDoc).ComponentDefinition.BOMStructure);
                if (string.IsNullOrEmpty(bomStruct))
                {
                    return "NONE";
                }
                return bomStruct;
            }
        }

        public string SheetThickness
        {
            get
            {
                if (currentDocument.IsSheetMetalDocument())
                {
                    oSMCD = (SheetMetalComponentDefinition)((PartDocument)partDoc).ComponentDefinition;
                    string thickness = oSMCD.Thickness.Expression;
                    if (!String.IsNullOrEmpty(thickness))
                    {
                        return thickness;
                    }
                    return "";
                }

                //TODO: Change
                return "0";
            }
            
        }
        public override string Material
        {
            get
            {
                string material = ((PartDocument)partDoc).ActiveMaterial.DisplayName.ToString();
                if (String.IsNullOrEmpty(material))
                {
                    return "NONE";
                }
                return material;
            }
        }

        public override int Quantity
        {
            get
            {
                if (accessDocument == partDoc)
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

        public SheetMetal_Document(Document partDocument, Document accessDocument)
        {
            this.currentDocument = partDocument;
            this.partDoc = (PartDocument)currentDocument;
            this.accessDocument = accessDocument;
        }

    }
}
