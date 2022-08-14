using Microsoft.VisualStudio.TestTools.UnitTesting;
using DevAddIns;
using Inventor;

namespace DevAddInsTests
{
    [TestClass]
    public class stringConvertersTests
    {

        #region "UnitsTypeEnum"
        [TestMethod]
        public void UnitsTypeEnumToStringExt_UnitsTypeEnumkInchLengthUnits_inch()
        {
            //Arrange
            UnitsTypeEnum inchLengthUnits = UnitsTypeEnum.kInchLengthUnits;
            string expected = StringConverters.InchUnits;

            //Action
            string actual = inchLengthUnits.ToStringExt();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UnitsTypeEnumToStringExt_UnitsTypeEnumkMeterLengthUnits_m()
        {
            //Arrange
            UnitsTypeEnum meterLengthUnits = UnitsTypeEnum.kMeterLengthUnits;
            string expected = StringConverters.MeterUnits;

            //Action
            string actual = meterLengthUnits.ToStringExt();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UnitsTypeEnumToStringExt_UnitsTypeEnumkMMLengthUnits_mm()
        {
            //Arrange
            UnitsTypeEnum milimeterLengthUnits = UnitsTypeEnum.kMillimeterLengthUnits;
            string expected = StringConverters.MillimiterUnits;

            //Action
            string actual = milimeterLengthUnits.ToStringExt();

            //Assert
            Assert.AreEqual(expected, actual);
        }
        #endregion


        #region "BOMStructureEnum"
        [TestMethod]
        public void BOMStructureEnumToStringExt_kDefaultBOMStructure_Default()
        {
            //Arrange
            BOMStructureEnum defaultBOMStructure = BOMStructureEnum.kDefaultBOMStructure;
            string expected = StringConverters.DefaultStructure;

            //Action
            string actual = defaultBOMStructure.ToStringExt();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void BOMStructureEnumToStringExt_kNormalBOMStructure_Default()
        {
            //Arrange
            BOMStructureEnum defaultBOMStructure = BOMStructureEnum.kNormalBOMStructure;
            string expected = StringConverters.NormalStructure;

            //Action
            string actual = defaultBOMStructure.ToStringExt();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void BOMStructureEnumToStringExt_kPhantomBOMStructure_Default()
        {
            //Arrange
            BOMStructureEnum defaultBOMStructure = BOMStructureEnum.kPhantomBOMStructure;
            string expected = StringConverters.PhantomStructure;

            //Action
            string actual = defaultBOMStructure.ToStringExt();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void BOMStructureEnumToStringExt_kReferenceBOMStructure_Default()
        {
            //Arrange
            BOMStructureEnum defaultBOMStructure = BOMStructureEnum.kReferenceBOMStructure;
            string expected = StringConverters.ReferenceStructure;

            //Action
            string actual = defaultBOMStructure.ToStringExt();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void BOMStructureEnumToStringExt_kPurchasedBOMStructure_Default()
        {
            //Arrange
            BOMStructureEnum defaultBOMStructure = BOMStructureEnum.kPurchasedBOMStructure;
            string expected = StringConverters.PurchasedStructure;

            //Action
            string actual = defaultBOMStructure.ToStringExt();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void BOMStructureEnumToStringExt_kInseperableBOMStructure_Default()
        {
            //Arrange
            BOMStructureEnum defaultBOMStructure = BOMStructureEnum.kInseparableBOMStructure;
            string expected = StringConverters.InseparableStructure;

            //Action
            string actual = defaultBOMStructure.ToStringExt();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void BOMStructureEnumToStringExt_kVariesBOMStructure_Default()
        {
            //Arrange
            BOMStructureEnum defaultBOMStructure = BOMStructureEnum.kVariesBOMStructure;
            string expected = StringConverters.VariesStructure;

            //Action
            string actual = defaultBOMStructure.ToStringExt();

            //Assert
            Assert.AreEqual(expected, actual);
        }


        #endregion
    }
}
