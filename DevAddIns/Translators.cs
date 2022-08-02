using Inventor;
using System;
using System.Windows.Forms;



namespace DevAddIns
{
    class Translators
    {
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

        public static bool includeParts { get; set; }
        public static bool packAssembly { get; set; }
        public Document activeDocument
        {
            get
            {
                return InventorApplication.ActiveDocument;
            }
        }

        public TranslatorAddIn oTranslator;
        public TranslationContext oContext;
        public NameValueMap oOptions;
        public DataMedium oDataMedium;

        public Translators()
        {
            oContext = InventorApplication.TransientObjects.CreateTranslationContext();
            oOptions = InventorApplication.TransientObjects.CreateNameValueMap();
            oDataMedium = InventorApplication.TransientObjects.CreateDataMedium();
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