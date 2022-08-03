using Inventor;
using System;

namespace DevAddIns
{
    class DXFTranslator : Translators
    {
        public DXFTranslator() : base() { }

        string filePath = "";
        string extension = "dxf";
        SheetMetalComponentDefinition oSMDef;
        DataIO oDataIO;

        string sOut = "FLAT PATTERN DXF?AcadVersion=2010&OuterProfileLayer=Outer&BendLayer=Bend&OuterProfileLayerColor=0;0;0&BendUpLayerColor=0;0;0&BendUpLayerLineType=37644&BendDownLayerColor=0;0;0&BendUpLayerLineWeight=.025&TrimCenterlinesAtContour=True&MergeProfilesIntoPolyline=True&InvisibleLayers=IV_TANGENT;IV_ARC_CENTERS;&RebaseGeometry=True";
        //help source on string build: https://www.cadforum.cz/en/export-unfolds-of-sheetmetal-parts-to-dxf-parameters-for-ilogic-

        public void createFlatDXF(Document doc)
        {

            if(doc.isDrawingDocument())
            {
                if (packAssembly)
                {
                    foreach (Document oFD in doc.AllReferencedDocuments)
                    {
                        TestMeth(oFD);
                    }
                    return;
                }
                else
                {
                    foreach (Document oFDF in activeDocument.ReferencedDocuments)
                    {//Check for every referenced document in the drawing and create step file of each
                        TestMeth(oFDF);
                        //referencedDoc = (PartDocument)InventorApplication.Documents.ItemByName[doc.FullFileName
                    }
                }

            }
            else if (doc.isSheetMetalDocument())
            {
                TestMeth(doc);
            }
            else if (doc.isAssemblyDocument() || doc.isWeldmentDocument())
            {
                if(packAssembly)
                {
                    foreach(Document oFD in doc.AllReferencedDocuments)
                    {
                        TestMeth(oFD);
                    }
                }
                else if (includeParts)
                {
                    foreach (Document oFDF in doc.ReferencedDocuments)
                    {//Check for every referenced document in the drawing and create step file of each

                        if (oFDF.isAssemblyDocument())
                        {
                            if (((AssemblyDocument)oFDF).ComponentDefinition.BOMStructure == BOMStructureEnum.kPurchasedBOMStructure) //Check to see if the part purchased or not
                            {
                                continue;
                            }
                        }
                        else if (oFDF.isPartDocument())
                        {
                            if (((PartDocument)oFDF).ComponentDefinition.BOMStructure == BOMStructureEnum.kPurchasedBOMStructure) //Check to see if the part purchased or not
                            {
                                continue;
                            }
                        }
                        else
                        {
                            continue;
                        }

                        if (!oFDF.isSheetMetalDocument())
                        {
                            continue;
                        }

                        TestMeth(oFDF);
                    }
                }
                else
                {
                    return;
                }
            }
        }
        private void filePathHelper(Document doc)
        {
            if (!String.IsNullOrEmpty(doc.FullFileName))
            {
                if (!doc.isSheetMetalDocument())
                {
                    return;
                }
                filePath = RevisionHelper.addRevisionLetter(doc, PathConverter.clearExtension(doc), extension);
            }
            else
            {
                filePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "\\tempOutput";
                int iterator = 1;
                while (System.IO.File.Exists(filePath + $".{extension}"))
                {
                    filePath = filePath.Remove(filePath.Length - 1) + iterator.ToString();
                    iterator++;
                }
                filePath += $".{extension}";
            }
        }

        private void TestMeth(Document doc)
        {
            if (doc.isSheetMetalDocument())
            {
                doc.UnitsOfMeasure.LengthUnits = UnitsTypeEnum.kMillimeterLengthUnits;

                filePathHelper(doc);
                oSMDef = (SheetMetalComponentDefinition)((PartDocument)doc).ComponentDefinition;
                oDataIO = oSMDef.DataIO;

                if (!oSMDef.HasFlatPattern)
                {
                    //Error prone
                    oSMDef.Unfold();
                    oSMDef.FlatPattern.ExitEdit();
                }

                oDataIO.WriteDataToFile(sOut, filePath);

                if (doc != InventorApplication.ActiveDocument)
                {
                    doc.Close(); 
                }
            }
            else return;
        }

    }
}
