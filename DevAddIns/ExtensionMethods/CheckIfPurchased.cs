using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventor;

namespace DevAddIns
{
    static class CheckIfPurchased
    {
        public static bool IsPurchased(this Document document)
        {
            //TODO: Implement
            if (document.IsAssemblyDocument() || document.IsWeldmentDocument())
            {
                return ((AssemblyDocument)document).ComponentDefinition.BOMStructure == BOMStructureEnum.kPurchasedBOMStructure;
            }
            else if (document.IsPartDocument() || document.IsSheetMetalDocument())
            {
                return ((PartDocument)document).ComponentDefinition.BOMStructure == BOMStructureEnum.kPurchasedBOMStructure;
            }
            return false;
        }
    }
}
