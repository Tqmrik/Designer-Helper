using Inventor;

namespace DevAddIns
{

    static class DocumentChecker
    {
        //Doc with subtypes: C:\Users\Public\Documents\Autodesk\Inventor 2021\SDK\DeveloperTools\Include\DocCLSIDs.h
        public static bool isPartDocument(this Document doc)
        {

            if (doc is null)
            {
                return false;
            }

            else if (doc.SubType == "{4D29B490-49B2-11D0-93C3-7E0706000000}")
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

            else if (doc.SubType == "{9C464203-9BAE-11D3-8BAD-0060B0CE6BB4}")
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

            else if (doc.SubType == "{4D8D80D4-F5B0-4460-8CEA-4CD222684469}")
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

            else if (doc.SubType == "{BBF9FDF1-52DC-11D0-8C04-0800090BE8EC}")
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

            else if (doc.SubType == "{E60F81E1-49B3-11D0-93C3-7E0706000000}")
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

            else if (doc.SubType == "{28EC8354-9024-440F-A8A2-0E0E55D635B0}")
            {
                return true;
            }
            else return false;
        }
    }

}
