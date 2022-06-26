using Inventor;


namespace DevAddIns
{
    static class StringConverters
    {
        public static string ToStringExt(this Property proper)
        {
            return $"Display name: \"{proper.DisplayName}\"\nName: \"{proper.Name}\"\nValue: {proper.Value}\nExpression: {proper.Expression}\nType: {proper.Type}\nPropId: {proper.PropId}";
        }

        public static string ToStringExt(this PropertySet propSet)
        {
            return $"Display name: \"{propSet.DisplayName}\"\nName: \"{propSet.Name}\nInternalname: \"{propSet.InternalName}\"\nCount: {propSet.Count}\nType: {propSet.Type}";
        }
    }
}
