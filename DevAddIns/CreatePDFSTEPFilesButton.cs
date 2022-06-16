using Inventor;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DevAddIns
{
    internal class CreatePDFSTEPFilesButton : Button
    {
        #region "Constructors"
        //Use constructors of the base class
        public CreatePDFSTEPFilesButton(string displayName, string internalName, CommandTypesEnum commandType, string clientId, string description, string tooltip, Image standardIcon, Image largeIcon, ButtonDisplayEnum buttonDisplayType)
            : base(displayName, internalName, commandType, clientId, description, tooltip, standardIcon, largeIcon, buttonDisplayType)
        {

        }
        public CreatePDFSTEPFilesButton(string displayName, string internalName, CommandTypesEnum commandType, string clientId, string description, string tooltip, ButtonDisplayEnum buttonDisplayType)
            : base(displayName, internalName, commandType, clientId, description, tooltip, buttonDisplayType)
        {

        }
        #endregion


        private Document activeDocument
        {
            get
            {
                return InventorApplication.ActiveDocument;
            }
        }
        private string fPath
        {
            get
            {
                return activeDocument.FullDocumentName;
            }
        }
        private string revision
        {
            get
            {
                return activeDocument.PropertySets[1][7].Value.ToString();
            }

        }

        MultilanguageDictionary MLDict = new MultilanguageDictionary();


        #region "EventHandling"
        override protected void ButtonDefinition_OnExecute(NameValueMap context)
        {
            /*
             * A File object represents a storage in the file system (i.e. a file on disk). A Document object represents an instance of a model or drawing in memory. A Document can only have a single associated File object. However, since it is possible to have multiple instances (or level of detail representations) of a file in memory that are persisted in the same storage on the file system, multiple Document objects may be associated with the same File object.

            File and Document References
            A FileDescriptor object describes the reference from a File to another File. A DocumentDescriptor describes the reference from a Document to another Document. A descriptor contains all the information needed to find the referenced file/document as well as the state of the reference (healthy, unresolved, replaced, etc.). The File and FileDescriptor objects represent the consolidated view of all of the representations of a Document. The figure below shows the relationships between the FileDescriptor, File, DocumentDescriptor and Document objects.

             */
            

            //Get document that the drawing refernces
            //Create step file of such document

            //TODO: Wrap in the try's; check to see if it's possible to create step without opening the actual document; play with the algorith to see other errors in documents with null references and so on


            if (!(activeDocument.SubType == "{BBF9FDF1-52DC-11D0-8C04-0800090BE8EC}")) return;
            createPDF();
            createSTEP();

            //var refDocs = activeDocument.ReferencedDocuments.Type;
            //var refFiles = activeDocument.ReferencedFiles.Type;
            //var refDocsDesct = activeDocument.ReferencedDocumentDescriptors.Type;
            //var refFileDesct = activeDocument.ReferencedFileDescriptors.Type;
            //var referencingDocs = activeDocument.ReferencingDocuments.Type;

            //FileDescriptor fd = (FileDescriptor)activeDocument.ReferencedFileDescriptors; 
            #endregion
        }

        public void createPDF()
        {
            TranslatorAddIn pdfTranslator = (TranslatorAddIn)InventorApplication.ApplicationAddIns.ItemById["{0AC6FD96-2F4D-42CE-8BE0-8AEA580399E4}"];

            TranslationContext oContext = InventorApplication.TransientObjects.CreateTranslationContext();
            oContext.Type = IOMechanismEnum.kFileBrowseIOMechanism; //Just specifies the type of the operation

            NameValueMap oOptions = InventorApplication.TransientObjects.CreateNameValueMap();//??

            DataMedium oDataMedium = InventorApplication.TransientObjects.CreateDataMedium();//???

            string filePath = fPath;

            if (activeDocument.FullDocumentName != null)
            {//If drawing is placed in the folder, save it to the folder as well
                filePath = activeDocument.FullDocumentName.Replace(".idw", "");
                if (!String.IsNullOrEmpty(revision))
                {
                    filePath += $"_{revision}.pdf";
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
        public void createSTEP()
        {

            TranslatorAddIn oSTEPTranslator = (TranslatorAddIn)InventorApplication.ApplicationAddIns.ItemById["{90AF7F40-0C01-11D5-8E83-0010B541CD80}"];

            TranslationContext oContext = InventorApplication.TransientObjects.CreateTranslationContext();
            oContext.Type = IOMechanismEnum.kFileBrowseIOMechanism; //Just specifies the type of the operation

            NameValueMap oOptions = InventorApplication.TransientObjects.CreateNameValueMap();//??

            DataMedium oDataMedium = InventorApplication.TransientObjects.CreateDataMedium();//???

            string filePath = fPath;
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
                    //Allows to get the document without specifically opening it
                    //Test on assemblies


                    //foreach(var sd in referencedDoc.BrowserPanes.ActivePane.TopNode.BrowserNodes)
                    //{
                    //    var BN = sd;//Flat Pattern
                    //}

                    //Check type of the file afterwards by that int number in the type

                    filePathStep = oFD.FullFileName;
                    filePathStep = filePathStep.Replace(".iam", "");
                    filePathStep = filePathStep.Replace(".ipt", "");

                    if (!String.IsNullOrEmpty(revision))
                    {
                        filePathStep += $"_{revision}.stp";
                    }
                    else
                    {
                        filePathStep += ".stp";
                    }

                    if (referencedDoc.SubType == "{9C464203-9BAE-11D3-8BAD-0060B0CE6BB4}")
                    {
                        createFlatDXF((PartDocument)referencedDoc, filePathStep);
                        //if(partDoc.ComponentDefinition.FlatPattern.Equals(null))
                    }
                    // TODO: Method to get rid of the extension
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

                //TranslationContext oStepContext = InventorApplication.TransientObjects.CreateTranslationContext();
                //oStepContext.Type = IOMechanismEnum.kFileBrowseIOMechanism; //Just specifies the type of the 
                //NameValueMap oStepOptions = InventorApplication.TransientObjects.CreateNameValueMap();//??

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

        private void createFlatDXF(PartDocument document, string fileName)
        {
            //TranslatorAddIn oDXFTranslator = (TranslatorAddIn)InventorApplication.ApplicationAddIns.ItemById["{C24E3AC4-122E-11D5-8E91-0010B541CD80}"];

            //TranslationContext oContext = InventorApplication.TransientObjects.CreateTranslationContext();
            //oContext.Type = IOMechanismEnum.kFileBrowseIOMechanism; //Just specifies the type of the 

            //NameValueMap oOptions = InventorApplication.TransientObjects.CreateNameValueMap();//??

            //DataMedium oDataMedium = InventorApplication.TransientObjects.CreateDataMedium();//???

            //if(oDXFTranslator.HasSaveCopyAsOptions[activeDocument, oContext, oOptions])
            //{
            //    string strIniFile = @"C:\Users\Public\Documents\Autodesk\Inventor 2021\Design Data\DWG-DXF\exportdxf.ini";
            //    oOptions.Value["Export_Acad_IniFile"] = strIniFile;
            //}
            ////TODO: Browse DataIO

            //fileName = fileName.Replace(".stp", ".dxf");
            //oDataMedium.FileName = fileName;


            //oDXFTranslator.SaveCopyAs(document, oContext, oOptions, oDataMedium);

            DataIO oDataIO = document.ComponentDefinition.DataIO;


            SheetMetalComponentDefinition oSMDef = (SheetMetalComponentDefinition)document.ComponentDefinition;

            if (!oSMDef.HasFlatPattern)
            {
                oSMDef.Unfold();
                oSMDef.FlatPattern.ExitEdit();
            }
            string sOut = "FLAT PATTERN DXF? AcadVersion = R12  & OuterProfileLayer = Outer & TrimCenterlinesAtContour=True";
            oDataIO.WriteDataToFile(sOut, fileName);
        }
    }
}

