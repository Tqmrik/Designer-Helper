﻿using Inventor;
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
        private Document activeDocument
        {
            get
            {
                return InventorApplication.ActiveDocument;
            }
        }
        private string filePath
        {
            get
            {
                return activeDocument.FullDocumentName;
            }
        }
        private string revisionLetter
        {
            get
            {
                return activeDocument.PropertySets[1][7].Value.ToString();
            }

        }
        public void createPDF()
        {
            if (!activeDocument.isDrawingDocument())
            {
                return;
            }
            TranslatorAddIn pdfTranslator = (TranslatorAddIn)InventorApplication.ApplicationAddIns.ItemById["{0AC6FD96-2F4D-42CE-8BE0-8AEA580399E4}"];

            TranslationContext oContext = InventorApplication.TransientObjects.CreateTranslationContext();
            oContext.Type = IOMechanismEnum.kFileBrowseIOMechanism; //Just specifies the type of the operation

            NameValueMap oOptions = InventorApplication.TransientObjects.CreateNameValueMap();//??

            DataMedium oDataMedium = InventorApplication.TransientObjects.CreateDataMedium();//???

            string filePath = this.filePath;

            if(activeDocument.isDrawingDocument())
            {
                if (!String.IsNullOrEmpty(activeDocument.FullDocumentName))
                {//If drawing is placed in the folder, save it to the folder as well
                    filePath = activeDocument.FullDocumentName.Replace(".idw", "");
                    if (!String.IsNullOrEmpty(revisionLetter))
                    {
                        filePath += $"_{revisionLetter}.pdf";
                    }
                    else
                    {
                        filePath += ".pdf";
                    }
                }
                else
                {//If drawing is new place the files to the desktop without overwriting them
                 //Add file check and then increment
                    filePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "\\tempOutput";
                    int iterator = 1;
                    while (System.IO.File.Exists(filePath + ".pdf"))
                    {
                        filePath = filePath.Remove(filePath.Length - 1) + iterator.ToString();
                        iterator++;
                    }
                    filePath += ".pdf";
                }


                if (pdfTranslator.HasSaveCopyAsOptions[activeDocument, oContext, oOptions])
                {
                    oOptions.Value["All_Color_AS_Black"] = 0;
                    //TODO: What are the other options?
                }

                oDataMedium.FileName = filePath;

                try
                {
                    //Will adding the transaction alter the operation????
                    //TODO: Check if document is opened if so: 1)Try to close(kinda intrusive); 2)don't perform an export and display message
                    pdfTranslator.SaveCopyAs(activeDocument, oContext, oOptions, oDataMedium);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message + "\n" + e.Source + "\n" + e.StackTrace + "\nAddIn: Sedenum Pack\nMethod: CreatePdfStep");

                }
            }
            else if(activeDocument.isAssemblyDocument() || activeDocument.isPartDocument() || activeDocument.isWeldmentDocument() || activeDocument.isSheetMetalDocument())
            {

            }
            
        }
        public void createSTEP()
        {

            TranslatorAddIn oSTEPTranslator = (TranslatorAddIn)InventorApplication.ApplicationAddIns.ItemById["{90AF7F40-0C01-11D5-8E83-0010B541CD80}"];

            TranslationContext oContext = InventorApplication.TransientObjects.CreateTranslationContext();
            oContext.Type = IOMechanismEnum.kFileBrowseIOMechanism; //Just specifies the type of the operation

            NameValueMap oOptions = InventorApplication.TransientObjects.CreateNameValueMap();//??

            DataMedium oDataMedium = InventorApplication.TransientObjects.CreateDataMedium();//???

            string filePath = this.filePath;
            string filePathStep = "";
            Document referencedDoc = null;

            if (oSTEPTranslator.Equals(null))
            {
                MessageBox.Show("Couldn't connect to the step translator");
                return;
            }

            if (activeDocument.isDrawingDocument())//Drawing
            {
                foreach (ReferencedFileDescriptor oFD in activeDocument.ReferencedFileDescriptors)
                {//Check for every referenced document in the drawing and create step file of each

                    if (!String.IsNullOrEmpty(oFD.FullFileName))
                    {
                        referencedDoc = InventorApplication.Documents.ItemByName[oFD.FullFileName];
                        filePathStep = oFD.FullFileName;
                        filePathStep = filePathStep.Replace(".iam", "");
                        filePathStep = filePathStep.Replace(".ipt", "");

                        if (!String.IsNullOrEmpty(revisionLetter))
                        {
                            filePathStep += $"_{revisionLetter}.stp";
                        }
                        else
                        {
                            filePathStep += ".stp";
                        }

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
                        //Set application protocol.
                        //2 = AP 203 - Configuration Controlled Design
                        //3 = AP 214 - Automotive Design
                        oOptions.Value["ApplicationProtocolType"] = 3;

                        //Other options...
                        //oOptions.Value("Author") = ""
                        //oOptions.Value("Authorization") = ""
                        //oOptions.Value("Description") = ""
                        //oOptions.Value("Organization") = ""

                        //TODO: Browse the possible options


                        //DataMedium oStepDataMedium = InventorApplication.TransientObjects.CreateDataMedium();//???

                        oDataMedium.FileName = filePathStep;

                        try
                        {
                            //Will adding the transaction alter the operation????
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
            else if ((activeDocument.isAssemblyDocument() || activeDocument.isWeldmentDocument()) && includeBottomDocuments)
            {
                foreach (ReferencedFileDescriptor oFD in activeDocument.ReferencedFileDescriptors)
                {//Check for every referenced document in the drawing and create step file of each

                    if (!String.IsNullOrEmpty(oFD.FullFileName))
                    {
                        referencedDoc = InventorApplication.Documents.ItemByName[oFD.FullFileName];
                        filePathStep = oFD.FullFileName;
                        filePathStep = filePathStep.Replace(".iam", "");
                        filePathStep = filePathStep.Replace(".ipt", "");

                        if (!String.IsNullOrEmpty(revisionLetter))
                        {
                            filePathStep += $"_{revisionLetter}.stp";
                        }
                        else
                        {
                            filePathStep += ".stp";
                        }

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
                        //Set application protocol.
                        //2 = AP 203 - Configuration Controlled Design
                        //3 = AP 214 - Automotive Design
                        oOptions.Value["ApplicationProtocolType"] = 3;

                        //Other options...
                        //oOptions.Value("Author") = ""
                        //oOptions.Value("Authorization") = ""
                        //oOptions.Value("Description") = ""
                        //oOptions.Value("Organization") = ""

                        //TODO: Browse the possible options


                        //DataMedium oStepDataMedium = InventorApplication.TransientObjects.CreateDataMedium();//???

                        oDataMedium.FileName = filePathStep;

                        try
                        {
                            //Will adding the transaction alter the operation????
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
            else if (activeDocument.isPartDocument() || activeDocument.isSheetMetalDocument() || activeDocument.isAssemblyDocument())
            {
                if (!String.IsNullOrEmpty(activeDocument.FullFileName))
                {
                    referencedDoc = InventorApplication.Documents.ItemByName[activeDocument.FullFileName];
                    filePathStep = activeDocument.FullFileName;
                    filePathStep = filePathStep.Replace(".iam", "");
                    filePathStep = filePathStep.Replace(".ipt", "");

                    if (!String.IsNullOrEmpty(revisionLetter))
                    {
                        filePathStep += $"_{revisionLetter}.stp";
                    }
                    else
                    {
                        filePathStep += ".stp";
                    }

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
                    //Set application protocol.
                    //2 = AP 203 - Configuration Controlled Design
                    //3 = AP 214 - Automotive Design
                    oOptions.Value["ApplicationProtocolType"] = 3;

                    //Other options...
                    //oOptions.Value("Author") = ""
                    //oOptions.Value("Authorization") = ""
                    //oOptions.Value("Description") = ""
                    //oOptions.Value("Organization") = ""

                    //TODO: Browse the possible options


                    //DataMedium oStepDataMedium = InventorApplication.TransientObjects.CreateDataMedium();//???

                    oDataMedium.FileName = filePathStep;

                    try
                    {
                        //Will adding the transaction alter the operation????
                        oSTEPTranslator.SaveCopyAs(referencedDoc, oContext, oOptions, oDataMedium);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message + "\n" + e.Source + "\n" + e.StackTrace + "\nAddIn: Sedenum Pack\nMethod: CreatePdfStep");

                    }

                }
            }

        }
        public void createFlatDXF()
        {
            if (!(activeDocument.isSheetMetalDocument() || activeDocument.isDrawingDocument()))
            {
                return;
            }
            string filePath = this.filePath;
            string filePathDXF = "";
            PartDocument referencedDoc = null;
            SheetMetalComponentDefinition oSMDef = null;
            DataIO oDataIO = null;

            if (activeDocument.isDrawingDocument())
            {


                foreach (ReferencedFileDescriptor oFD in activeDocument.ReferencedFileDescriptors)
                {//Check for every referenced document in the drawing and create step file of each

                    if (!String.IsNullOrEmpty(oFD.FullFileName))
                    {
                        referencedDoc = (PartDocument)InventorApplication.Documents.ItemByName[oFD.FullFileName];

                        if (!(referencedDoc.SubType == "{9C464203-9BAE-11D3-8BAD-0060B0CE6BB4}"))
                        {
                            return;
                        }

                        oSMDef = (SheetMetalComponentDefinition)referencedDoc.ComponentDefinition;
                        oDataIO = oSMDef.DataIO;

                        filePathDXF = oFD.FullFileName;
                        filePathDXF = filePathDXF.Replace(".iam", "");
                        filePathDXF = filePathDXF.Replace(".ipt", "");

                        if (!String.IsNullOrEmpty(revisionLetter))
                        {
                            filePathDXF += $"_{revisionLetter}.dxf";
                        }
                        else
                        {
                            filePathDXF += ".dxf";

                            // TODO: Method to get rid of the extension
                        }
                    }
                    else
                    {
                        //Add file check and then increment
                        filePathDXF = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "\\tempOutput";
                        int iterator = 1;
                        while (System.IO.File.Exists(filePath + ".stp"))
                        {
                            filePathDXF = filePathDXF.Remove(filePath.Length - 1) + iterator.ToString();
                            iterator++;
                        }
                        filePathDXF += ".dxf";

                        if (!oSMDef.HasFlatPattern)
                        {
                            oSMDef.Unfold();
                            oSMDef.FlatPattern.ExitEdit();

                            //help source on string build: https://www.cadforum.cz/en/export-unfolds-of-sheetmetal-parts-to-dxf-parameters-for-ilogic-
                            string sOut = "FLAT PATTERN DXF?AcadVersion=R12&OuterProfileLayer=Outer&BendLayer=Bend&OuterProfileLayerColor=0;0;0&BendUpLayerColor=0;0;0&BendUpLayerLineType=37638&BendDownLayerColor=0;0;0&TrimCenterlinesAtContour=True&InvisibleLayers=IV_TANGENT";
                            oDataIO.WriteDataToFile(sOut, filePathDXF);
                        }
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

                    filePathDXF = activeDocument.FullFileName;
                    filePathDXF = filePathDXF.Replace(".iam", "");
                    filePathDXF = filePathDXF.Replace(".ipt", "");

                    if (!String.IsNullOrEmpty(revisionLetter))
                    {
                        filePathDXF += $"_{revisionLetter}.dxf";
                    }
                    else
                    {
                        filePathDXF += ".dxf";

                        // TODO: Method to get rid of the extension
                    }
                }
                else
                {
                    //Add file check and then increment
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
                }
                //help source on string build: https://www.cadforum.cz/en/export-unfolds-of-sheetmetal-parts-to-dxf-parameters-for-ilogic-
                string sOut = "FLAT PATTERN DXF?AcadVersion=R12&OuterProfileLayer=Outer&BendLayer=Bend&OuterProfileLayerColor=0;0;0&BendUpLayerColor=0;0;0&BendUpLayerLineType=37638&BendDownLayerColor=0;0;0&TrimCenterlinesAtContour=True&InvisibleLayers=IV_TANGENT&RebaseGeometry=True";
                oDataIO.WriteDataToFile(sOut, filePathDXF);
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

            foreach (ReferencedFileDescriptor oFD in activeDocument.ReferencedFileDescriptors)
            {//Check for every referenced document in the drawing and create step file of each

                if (oSTEPTranslator.Equals(null))
                {
                    MessageBox.Show("Couldn't connect to the step translator");
                    return;
                }

                if (!String.IsNullOrEmpty(oFD.FullFileName))
                {
                    referencedDoc = InventorApplication.Documents.ItemByName[oFD.FullFileName];
                    filePathStep = oFD.FullFileName;
                    filePathStep = filePathStep.Replace(".iam", "");
                    filePathStep = filePathStep.Replace(".ipt", "");

                    if (!String.IsNullOrEmpty(revisionLetter))
                    {
                        filePathStep += $"_{revisionLetter}.stp";
                    }
                    else
                    {
                        filePathStep += ".stp";
                    }

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


