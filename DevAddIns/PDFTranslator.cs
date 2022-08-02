using Inventor;
using System;
using System.Windows.Forms;

namespace DevAddIns
{
    class PDFTranslator : Translators
    {
        string filePath;
        const string extension = "pdf";

        public PDFTranslator() : base()
        {
            oTranslator = (TranslatorAddIn)InventorApplication.ApplicationAddIns.ItemById["{0AC6FD96-2F4D-42CE-8BE0-8AEA580399E4}"];
        }
        public void createPDF(Document document)
        {
            oContext.Type = IOMechanismEnum.kFileBrowseIOMechanism;

            Document drawingDocumentObject = null;

            string referencedDocumentDrawingPath = null;
            string currentAssemblyDrawingPath = null;



            if (oTranslator.Equals(null))
            {
                MessageBox.Show("Couldn't connect to the PDF translator");
                return;
            }

            if (document.isDrawingDocument())
            {
                filePathHelper(document);


                if (oTranslator.HasSaveCopyAsOptions[document, oContext, oOptions])
                {
                    oOptionsSetter();
                }

                oDataMedium.FileName = filePath;

                try
                {
                    //TODO: Check if document is opened if so: 1)Try to close(kinda intrusive); 2)don't perform an export and display message
                    oTranslator.SaveCopyAs(activeDocument, oContext, oOptions, oDataMedium);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message + "\n" + e.Source + "\n" + e.StackTrace + "\nAddIn: Sedenum Pack\nMethod: CreatePdfStep");

                }
            }
            else if (document.isAssemblyDocument() || document.isWeldmentDocument())
            {
                if(packAssembly)
                {
                    TestMeth(document);
                    foreach (Document dasd in document.AllReferencedDocuments)
                    {
                        TestMeth(dasd);
                    }
                    return;
                }
                //Make pdf for the assembly drawing
                if (activeDocument != null)
                {
                    if (((AssemblyDocument)activeDocument).ComponentDefinition.BOMStructure == BOMStructureEnum.kPurchasedBOMStructure) //Check to see if the part purchased or not
                    {

                    }
                    else
                    {
                        if (!String.IsNullOrEmpty(activeDocument.FullFileName))
                        {
                            string assemblyDirectory = System.IO.Path.GetDirectoryName(activeDocument.FullDocumentName);
                            string assemblyDrawingPath = PathConverter.guessDrawingPath(activeDocument);
                            currentAssemblyDrawingPath = InventorApplication.DesignProjectManager.ResolveFile(assemblyDirectory, assemblyDrawingPath);


                            if (String.IsNullOrEmpty(currentAssemblyDrawingPath))//Make more advanced directory search??
                            {
                                foreach (System.IO.DirectoryInfo directory in new System.IO.DirectoryInfo(assemblyDirectory).GetDirectories())
                                {
                                    if (currentAssemblyDrawingPath == null)
                                    {
                                        currentAssemblyDrawingPath = InventorApplication.DesignProjectManager.ResolveFile(directory.FullName, assemblyDrawingPath);
                                    }
                                    else break;
                                }
                            }


                            if (!String.IsNullOrEmpty(currentAssemblyDrawingPath))
                            {
                                drawingDocumentObject = InventorApplication.Documents.Open(currentAssemblyDrawingPath, OpenVisible: false);
                                filePath = RevisionHelper.addRevisionLetter(drawingDocumentObject, PathConverter.clearExtension(drawingDocumentObject), extension);

                                if (oTranslator.HasSaveCopyAsOptions[drawingDocumentObject, oContext, oOptions])
                                {
                                    oOptionsSetter();
                                }

                                oDataMedium.FileName = filePath;

                                try
                                {
                                    oTranslator.SaveCopyAs(drawingDocumentObject, oContext, oOptions, oDataMedium);
                                }
                                catch (Exception e)
                                {
                                    MessageBox.Show(e.Message + "\n" + e.Source + "\n" + e.StackTrace + "\nAddIn: Sedenum Pack\nMethod: CreatePdfStep");

                                }
                            }

                            else
                            {
                                MessageBox.Show("Wasn't able to find an assembly drawing");
                            }
                        }
                    }
                }
                //Make pdfs for the parts drawings
                if (includeParts)
                {
                    foreach (Document referencedDocument in activeDocument.ReferencedDocuments)
                    {
                        if (referencedDocument.isAssemblyDocument())
                        {
                            if (((AssemblyDocument)referencedDocument).ComponentDefinition.BOMStructure == BOMStructureEnum.kPurchasedBOMStructure) //Check to see if the part purchased or not
                            {
                                continue;
                            }
                        }
                        else if (referencedDocument.isPartDocument())
                        {
                            if (((PartDocument)referencedDocument).ComponentDefinition.BOMStructure == BOMStructureEnum.kPurchasedBOMStructure) //Check to see if the part purchased or not
                            {
                                continue;
                            }
                        }

                        string sourcePartPath = System.IO.Path.GetDirectoryName(referencedDocument.FullDocumentName);
                        string tempDrawingFilePath = PathConverter.guessDrawingPath(referencedDocument);

                        if (!String.IsNullOrEmpty(tempDrawingFilePath)) //If there is invalid path
                        {
                            referencedDocumentDrawingPath = InventorApplication.DesignProjectManager.ResolveFile(sourcePartPath, tempDrawingFilePath);
                        }
                        else continue;


                        if (String.IsNullOrEmpty(referencedDocumentDrawingPath))//Make more advanced directory search??
                        {
                            foreach (System.IO.DirectoryInfo directory in new System.IO.DirectoryInfo(sourcePartPath).GetDirectories())
                            {
                                if (referencedDocumentDrawingPath == null)
                                {
                                    referencedDocumentDrawingPath = InventorApplication.DesignProjectManager.ResolveFile(directory.FullName, tempDrawingFilePath);
                                }
                                else break;
                            }
                        }

                        //If file path was finally found tries to convert it
                        if (!String.IsNullOrEmpty(referencedDocumentDrawingPath))
                        {//If drawing is placed in the folder, save it to the folder as well
                            drawingDocumentObject = InventorApplication.Documents.Open(referencedDocumentDrawingPath, OpenVisible: false);
                            filePath = RevisionHelper.addRevisionLetter(drawingDocumentObject, PathConverter.clearExtension(drawingDocumentObject), extension);

                            if (oTranslator.HasSaveCopyAsOptions[drawingDocumentObject, oContext, oOptions])
                            {
                                oOptionsSetter();
                            }

                            oDataMedium.FileName = filePath;

                            try
                            {
                                //TODO: Check if document is opened if so: 1)Try to close(kinda intrusive); 2)don't perform an export and display message
                                oTranslator.SaveCopyAs(drawingDocumentObject, oContext, oOptions, oDataMedium);
                            }
                            catch (Exception e)
                            {
                                MessageBox.Show(e.Message + "\n" + e.Source + "\n" + e.StackTrace + "\nAddIn: Sedenum Pack\nMethod: CreatePdfStep");

                            }
                        }
                        //Else just skips the part
                        else
                        {
                            continue;
                        }
                    }
                }
            }
            else if (document.isPartDocument() || document.isSheetMetalDocument())
            {
                if (((PartDocument)activeDocument).ComponentDefinition.BOMStructure == BOMStructureEnum.kPurchasedBOMStructure) //Check to see if the part purchased or not
                {

                }
                else
                {
                    string partDirectory = System.IO.Path.GetDirectoryName(document.FullDocumentName);
                    string partDrawingPath = PathConverter.guessDrawingPath(document);

                    if (!String.IsNullOrEmpty(partDrawingPath)) //If there is invalid path
                    {
                        referencedDocumentDrawingPath = InventorApplication.DesignProjectManager.ResolveFile(partDirectory, partDrawingPath);
                        //Resolve file also searches all the subdirectories that it could find
                    }
                    else return;

                    if (!String.IsNullOrEmpty(referencedDocumentDrawingPath))
                    {
                        //If drawing is placed in the folder, save it to the folder as well
                        drawingDocumentObject = InventorApplication.Documents.Open(referencedDocumentDrawingPath, OpenVisible: false);
                        filePath = RevisionHelper.addRevisionLetter(drawingDocumentObject, PathConverter.clearExtension(drawingDocumentObject), extension);

                        if (oTranslator.HasSaveCopyAsOptions[drawingDocumentObject, oContext, oOptions])
                        {
                            oOptionsSetter();
                        }
                        oDataMedium.FileName = filePath;
                        try
                        {                          
                            oTranslator.SaveCopyAs(drawingDocumentObject, oContext, oOptions, oDataMedium);
                            drawingDocumentObject.Close();
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message + "\n" + e.Source + "\n" + e.StackTrace + "\nAddIn: Sedenum Pack\nMethod: CreatePdfStep");
                        }
                    }
                    else
                    {
                        return;
                    }
                }
            }

            oTranslator = null;
            oContext = null;
            oDataMedium = null;
            oDataMedium = null;
        }

        private void filePathHelper(Document doc) //Add a revision letter to the output file name
        {
            string filePath;
            if (!String.IsNullOrEmpty(doc.FullDocumentName))
            {
                //Add revision letter to the file name
                filePath = RevisionHelper.addRevisionLetter(doc, PathConverter.clearExtension(activeDocument), extension);
            }
            else
            {
                //Try to save to the desktop
                filePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);
                int iterator = 1;
                while (System.IO.File.Exists(filePath + $".{extension}"))
                {
                    filePath = filePath.Remove(filePath.Length - 1) + $"{iterator}";
                    iterator++;
                }
                filePath += $".{extension}";
            }
        }

        private void oOptionsSetter()
        {
            oOptions.Value["All_Color_AS_Black"] = 0;
            oOptions.Value["Remove_Line_Weights"] = 0;

            //oOptions.Value["Password"] = 0;

            //oOptions.Value["Custom_Begin_Sheet"] = 1;
            //oOptions.Value["Custom_End_Sheet"] = 1;

            //oOptions.Value["Vector_Resolution"] = 400;
        }

        private void TestMeth(Document document)
        {
            Document drawingDocumentObject = null;
            string referencedDocumentDrawingPath = null;
            string currentAssemblyDrawingPath = null;
            string partDirectory = System.IO.Path.GetDirectoryName(document.FullDocumentName);
            string partDrawingPath = PathConverter.guessDrawingPath(document);

            if (!String.IsNullOrEmpty(partDrawingPath)) //If there is invalid path
            {
                referencedDocumentDrawingPath = InventorApplication.DesignProjectManager.ResolveFile(partDirectory, partDrawingPath);
                //Resolve file also searches all the subdirectories that it could find
            }
            else return;

            if (!String.IsNullOrEmpty(referencedDocumentDrawingPath))
            {
                //If drawing is placed in the folder, save it to the folder as well
                drawingDocumentObject = InventorApplication.Documents.Open(referencedDocumentDrawingPath, OpenVisible: false);
                filePath = RevisionHelper.addRevisionLetter(drawingDocumentObject, PathConverter.clearExtension(drawingDocumentObject), extension);

                if (oTranslator.HasSaveCopyAsOptions[drawingDocumentObject, oContext, oOptions])
                {
                    oOptionsSetter();
                }
                oDataMedium.FileName = filePath;
                try
                {
                    oTranslator.SaveCopyAs(drawingDocumentObject, oContext, oOptions, oDataMedium);
                    drawingDocumentObject.Close();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message + "\n" + e.Source + "\n" + e.StackTrace + "\nAddIn: Sedenum Pack\nMethod: CreatePdfStep");
                }
            }
            else
            {
                return;
            }
        }


    }
}
