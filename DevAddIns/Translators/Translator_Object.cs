using Inventor;
using System;
using System.Windows.Forms;
using System.Collections.Generic;



namespace DevAddIns
{
    class Translator_Object
    {
        //TODO: Think about whether to make class abstract or not

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

        public TranslatorAddIn oTranslator;
        public TranslationContext oContext;
        public NameValueMap oOptions;
        public DataMedium oDataMedium;

        //TODO: Create constructor with a document in it so that we can use that doc accross all the methods inside translators

        public Translator_Object()
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
    }
}


//Create method overrides that will take path as arguments???s

//TODO: Create a typicall translator class
//TODO: Add a file finder in the directories(use recursion until NULL)
//TODO: Change Forms so that they will look a bit presentable
//TODO: CheckBox for replacing existing files???


//TODO: There is apparently option to save sheet metal into pdf document???


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