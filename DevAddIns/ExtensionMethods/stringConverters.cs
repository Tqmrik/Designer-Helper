using Inventor;


namespace DevAddIns
{
    public static class StringConverters
    {

        //If you'll ever think to rename them to ToString then field will use the default ToString methode instead of one you provided.
        public static string ToStringExt(this Property proper)
        {
            return $"Display name: \"{proper.DisplayName}\"\nName: \"{proper.Name}\"\nValue: {proper.Value}\nExpression: {proper.Expression}\nType: {proper.Type}\nPropId: {proper.PropId}";
        }

        public static string ToStringExt(this PropertySet propSet)
        {
            return $"Display name: \"{propSet.DisplayName}\"\nName: \"{propSet.Name}\nInternalname: \"{propSet.InternalName}\"\nCount: {propSet.Count}\nType: {propSet.Type}";
        }

        public static string ToStringExt(this TranslatorAddIn transAddIn)
        {
            return $"Class Id string: {transAddIn.ClassIdString}\nDisplay name: {transAddIn.DisplayName}\nDescription: {transAddIn.Description}\nFile extensions: {transAddIn.FileExtensions}\nSupports save copy as from: {transAddIn.SupportsSaveCopyAsFrom}";
        }


        #region "Test string holders"
        public const string DefaultStructure = "Default";
        public const string NormalStructure = "Normal";
        public const string PhantomStructure = "Phantom";
        public const string ReferenceStructure = "Reference";
        public const string PurchasedStructure = "Purchased";
        public const string InseparableStructure = "Inseparable";
        public const string VariesStructure = "*Varies*";
        #endregion

        public static string ToStringExt(this BOMStructureEnum BOMSE)
        {
            switch(BOMSE)
            {
                case BOMStructureEnum.kDefaultBOMStructure: return DefaultStructure;
                case BOMStructureEnum.kNormalBOMStructure: return NormalStructure;
                case BOMStructureEnum.kPhantomBOMStructure: return PhantomStructure;
                case BOMStructureEnum.kReferenceBOMStructure: return ReferenceStructure;
                case BOMStructureEnum.kPurchasedBOMStructure: return PurchasedStructure;
                case BOMStructureEnum.kInseparableBOMStructure: return InseparableStructure;
                case BOMStructureEnum.kVariesBOMStructure: return VariesStructure;
                default: return BOMSE.ToString();
            }
        }


        #region "Test string holders"
        public const string InchUnits = "inch";
        public const string MeterUnits = "m";
        public const string MillimiterUnits = "mm";
        #endregion

        public static string ToStringExt(this UnitsTypeEnum unitsType)
        {
            switch(unitsType)
            {
                case UnitsTypeEnum.kInchLengthUnits: return InchUnits;
                case UnitsTypeEnum.kMeterLengthUnits: return MeterUnits;
                case UnitsTypeEnum.kMillimeterLengthUnits: return MillimiterUnits;
                default: return unitsType.ToString();
            }
        }

        //NOTE: int to string format provider: https://docs.microsoft.com/en-us/dotnet/api/system.int64.tostring?view=net-6.0#system-int64-tostring(system-string)
        //NOTE: Custom numeric format strings: https://docs.microsoft.com/en-us/dotnet/standard/base-types/custom-numeric-format-strings
        //NOTE: Standart numeric format strings: https://docs.microsoft.com/en-us/dotnet/standard/base-types/standard-numeric-format-strings
    }
}
