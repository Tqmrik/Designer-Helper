using Microsoft.VisualStudio.TestTools.UnitTesting;
using DevAddIns;
using Inventor;

namespace DevAddInsTests
{
    [TestClass]
    public class DocumentCheckerTests
    {
        private static Inventor.Application InventorApp;
        public static Inventor.Application _InventorApp
        {
            get
            {
                return InventorApp;
            }
            set
            {
                InventorApp = value;
            }
        }


        //TODO: How to check???


        //[TestMethod]
        //public void isPartDocument_PartDocument_true()
        //{
        //    //Arrange
        //    Document partDocument = InventorApp.Documents.Add(DocumentTypeEnum.kPartDocumentObject, CreateVisible:false);
        //    partDocument.SubType = DocumentChecker.PartDocumentCLSID;
        //    bool expected = true;

        //    //Action //Assert
        //    Assert.AreEqual(expected, partDocument.isPartDocument());
        //}

    }
}
