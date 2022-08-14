using Inventor;


namespace DevAddIns
{
    static class StringConverters
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

        public static string ToStringExt(this BOMStructureEnum BOMSE)
        {
            switch(BOMSE)
            {
                case BOMStructureEnum.kDefaultBOMStructure: return "Default";
                case BOMStructureEnum.kNormalBOMStructure: return "Normal";
                case BOMStructureEnum.kPhantomBOMStructure: return "Phantom";
                case BOMStructureEnum.kReferenceBOMStructure: return "Reference";
                case BOMStructureEnum.kPurchasedBOMStructure: return "Purchased";
                case BOMStructureEnum.kInseparableBOMStructure: return "Inseparable";
                case BOMStructureEnum.kVariesBOMStructure: return "*Varies*";
                default: return BOMSE.ToString();
            }
        }
        
        public static string ToStringExt(this UnitsTypeEnum unitsType)
        {
            switch(unitsType)
            {
                case UnitsTypeEnum.kInchLengthUnits: return "inch";
                case UnitsTypeEnum.kMeterLengthUnits: return "m";
                case UnitsTypeEnum.kMillimeterLengthUnits: return "mm";
                default: return unitsType.ToString();
            }
        }

        //NOTE: int to string format provider: https://docs.microsoft.com/en-us/dotnet/api/system.int64.tostring?view=net-6.0#system-int64-tostring(system-string)
        //NOTE: Custom numeric format strings: https://docs.microsoft.com/en-us/dotnet/standard/base-types/custom-numeric-format-strings
        //NOTE: Standart numeric format strings: https://docs.microsoft.com/en-us/dotnet/standard/base-types/standard-numeric-format-strings
    }
}
