using Inventor;
using System;
using System.Windows.Forms;



namespace DevAddIns
{
    class Translators
    {
        public bool includeBottomDocuments = false;
        private static Inventor.Application m_inventorApplication;

        public static Inventor.Application InventorApplication
        {
            set
            {
                m_inventorApplication = value;
            }
            get
            {
                return m_inventorApplication;
            }
        }

        public bool includeParts { get; set; }
        private Document activeDocument
        {
            get
            {
                return InventorApplication.ActiveDocument;
            }
        }
        private string filePath //Why do i need that again???
        {
            get
            {
                return activeDocument.FullDocumentName;
            }
        }

        public void createPDF()
        {
            TranslatorAddIn oPDFTranslator = (TranslatorAddIn)InventorApplication.ApplicationAddIns.ItemById["{0AC6FD96-2F4D-42CE-8BE0-8AEA580399E4}"];
            TranslationContext oContext = InventorApplication.TransientObjects.CreateTranslationContext();
            oContext.Type = IOMechanismEnum.kFileBrowseIOMechanism;
            NameValueMap oOptions = InventorApplication.TransientObjects.CreateNameValueMap();
            DataMedium oDataMedium = InventorApplication.TransientObjects.CreateDataMedium();

            Document drawingDocumentObject = null;

            string referencedDocumentDrawingPath = null;
            string currentAssemblyDrawingPath = null;

            string filePath = this.filePath;

            if (oPDFTranslator.Equals(null))
            {
                MessageBox.Show("Couldn't connect to the PDF translator");
                return;
            }

            if (activeDocument.isDrawingDocument())
            {
                if (!String.IsNullOrEmpty(activeDocument.FullDocumentName))
                {
                    //Add revision letter to the file name
                    filePath = RevisionHelper.addRevisionLetter(activeDocument, PathConverter.clearExtension(activeDocument), "pdf");
                }

                else
                {
                    //Try to save to the desktop
                    filePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "\\tempOutput";
                    int iterator = 1;
                    while (System.IO.File.Exists(filePath + ".pdf"))
                    {
                        filePath = filePath.Remove(filePath.Length - 1) + iterator.ToString();
                        iterator++;
                    }
                    filePath += ".pdf";
                }


                if (oPDFTranslator.HasSaveCopyAsOptions[activeDocument, oContext, oOptions])
                {
                    oOptions.Value["All_Color_AS_Black"] = 0;
                }

                oDataMedium.FileName = filePath;

                try
                {
                    //TODO: Check if document is opened if so: 1)Try to close(kinda intrusive); 2)don't perform an export and display message
                    oPDFTranslator.SaveCopyAs(activeDocument, oContext, oOptions, oDataMedium);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message + "\n" + e.Source + "\n" + e.StackTrace + "\nAddIn: Sedenum Pack\nMethod: CreatePdfStep");

                }
            }
            else if (activeDocument.isAssemblyDocument() || activeDocument.isWeldmentDocument())
            {
                //Make pdf for the assembly drawing
                if(!String.IsNullOrEmpty(activeDocument.FullFileName))
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
                        filePath = RevisionHelper.addRevisionLetter(drawingDocumentObject, PathConverter.clearExtension(drawingDocumentObject), "pdf");

                        if (oPDFTranslator.HasSaveCopyAsOptions[drawingDocumentObject, oContext, oOptions])
                        {
                            oOptions.Value["All_Color_AS_Black"] = 0;
                        }

                        oDataMedium.FileName = filePath;

                        try
                        {
                            oPDFTranslator.SaveCopyAs(drawingDocumentObject, oContext, oOptions, oDataMedium);
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
                //Make pdfs for the parts drawings
                if (includeParts)
                {
                    foreach (Document referencedDocument in activeDocument.ReferencedDocuments)
                    {
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
                            filePath = RevisionHelper.addRevisionLetter(drawingDocumentObject, PathConverter.clearExtension(drawingDocumentObject), "pdf");

                            if (oPDFTranslator.HasSaveCopyAsOptions[drawingDocumentObject, oContext, oOptions])
                            {
                                oOptions.Value["All_Color_AS_Black"] = 0;
                            }

                            oDataMedium.FileName = filePath;

                            try
                            {
                                //TODO: Check if document is opened if so: 1)Try to close(kinda intrusive); 2)don't perform an export and display message
                                oPDFTranslator.SaveCopyAs(drawingDocumentObject, oContext, oOptions, oDataMedium);
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
            else if (activeDocument.isPartDocument() || activeDocument.isSheetMetalDocument())
            {
                string partDirectory = System.IO.Path.GetDirectoryName(activeDocument.FullDocumentName);
                string partDrawingPath = PathConverter.guessDrawingPath(activeDocument);
                referencedDocumentDrawingPath = InventorApplication.DesignProjectManager.ResolveFile(partDirectory, partDrawingPath);

                if (!String.IsNullOrEmpty(partDrawingPath)) //If there is invalid path
                {
                    referencedDocumentDrawingPath = InventorApplication.DesignProjectManager.ResolveFile(partDirectory, partDrawingPath);
                }
                else return;

                if (String.IsNullOrEmpty(referencedDocumentDrawingPath))//Make more advanced directory search??
                {
                    foreach (System.IO.DirectoryInfo directory in new System.IO.DirectoryInfo(partDirectory).GetDirectories())
                    {
                        if (referencedDocumentDrawingPath == null)
                        {
                            referencedDocumentDrawingPath = InventorApplication.DesignProjectManager.ResolveFile(directory.FullName, partDrawingPath);
                        }
                        else break;
                    }
                }

                if (!String.IsNullOrEmpty(referencedDocumentDrawingPath))
                {//If drawing is placed in the folder, save it to the folder as well
                    drawingDocumentObject = InventorApplication.Documents.Open(referencedDocumentDrawingPath, OpenVisible: false);
                    filePath = RevisionHelper.addRevisionLetter(drawingDocumentObject, PathConverter.clearExtension(drawingDocumentObject), "pdf");

                    if (oPDFTranslator.HasSaveCopyAsOptions[drawingDocumentObject, oContext, oOptions])
                    {
                        oOptions.Value["All_Color_AS_Black"] = 0;
                        //TODO: What are the other options?
                    }

                    oDataMedium.FileName = filePath;

                    try
                    {
                        //Will adding the transaction alter the operation????
                        //TODO: Check if document is opened if so: 1)Try to close(kinda intrusive); 2)don't perform an export and display message
                        oPDFTranslator.SaveCopyAs(drawingDocumentObject, oContext, oOptions, oDataMedium);
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

            oPDFTranslator = null;
            oContext = null;
            oDataMedium = null;
            oDataMedium = null;
        }
        public void createSTEP()
        {
            //Wrap in the try/catch???

            TranslatorAddIn oSTEPTranslator = (TranslatorAddIn)InventorApplication.ApplicationAddIns.ItemById["{90AF7F40-0C01-11D5-8E83-0010B541CD80}"];
            TranslationContext oContext = InventorApplication.TransientObjects.CreateTranslationContext();
            oContext.Type = IOMechanismEnum.kFileBrowseIOMechanism; 
            NameValueMap oOptions = InventorApplication.TransientObjects.CreateNameValueMap();
            DataMedium oDataMedium = InventorApplication.TransientObjects.CreateDataMedium();

            string filePath = this.filePath;
            string filePathStep = "";

            Document referencedDocumentObject = null;

            if (oSTEPTranslator.Equals(null))
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
                        filePathStep = RevisionHelper.addRevisionLetter(referencedDocumentObject, PathConverter.clearExtension(referencedDocumentObject), "stp");
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


                    if (oSTEPTranslator.HasSaveCopyAsOptions[activeDocument, oContext, oOptions])
                    {
                        oOptions.Value["ApplicationProtocolType"] = 3;
                        oOptions.Value["Author"] = oFD.PropertySets[3][24].Value;
                        //oOptions.Value("Authorization") = ""
                        oOptions.Value["Description"] = oFD.PropertySets[3][14].Value;
                        oOptions.Value["Organization"] = oFD.PropertySets[2][3].Value;

                        oDataMedium.FileName = filePathStep;

                        try
                        {
                            oSTEPTranslator.SaveCopyAs(referencedDocumentObject, oContext, oOptions, oDataMedium);
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

                    if (oSTEPTranslator.HasSaveCopyAsOptions[activeDocument, oContext, oOptions])
                    {
                        oOptions.Value["ApplicationProtocolType"] = 3;
                        oOptions.Value["Author"] = activeDocument.PropertySets[3][24].Value;
                        //oOptions.Value("Authorization") = ""
                        oOptions.Value["Description"] = activeDocument.PropertySets[3][14].Value;
                        oOptions.Value["Organization"] = activeDocument.PropertySets[2][3].Value;

                        oDataMedium.FileName = filePathStep;

                        try
                        {
                            oSTEPTranslator.SaveCopyAs(referencedDocumentObject, oContext, oOptions, oDataMedium);
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
                            referencedDocumentObject = InventorApplication.Documents.ItemByName[oFD.FullFileName]; //Why do i need that as well????
                            filePathStep = RevisionHelper.addRevisionLetter(oFD, PathConverter.clearExtension(oFD), "stp");

                            if (oSTEPTranslator.HasSaveCopyAsOptions[oFD, oContext, oOptions])
                            {
                                oOptions.Value["ApplicationProtocolType"] = 3;
                                oOptions.Value["Author"] = oFD.PropertySets[3][24].Value;
                                //oOptions.Value("Authorization") = ""
                                oOptions.Value["Description"] = oFD.PropertySets[3][14].Value;
                                oOptions.Value["Organization"] = oFD.PropertySets[2][3].Value;

                                oDataMedium.FileName = filePathStep;

                                try
                                {
                                    oSTEPTranslator.SaveCopyAs(referencedDocumentObject, oContext, oOptions, oDataMedium);
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

                if (oSTEPTranslator.HasSaveCopyAsOptions[activeDocument, oContext, oOptions])
                {
                    oOptions.Value["ApplicationProtocolType"] = 3;
                    oOptions.Value["Author"] = activeDocument.PropertySets[3][24].Value;
                    //oOptions.Value("Authorization") = ""
                    oOptions.Value["Description"] = activeDocument.PropertySets[3][14].Value;
                    oOptions.Value["Organization"] = activeDocument.PropertySets[2][3].Value;

                    oDataMedium.FileName = filePathStep;

                    try
                    {
                        oSTEPTranslator.SaveCopyAs(referencedDocumentObject, oContext, oOptions, oDataMedium);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message + "\n" + e.Source + "\n" + e.StackTrace + "\nAddIn: Sedenum Pack\nMethod: CreatePdfStep");

                    }

                }
            }

            oSTEPTranslator = null;
            oContext = null;
            oDataMedium = null;
            oDataMedium = null;

        }
        public void createFlatDXF()
        {
            string filePath = this.filePath;
            string filePathDXF = "";
            PartDocument referencedDoc = null;
            SheetMetalComponentDefinition oSMDef = null;
            DataIO oDataIO = null;
            string sOut = "FLAT PATTERN DXF?AcadVersion=R12&OuterProfileLayer=Outer&BendLayer=Bend&OuterProfileLayerColor=0;0;0&BendUpLayerColor=0;0;0&BendUpLayerLineType=37638&BendDownLayerColor=0;0;0&TrimCenterlinesAtContour=True&InvisibleLayers=IV_TANGENT&RebaseGeometry=True";
            PartDocument oFD = null;

            if (activeDocument.isDrawingDocument())
            {
                foreach (Document oFDF in activeDocument.ReferencedDocuments)
                {//Check for every referenced document in the drawing and create step file of each

                    if (!String.IsNullOrEmpty(oFDF.FullFileName))
                    {
                        referencedDoc = (PartDocument)InventorApplication.Documents.ItemByName[oFDF.FullFileName];

                        if (!(referencedDoc.SubType == "{9C464203-9BAE-11D3-8BAD-0060B0CE6BB4}"))
                        {
                            return;
                        }

                        oSMDef = (SheetMetalComponentDefinition)referencedDoc.ComponentDefinition;
                        oDataIO = oSMDef.DataIO;

                        filePathDXF = RevisionHelper.addRevisionLetter(oFDF, PathConverter.clearExtension(oFDF), "dxf");
                    }
                    else
                    {
                        filePathDXF = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "\\tempOutput";
                        int iterator = 1;
                        while (System.IO.File.Exists(filePath + ".stp"))
                        {
                            filePathDXF = filePathDXF.Remove(filePath.Length - 1) + iterator.ToString();
                            iterator++;
                        }
                        filePathDXF += ".dxf";
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

                    if (!(referencedDoc.SubType == "{9C464203-9BAE-11D3-8BAD-0060B0CE6BB4}"))
                    {
                        return;
                    }

                    oSMDef = (SheetMetalComponentDefinition)referencedDoc.ComponentDefinition;
                    oDataIO = oSMDef.DataIO;

                    filePathDXF = RevisionHelper.addRevisionLetter(activeDocument, PathConverter.clearExtension(activeDocument), "dxf");
                }
                else
                {
                    //Add file check and then increment
                    filePathDXF = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "\\tempOutput";
                    int iterator = 1;
                    while (System.IO.File.Exists(filePath + ".dxf"))
                    {
                        filePathDXF = filePathDXF.Remove(filePath.Length - 1) + iterator.ToString();
                        iterator++;
                    }
                    filePathDXF += ".dxf";
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

                        else
                        {
                            oFD = (PartDocument)oFDF;
                        }

                        if (!String.IsNullOrEmpty(oFD.FullFileName))
                        {
                            referencedDoc = (PartDocument)InventorApplication.Documents.ItemByName[oFD.FullFileName];

                            if (!(referencedDoc.SubType == "{9C464203-9BAE-11D3-8BAD-0060B0CE6BB4}"))
                            {
                                continue;
                            }

                            oSMDef = (SheetMetalComponentDefinition)oFD.ComponentDefinition;
                            oDataIO = oSMDef.DataIO;

                            filePathDXF = RevisionHelper.addRevisionLetter(oFDF, PathConverter.clearExtension(oFDF), "dxf");

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
        public void createParasolid()
        {
            TranslatorAddIn oSTEPTranslator = (TranslatorAddIn)InventorApplication.ApplicationAddIns.ItemById["{93D506C4-8355-4E28-9C4E-C2B5F1EDC6AE}"];

            TranslationContext oContext = InventorApplication.TransientObjects.CreateTranslationContext();
            oContext.Type = IOMechanismEnum.kFileBrowseIOMechanism; //Just specifies the type of the operation

            NameValueMap oOptions = InventorApplication.TransientObjects.CreateNameValueMap();//??

            DataMedium oDataMedium = InventorApplication.TransientObjects.CreateDataMedium();//???

            string filePath = this.filePath;
            string filePathStep = "";
            Document referencedDoc = null;

            foreach (Document oFD in activeDocument.ReferencedDocuments)
            {//Check for every referenced document in the drawing and create step file of each

                if (oSTEPTranslator.Equals(null))
                {
                    MessageBox.Show("Couldn't connect to the step translator");
                    return;
                }

                if (!String.IsNullOrEmpty(oFD.FullFileName))
                {
                    referencedDoc = InventorApplication.Documents.ItemByName[oFD.FullFileName];
                    filePathStep = RevisionHelper.addRevisionLetter(oFD, PathConverter.clearExtension(oFD), "x_t");

                }
                else
                {
                    filePathStep = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "\\tempOutput";
                    int iterator = 1;
                    while (System.IO.File.Exists(filePath + ".stp"))
                    {
                        filePathStep = filePathStep.Remove(filePath.Length - 1) + iterator.ToString();
                        iterator++;
                    }
                    filePathStep += ".stp";
                }

                if (oSTEPTranslator.HasSaveCopyAsOptions[activeDocument, oContext, oOptions])
                {
                    oOptions.Value["ApplicationProtocolType"] = 3;
                    oDataMedium.FileName = filePathStep;

                    try
                    {
                        oSTEPTranslator.SaveCopyAs(referencedDoc, oContext, oOptions, oDataMedium);
                        referencedDoc.Close();
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


//Create method overrides that will take path as arguments???s

//TODO: Create a typicall translator class
//TODO: Add a file finder in the directories(use recursion until NULL)
//TODO: Change Forms so that they will look a bit presentable
//TODO: CheckBox for replacing existing files???


//PDF options:
//oOptions.Value("Remove_Line_Weights") = 0
//oOptions.Value("Vector_Resolution") = 400
//oOptions.Value("Sheet_Range") = kPrintAllSheets
//oOptions.Value("Custom_Begin_Sheet") = 2
//oOptions.Value("Custom_End_Sheet") = 4



//STEP options:
//Set application protocol.
//2 = AP 203 - Configuration Controlled Design
//3 = AP 214 - Automotive Design
//oOptions.Value["ApplicationProtocolType"] = 3;
//Other options...
//oOptions.Value("Author") = ""
//oOptions.Value("Authorization") = ""
//oOptions.Value("Description") = ""
//oOptions.Value("Organization") = ""