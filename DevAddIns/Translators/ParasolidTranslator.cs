using Inventor;
using System;
using System.Windows.Forms;

namespace DevAddIns
{
    class ParasolidTranslator : Translators
    {
        //TODO: Remake class
        public ParasolidTranslator() : base() 
        {
            TranslatorAddIn oTranslator = (TranslatorAddIn)_inventorApplication.ApplicationAddIns.ItemById["{8F9D3571-3CB8-42F7-8AFF-2DB2779C8465}"];
        }
        public void createParasolid()
        {
            string filePath = "";
            string filePathParasolid = "";
            string extension = "x_t";

            Document referencedDocumentObject = null;

            if (oTranslator.Equals(null))
            {
                MessageBox.Show("Couldn't connect to the PARASOLID translator");
                return;
            }



            if (activeDocument.IsDrawingDocument())//Drawing
            {
                foreach (Document oFD in activeDocument.ReferencingDocuments)
                {//Check for every referenced document in the drawing and create step file of each
                    //How did i found out about the type though

                    if (!String.IsNullOrEmpty(oFD.FullFileName))
                    {
                        referencedDocumentObject = _inventorApplication.Documents.ItemByName[oFD.FullFileName];
                        filePathParasolid = RevisionHelper.addRevisionLetter(referencedDocumentObject, PathConverter.clearExtension(referencedDocumentObject), extension);
                    }
                    else
                    {
                        //Add file check and then increment
                        filePathParasolid = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "\\tempOutput";
                        int iterator = 1;
                        while (System.IO.File.Exists(filePath + $".{extension}"))
                        {
                            filePathParasolid = filePathParasolid.Remove(filePath.Length - 1) + iterator.ToString();
                            iterator++;
                        }
                        filePathParasolid += $".{extension}";
                    }


                    if (oTranslator.HasSaveCopyAsOptions[activeDocument, oContext, oOptions])
                    {
                        //oOptions.Value["ApplicationProtocolType"] = 3;
                        //oOptions.Value["Author"] = activeDocument.PropertySets[3][24].Value;
                        ////oOptions.Value("Authorization") = ""
                        //oOptions.Value["Description"] = activeDocument.PropertySets[3][14].Value;
                        //oOptions.Value["Organization"] = activeDocument.PropertySets[2][3].Value;


                        oOptions.Value["Version"] = 20;

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
            else if ((activeDocument.IsAssemblyDocument() || activeDocument.IsWeldmentDocument()))
            {
                //Step for the assembly, only doing check to wrap things up
                if (activeDocument != null)
                {
                    if (!String.IsNullOrEmpty(activeDocument.FullFileName))
                    {
                        referencedDocumentObject = _inventorApplication.Documents.ItemByName[activeDocument.FullFileName];
                        filePathParasolid = RevisionHelper.addRevisionLetter(activeDocument, PathConverter.clearExtension(activeDocument), extension);
                    }
                    else
                    {
                        //Add file check and then increment
                        filePathParasolid = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "\\tempOutput";
                        int iterator = 1;
                        while (System.IO.File.Exists(filePath + $".{extension}"))
                        {
                            filePathParasolid = filePathParasolid.Remove(filePath.Length - 1) + iterator.ToString();
                            iterator++;
                        }
                        filePathParasolid += $".{extension}";
                    }

                    if (oTranslator.HasSaveCopyAsOptions[activeDocument, oContext, oOptions])
                    {
                        //oOptions.Value["ApplicationProtocolType"] = 3;
                        //oOptions.Value["Author"] = activeDocument.PropertySets[3][24].Value;
                        ////oOptions.Value("Authorization") = ""
                        //oOptions.Value["Description"] = activeDocument.PropertySets[3][14].Value;
                        //oOptions.Value["Organization"] = activeDocument.PropertySets[2][3].Value;
                        oTranslator.ShowSaveCopyAsOptions(activeDocument, oContext, oOptions);

                        NameValueMap oPt = oOptions;
                        for (int i = 1; i < oPt.Count + 1; i++)
                        {
                            var nam = oPt.Name[i];
                            var val = oPt.Value[nam];
                        }
                        //????
                        oOptions.Value["Version"] = "20";

                        try
                        {
                            oTranslator.SaveCopyAs(activeDocument, oContext, oOptions, oDataMedium);
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message + "\n" + e.Source + "\n" + e.StackTrace + "\nAddIn: Sedenum Pack\nMethod: CreatePdfStep");
                        }
                    }
                }
                //Steps for the parts
                if (includeParts)
                {
                    foreach (Document oFD in activeDocument.ReferencedDocuments)
                    {//Check for every referenced document in the drawing and create step file of each

                        if (!String.IsNullOrEmpty(oFD.FullFileName))
                        {
                            //It seems that to get the drawing you would need to search in the same folder for the file with the same name as a drawing
                            referencedDocumentObject = _inventorApplication.Documents.ItemByName[oFD.FullFileName]; //Why do i need that as well????
                            filePathParasolid = RevisionHelper.addRevisionLetter(oFD, PathConverter.clearExtension(oFD), extension);

                            if (oTranslator.HasSaveCopyAsOptions[oFD, oContext, oOptions])
                            {
                                //oOptions.Value["ApplicationProtocolType"] = 3;
                                //oOptions.Value["Author"] = activeDocument.PropertySets[3][24].Value;
                                ////oOptions.Value("Authorization") = ""
                                //oOptions.Value["Description"] = activeDocument.PropertySets[3][14].Value;
                                //oOptions.Value["Organization"] = activeDocument.PropertySets[2][3].Value;


                                oOptions.Value["Version"] = 20;

                                try
                                {
                                    oTranslator.SaveCopyAs(referencedDocumentObject, oContext, oOptions, oDataMedium);
                                    if (referencedDocumentObject != _inventorApplication.ActiveDocument) referencedDocumentObject.Close();
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
            else if (activeDocument.IsPartDocument() || activeDocument.IsSheetMetalDocument())
            {
                if (!String.IsNullOrEmpty(activeDocument.FullFileName))
                {
                    referencedDocumentObject = _inventorApplication.Documents.ItemByName[activeDocument.FullFileName];
                    filePathParasolid = RevisionHelper.addRevisionLetter(activeDocument, PathConverter.clearExtension(activeDocument), extension);
                }
                else
                {
                    //Add file check and then increment
                    filePathParasolid = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "\\tempOutput";
                    int iterator = 1;
                    while (System.IO.File.Exists(filePath + $".{extension}"))
                    {
                        filePathParasolid = filePathParasolid.Remove(filePath.Length - 1) + iterator.ToString();
                        iterator++;
                    }
                    filePathParasolid += $".{extension}";
                }

                if (oTranslator.HasSaveCopyAsOptions[activeDocument, oContext, oOptions])
                {
                    //oOptions.Value["ApplicationProtocolType"] = 3;
                    //oOptions.Value["Author"] = activeDocument.PropertySets[3][24].Value;
                    ////oOptions.Value("Authorization") = ""
                    //oOptions.Value["Description"] = activeDocument.PropertySets[3][14].Value;
                    //oOptions.Value["Organization"] = activeDocument.PropertySets[2][3].Value;


                    oOptions.Value["Version"] = 20;

                    oDataMedium.FileName = filePathParasolid;

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
    }
}
