using Inventor;

namespace DevAddIns
{

    public static class DocumentChecker
    {
        //Doc with subtypes: C:\Users\Public\Documents\Autodesk\Inventor 2021\SDK\DeveloperTools\Include\DocCLSIDs.h

        public const string PartDocumentCLSID = "{4D29B490-49B2-11D0-93C3-7E0706000000}";
        public const string SheetMetalDocumentCLSID = "{9C464203-9BAE-11D3-8BAD-0060B0CE6BB4}";
        public const string MoldedPartDocumentCLSID = "{4D8D80D4-F5B0-4460-8CEA-4CD222684469}";
        public const string AssemblyPartDocumentCLSID = "{E60F81E1-49B3-11D0-93C3-7E0706000000}";
        public const string DrawingDocumentCLSID = "{BBF9FDF1-52DC-11D0-8C04-0800090BE8EC}";
        public const string WeldmentDocumentCLSID = "{28EC8354-9024-440F-A8A2-0E0E55D635B0}";

        public static bool isPartDocument(this Document doc)
        {

            if (doc is null)
            {
                return false;
            }

            else if (doc.SubType == PartDocumentCLSID)
            {
                return true;
            }
            else return false;
        }

        public static bool isSheetMetalDocument(this Document doc)
        {
            if (doc is null)
            {
                return false;
            }

            else if (doc.SubType == SheetMetalDocumentCLSID)
            {
                return true;
            }
            else return false;
        }

        public static bool isMoldedPartDocument(this Document doc)
        {
            if (doc is null)
            {
                return false;
            }

            else if (doc.SubType == MoldedPartDocumentCLSID)
            {
                return true;
            }
            else return false;
        }

        public static bool isDrawingDocument(this Document doc)
        {
            if (doc is null)
            {
                return false;
            }

            else if (doc.SubType == DrawingDocumentCLSID)
            {
                return true;
            }
            else return false;
        }

        public static bool isAssemblyDocument(this Document doc)
        {
            if (doc is null)
            {
                return false;
            }

            else if (doc.SubType == AssemblyPartDocumentCLSID)
            {
                return true;
            }
            else return false;
        }

        public static bool isWeldmentDocument(this Document doc)
        {
            if (doc is null)
            {
                return false;
            }

            else if (doc.SubType == WeldmentDocumentCLSID)
            {
                return true;
            }
            else return false;
        }
    }

}
