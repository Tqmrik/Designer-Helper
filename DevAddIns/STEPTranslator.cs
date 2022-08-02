using Inventor;
using System;
using System.Windows.Forms;

namespace DevAddIns
{
    class STEPTranslator : Translators
    {

        public STEPTranslator() : base()
        {
            oTranslator = (TranslatorAddIn)InventorApplication.ApplicationAddIns.ItemById["{90AF7F40-0C01-11D5-8E83-0010B541CD80}"];
        }

        public void createSTEP()
        {
            //Wrap in the try/catch???
            oContext.Type = IOMechanismEnum.kFileBrowseIOMechanism;

            string filePath = "";
            string filePathStep = "";
            string extension = "stp";

            Document referencedDocumentObject = null;

            if (oTranslator.Equals(null))
            {
                MessageBox.Show("Couldn't connect to the STEP translator");
                return;
            }

            if (activeDocument.isDrawingDocument())//Drawing
            {
                foreach (Document oFD in activeDocument.ReferencingDocuments)
                {//Check for every referenced document in the drawing and create step file of each
                    //How did i found out about the type though

                    if (!String.IsNullOrEmpty(oFD.FullFileName))
                    {
                        referencedDocumentObject = InventorApplication.Documents.ItemByName[oFD.FullFileName];
                        filePathStep = RevisionHelper.addRevisionLetter(referencedDocumentObject, PathConverter.clearExtension(referencedDocumentObject), extension);
                    }
                    else
                    {
                        //Add file check and then increment
                        filePathStep = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "\\tempOutput";
                        int iterator = 1;
                        while (System.IO.File.Exists(filePath + $".{extension}"))
                        {
                            filePathStep = filePathStep.Remove(filePath.Length - 1) + iterator.ToString();
                            iterator++;
                        }
                        filePathStep += $".{extension}";
                    }


                    if (oTranslator.HasSaveCopyAsOptions[activeDocument, oContext, oOptions])
                    {
                        oOptions.Value["ApplicationProtocolType"] = 3;
                        oOptions.Value["Author"] = oFD.PropertySets[3][24].Value;
                        //oOptions.Value("Authorization") = ""
                        oOptions.Value["Description"] = oFD.PropertySets[3][14].Value;
                        oOptions.Value["Organization"] = oFD.PropertySets[2][3].Value;

                        oDataMedium.FileName = filePathStep;

                        try
                        {
                            oTranslator.SaveCopyAs(referencedDocumentObject, oContext, oOptions, oDataMedium);
                            referencedDocumentObject.Close();
                            //Close doc, save memory
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message + "\n" + e.Source + "\n" + e.StackTrace + "\nAddIn: Sedenum Pack\nMethod: CreatePdfStep");
                        }

                    }
                }
            }
            else if ((activeDocument.isAssemblyDocument() || activeDocument.isWeldmentDocument()))
            {

                //Step for the assembly, only doing check to wrap things up
                if (activeDocument != null)
                {
                    if (((AssemblyDocument)activeDocument).ComponentDefinition.BOMStructure == BOMStructureEnum.kPurchasedBOMStructure) //Check to see if the part purchased or not
                    {

                    }
                    else
                    {
                        if (!String.IsNullOrEmpty(activeDocument.FullFileName))
                        {
                            referencedDocumentObject = InventorApplication.Documents.ItemByName[activeDocument.FullFileName];
                            filePathStep = RevisionHelper.addRevisionLetter(activeDocument, PathConverter.clearExtension(activeDocument), "stp");
                        }
                        else
                        {
                            //Add file check and then increment
                            filePathStep = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "\\tempOutput";
                            int iterator = 1;
                            while (System.IO.File.Exists(filePath + ".stp"))
                            {
                                filePathStep = filePathStep.Remove(filePath.Length - 1) + iterator.ToString();
                                iterator++;
                            }
                            filePathStep += ".stp";
                        }

                        if (oTranslator.HasSaveCopyAsOptions[activeDocument, oContext, oOptions])
                        {
                            oOptions.Value["ApplicationProtocolType"] = 3;
                            oOptions.Value["Author"] = activeDocument.PropertySets[3][24].Value;
                            //oOptions.Value("Authorization") = ""
                            oOptions.Value["Description"] = activeDocument.PropertySets[3][14].Value;
                            oOptions.Value["Organization"] = activeDocument.PropertySets[2][3].Value;

                            oDataMedium.FileName = filePathStep;

                            try
                            {
                                oTranslator.SaveCopyAs(referencedDocumentObject, oContext, oOptions, oDataMedium);
                            }
                            catch (Exception e)
                            {
                                MessageBox.Show(e.Message + "\n" + e.Source + "\n" + e.StackTrace + "\nAddIn: Sedenum Pack\nMethod: CreatePdfStep");
                            }
                        }
                    }

                }
                //Steps for the parts
                if (includeParts)
                {
                    foreach (Document oFD in activeDocument.ReferencedDocuments)
                    {//Check for every referenced document in the drawing and create step file of each
                        if (oFD.isAssemblyDocument())
                        {
                            if (((AssemblyDocument)oFD).ComponentDefinition.BOMStructure == BOMStructureEnum.kPurchasedBOMStructure) //Check to see if the part purchased or not
                            {
                                continue;
                            }
                        }
                        else if (oFD.isPartDocument())
                        {
                            if (((PartDocument)oFD).ComponentDefinition.BOMStructure == BOMStructureEnum.kPurchasedBOMStructure) //Check to see if the part purchased or not
                            {
                                continue;
                            }
                        }
                        if (!String.IsNullOrEmpty(oFD.FullFileName))
                        {
                            //It seems that to get the drawing you would need to search in the same folder for the file with the same name as a drawing
                            referencedDocumentObject = InventorApplication.Documents.ItemByName[oFD.FullFileName]; //Why do i need that as well????
                            filePathStep = RevisionHelper.addRevisionLetter(oFD, PathConverter.clearExtension(oFD), extension);

                            if (oTranslator.HasSaveCopyAsOptions[oFD, oContext, oOptions])
                            {
                                oOptions.Value["ApplicationProtocolType"] = 3;
                                oOptions.Value["Author"] = oFD.PropertySets[3][24].Value;
                                //oOptions.Value("Authorization") = ""
                                oOptions.Value["Description"] = oFD.PropertySets[3][14].Value;
                                oOptions.Value["Organization"] = oFD.PropertySets[2][3].Value;

                                oDataMedium.FileName = filePathStep;

                                try
                                {
                                    oTranslator.SaveCopyAs(referencedDocumentObject, oContext, oOptions, oDataMedium);
                                    if (referencedDocumentObject != InventorApplication.ActiveDocument) referencedDocumentObject.Close();
                                }
                                catch (Exception e)
                                {
                                    MessageBox.Show(e.Message + "\n" + e.Source + "\n" + e.StackTrace + "\nAddIn: Sedenum Pack\nMethod: CreatePdfStep");
                                }
                            }
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
            }
            else if (activeDocument.isPartDocument() || activeDocument.isSheetMetalDocument())
            {
                if (((PartDocument)activeDocument).ComponentDefinition.BOMStructure == BOMStructureEnum.kPurchasedBOMStructure)
                {
                    return;
                }

                if (!String.IsNullOrEmpty(activeDocument.FullFileName))
                {
                    referencedDocumentObject = InventorApplication.Documents.ItemByName[activeDocument.FullFileName];
                    filePathStep = RevisionHelper.addRevisionLetter(activeDocument, PathConverter.clearExtension(activeDocument), extension);
                }
                else
                {
                    //Add file check and then increment
                    filePathStep = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "\\tempOutput";
                    int iterator = 1;
                    while (System.IO.File.Exists(filePath + $".{extension}"))
                    {
                        filePathStep = filePathStep.Remove(filePath.Length - 1) + iterator.ToString();
                        iterator++;
                    }
                    filePathStep += $".{extension}";
                }

                if (oTranslator.HasSaveCopyAsOptions[activeDocument, oContext, oOptions])
                {
                    oOptions.Value["ApplicationProtocolType"] = 3;
                    oOptions.Value["Author"] = activeDocument.PropertySets[3][24].Value;
                    //oOptions.Value("Authorization") = ""
                    oOptions.Value["Description"] = activeDocument.PropertySets[3][14].Value;
                    oOptions.Value["Organization"] = activeDocument.PropertySets[2][3].Value;

                    oDataMedium.FileName = filePathStep;

                    try
                    {
                        oTranslator.SaveCopyAs(referencedDocumentObject, oContext, oOptions, oDataMedium);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message + "\n" + e.Source + "\n" + e.StackTrace + "\nAddIn: Sedenum Pack\nMethod: CreatePdfStep");

                    }

                }
            }

            oTranslator = null;
            oContext = null;
            oDataMedium = null;
            oDataMedium = null;

        }
    }
}
