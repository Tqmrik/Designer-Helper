using Inventor;
using System;
using System.Windows.Forms;
using System.Collections.Generic;



namespace DevAddIns
{
    abstract class Translator_Object
    {

        private static Inventor.Application inventorApplication;
        public static Inventor.Application _inventorApplication
        {
            set
            {
                inventorApplication = value;
            }
            get
            {
                return inventorApplication;
            }
        }

        public static bool includeParts { get; set; }
        public static bool packAssembly { get; set; }
        public Document activeDocument
        {
            get
            {
                return _inventorApplication.ActiveDocument;
            }
        }

        public string filePath;
        public string dirPath;

        public TranslatorAddIn oTranslator;
        public TranslationContext oContext;
        public NameValueMap oOptions;
        public DataMedium oDataMedium;

        public Translator_Object()
        {
            oContext = _inventorApplication.TransientObjects.CreateTranslationContext();
            oOptions = _inventorApplication.TransientObjects.CreateNameValueMap();
            oDataMedium = _inventorApplication.TransientObjects.CreateDataMedium();
        }

        public Translator_Object(string directoryPath)
        {
            oContext = _inventorApplication.TransientObjects.CreateTranslationContext();
            oOptions = _inventorApplication.TransientObjects.CreateNameValueMap();
            oDataMedium = _inventorApplication.TransientObjects.CreateDataMedium();
        }

        public Translator_Object(Dictionary<string, string> oOptionsDictionary, string filePath)
        {
            foreach (string key in oOptionsDictionary.Keys)
            {
                oOptions.Value[key] = oOptionsDictionary[key];
            }
        }

        public void FilePathHelper(Document document, string extension) //Add a revision letter to the output file name
        {
            //TODO: Move into another class?
            if (!String.IsNullOrEmpty(document.FullDocumentName))
            {

                if(String.IsNullOrEmpty(this.dirPath))
                {
                    filePath = RevisionHelper.addRevisionLetter(document, PathConverter.FileNameWithoutExtension(document), extension);
                }

                else
                {
                    filePath = RevisionHelper.addRevisionLetter(document, PathConverter.FileNameWithoutExtension(document, this.dirPath), extension);
                }

                int iterator = 1;
                while (System.IO.File.Exists(filePath))
                {
                    filePath = filePath.Remove(filePath.Length - 1 - extension.Length) + $"_{iterator}" + $".{extension}";
                    iterator++;
                }
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
    }
}


//Create method overrides that will take path as arguments???s

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