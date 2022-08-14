using Inventor;
using Microsoft.VisualBasic.Compatibility.VB6;
using System;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DevAddIns
{
    abstract class _documentObject
    {
        public Document currentDocument;
        public Document accessDocument;
        public string PartNumber { get => currentDocument.PropertySets[3][2].Value.ToString(); }
        public ImageSource Thumbnail
        {
            get
            {
                using (var ms = new MemoryStream())
                {
                    Support.IPictureDispToImage(currentDocument.Thumbnail).Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                    ms.Seek(0, SeekOrigin.Begin);

                    var bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.StreamSource = ms;
                    bitmapImage.EndInit();

                    return bitmapImage;
                }
            }
        }
        public abstract string BOMStructure { get;}
        string Material { get;}
        public string Revision
        {
            get
            {
                string revision = currentDocument.PropertySets[1][7].Value.ToString();
                if (String.IsNullOrEmpty(revision))
                {
                    return "NONE";
                }
                return revision;
            }
        }
        int Quantity { get;}
        public string SubType
        {
            get
            {
                return currentDocument.SubType;
            }
        }
    }

    //Add unit qty(amount in the first level of the assembly, get by referdocs)
    //qty - get by counting all ref docs

    //Do we really need set?
    //Perhaps only if we are planning to work with Vault
}
