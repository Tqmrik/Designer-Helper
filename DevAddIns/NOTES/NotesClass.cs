using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevAddIns.NOTES
{
    class NotesClass
    {
    }
}

//NOTE: Guide to the unit testing with c#: https://docs.microsoft.com/en-us/visualstudio/test/walkthrough-creating-and-running-unit-tests-for-managed-code?view=vs-2022
//NOTE: Create and run unit tests for UWP apps: https://docs.microsoft.com/en-us/visualstudio/test/walkthrough-creating-and-running-unit-tests-for-windows-store-apps?view=vs-2022



//Apprentice server: 
/*The primary differences between Autodesk Inventor and Apprentice are in the Application and Document objects. The objects that represent the Application and Document are completely different in Autodesk Inventor and Apprentice. The Apprentice Application object is called ApprenticeServerComponent. It supports a much more limited API than the Inventor Application object. In Apprentice there isn't a Documents collection.

When ApprenticeServer.Open opens a document, a reference to that document is held by the ApprenticeServer component in order to be returned from the ApprenticeServer.Document. This ‘active top/last opened’ document is also considered the document used as the root of the save for the FileSaveAs object. A document is closed when either ApprenticeServerDocument.Close is called, or the document’s reference count fully drops to zero. Keep in mind that the reference held by the ApprenticeServer (if it was the last document to be opened) also counts as a reference. This ApprenticeServer reference will be released either when a different document is opened, or ApprenticeServer.Close is called, or when the ApprenticeServer’s reference count fully drops to zero and it is destroyed. 

The document objects used within Apprentice are different from the document objects used in Autodesk Inventor. In Inventor there are the PartDocument, AssemblyDocument, DrawingDocument, and PresentationDocument objects. In Apprentice, the ApprenticeServerDocument object represents the part, assembly, and presentation documents and the ApprenticeServerDrawingDocument represents the drawing document. The code below illustrates using Apprentice to open a document.
 */