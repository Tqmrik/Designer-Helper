using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevAddIns
{
    interface IPartDocument
    {
        string PartNumber { get; set; }
        string Preview { get; set; }
        string BOMStructure { get; set; }
        string Material { get; set; }
        string Revision { get; set; }
        int Quantity { get; set; }
    }
}
