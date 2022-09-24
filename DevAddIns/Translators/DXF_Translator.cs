using Inventor;
using System;

namespace DevAddIns
{
    class DXF_Translator : Translator_Object
    {
        public DXF_Translator() : base() { }

        readonly string extension = "dxf";
        SheetMetalComponentDefinition oSMDef;
        DataIO oDataIO;

        string sOut = "FLAT PATTERN DXF?AcadVersion=2010&OuterProfileLayer=Outer&BendLayer=Bend&OuterProfileLayerColor=0;0;0&BendUpLayerColor=0;0;0&BendUpLayerLineType=37644&BendDownLayerColor=0;0;0&BendUpLayerLineWeight=.025&TrimCenterlinesAtContour=True&MergeProfilesIntoPolyline=True&InvisibleLayers=IV_TANGENT;IV_ARC_CENTERS;&RebaseGeometry=True";
        //help source on string build: https://www.cadforum.cz/en/export-unfolds-of-sheetmetal-parts-to-dxf-parameters-for-ilogic-

        public void createFlatDXF(Document document)
        {

            if(document.IsDrawingDocument())
            {
                if (packAssembly)
                {
                    foreach (Document oFD in document.AllReferencedDocuments)
                    {
                        dxfCreate(oFD);
                    }
                    return;
                }
                else
                {
                    foreach (Document oFDF in activeDocument.ReferencedDocuments)
                    {//Check for every referenced document in the drawing and create step file of each
                        dxfCreate(oFDF);
                        //referencedDoc = (PartDocument)InventorApplication.Documents.ItemByName[doc.FullFileName
                    }
                }

            }
            else if (document.IsSheetMetalDocument())
            {
                dxfCreate(document);
            }
            else if (document.IsAssemblyDocument() || document.IsWeldmentDocument())
            {
                if(packAssembly)
                {
                    foreach(Document oFD in document.AllReferencedDocuments)
                    {
                        dxfCreate(oFD);
                    }
                }
                else if (includeParts)
                {
                    foreach (Document oFDF in document.ReferencedDocuments)
                    {//Check for every referenced document in the drawing and create step file of each

                        if (oFDF.IsAssemblyDocument())
                        {
                            if (((AssemblyDocument)oFDF).ComponentDefinition.BOMStructure == BOMStructureEnum.kPurchasedBOMStructure) //Check to see if the part purchased or not
                            {
                                continue;
                            }
                        }
                        else if (oFDF.IsPartDocument())
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

                        if (!oFDF.IsSheetMetalDocument())
                        {
                            continue;
                        }

                        dxfCreate(oFDF);
                    }
                }
                else
                {
                    return;
                }
            }
        }

        private void dxfCreate(Document document)
        {
            if (document.IsSheetMetalDocument())
            {
                document.UnitsOfMeasure.LengthUnits = UnitsTypeEnum.kMillimeterLengthUnits;

                if(String.IsNullOrEmpty(filePath))
                {
                    FilePathHelper(document, extension);
                }
                
                oSMDef = (SheetMetalComponentDefinition)((PartDocument)document).ComponentDefinition;

                oDataIO = oSMDef.DataIO;

                if (!oSMDef.HasFlatPattern)
                {
                    //Error prone
                    oSMDef.Unfold();
                    oSMDef.FlatPattern.ExitEdit();
                }

                oDataIO.WriteDataToFile(sOut, filePath);

                if (document != _inventorApplication.ActiveDocument)
                {
                    document.Close(); 
                }
            }
            else return;
        }

    }
}
