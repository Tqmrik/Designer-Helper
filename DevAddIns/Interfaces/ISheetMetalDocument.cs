using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevAddIns
{
    interface ISheetMetalDocument : IPartDocument
    {
        string SheetThickness { get; set; }
    }
}
