using Inventor;
using System;

namespace DevAddIns
{
    class DXFTranslator : Translators
    {
        public DXFTranslator() : base() { }
        public void createFlatDXF()
        {
            string filePath = "";
            string filePathDXF = "";
            string extension = "dxf";

            PartDocument referencedDoc = null;
            SheetMetalComponentDefinition oSMDef = null;
            DataIO oDataIO = null;
            string sOut = "FLAT PATTERN DXF?AcadVersion=2010&OuterProfileLayer=Outer&BendLayer=Bend&OuterProfileLayerColor=0;0;0&BendUpLayerColor=0;0;0&BendUpLayerLineType=37644&BendDownLayerColor=0;0;0&BendUpLayerLineWeight=.025&TrimCenterlinesAtContour=True&MergeProfilesIntoPolyline=True&InvisibleLayers=IV_TANGENT;IV_ARC_CENTERS;&RebaseGeometry=True";
            PartDocument oFD = null;


            if (activeDocument.isDrawingDocument())
            {
                foreach (Document oFDF in activeDocument.ReferencedDocuments)
                {//Check for every referenced document in the drawing and create step file of each

                    if (!String.IsNullOrEmpty(oFDF.FullFileName))
                    {
                        referencedDoc = (PartDocument)InventorApplication.Documents.ItemByName[oFDF.FullFileName];

                        if (!oFDF.isSheetMetalDocument())
                        {
                            return;
                        }

                        oSMDef = (SheetMetalComponentDefinition)referencedDoc.ComponentDefinition;
                        oDataIO = oSMDef.DataIO;

                        filePathDXF = RevisionHelper.addRevisionLetter(oFDF, PathConverter.clearExtension(oFDF), extension);
                    }
                    else
                    {
                        filePathDXF = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "\\tempOutput";
                        int iterator = 1;
                        while (System.IO.File.Exists(filePath + $".{extension}"))
                        {
                            filePathDXF = filePathDXF.Remove(filePath.Length - 1) + iterator.ToString();
                            iterator++;
                        }
                        filePathDXF += $".{extension}";
                    }

                    if (!oSMDef.HasFlatPattern)
                    {
                        oSMDef.Unfold();
                        oSMDef.FlatPattern.ExitEdit();

                        //help source on string build: https://www.cadforum.cz/en/export-unfolds-of-sheetmetal-parts-to-dxf-parameters-for-ilogic-
                        oDataIO.WriteDataToFile(sOut, filePathDXF);
                        if (referencedDoc != InventorApplication.ActiveDocument) referencedDoc.Close();
                    }
                }
            }
            else if (activeDocument.isSheetMetalDocument())
            {

                if (!String.IsNullOrEmpty(activeDocument.FullFileName))
                {
                    referencedDoc = (PartDocument)InventorApplication.Documents.ItemByName[activeDocument.FullFileName];

                    if (!activeDocument.isSheetMetalDocument())
                    {
                        return;
                    }

                    oSMDef = (SheetMetalComponentDefinition)referencedDoc.ComponentDefinition;
                    oDataIO = oSMDef.DataIO;

                    filePathDXF = RevisionHelper.addRevisionLetter(activeDocument, PathConverter.clearExtension(activeDocument), extension);
                }
                else
                {
                    //Add file check and then increment
                    filePathDXF = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "\\tempOutput";
                    int iterator = 1;
                    while (System.IO.File.Exists(filePath + $".{extension}"))
                    {
                        filePathDXF = filePathDXF.Remove(filePath.Length - 1) + iterator.ToString();
                        iterator++;
                    }
                    filePathDXF += $".{extension}";
                }

                if (!oSMDef.HasFlatPattern)
                {
                    oSMDef.Unfold();
                    oSMDef.FlatPattern.ExitEdit();
                }
                //help source on string build: https://www.cadforum.cz/en/export-unfolds-of-sheetmetal-parts-to-dxf-parameters-for-ilogic-
                oDataIO.WriteDataToFile(sOut, filePathDXF);
                if (referencedDoc != InventorApplication.ActiveDocument) referencedDoc.Close();
            }
            else if (activeDocument.isAssemblyDocument() || activeDocument.isWeldmentDocument())
            {
                if (includeParts)
                {
                    foreach (Document oFDF in activeDocument.ReferencedDocuments)
                    {//Check for every referenced document in the drawing and create step file of each

                        if (!(oFDF.isPartDocument() || oFDF.isSheetMetalDocument()))
                        {
                            continue;
                        }


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


                        if (!String.IsNullOrEmpty(oFDF.FullFileName))
                        {
                            referencedDoc = (PartDocument)InventorApplication.Documents.ItemByName[oFDF.FullFileName];

                            if (!oFDF.isSheetMetalDocument())
                            {
                                continue;
                            }

                            oSMDef = (SheetMetalComponentDefinition)((PartDocument)oFDF).ComponentDefinition;
                            oDataIO = oSMDef.DataIO;

                            filePathDXF = RevisionHelper.addRevisionLetter(oFDF, PathConverter.clearExtension(oFDF), extension);

                            if (!oSMDef.HasFlatPattern)
                            {
                                oSMDef.Unfold();
                                oSMDef.FlatPattern.ExitEdit();
                            }
                            oDataIO.WriteDataToFile(sOut, filePathDXF);
                            if (referencedDoc != InventorApplication.ActiveDocument) referencedDoc.Close();
                        }
                        else
                        {
                            ////Add file check and then increment
                            //filePathDXF = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "\\tempOutput";
                            //int iterator = 1;
                            //while (System.IO.File.Exists(filePath + ".stp"))
                            //{
                            //    filePathDXF = filePathDXF.Remove(filePath.Length - 1) + iterator.ToString();
                            //    iterator++;
                            //}
                            //filePathDXF += ".dxf";
                            continue;
                        }

                    }
                }
            }
        }
    }
}
