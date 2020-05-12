using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace OzlemBilgisayar.MvcWebUI.Utilities
{
    public static class ImageManager
    {
        public static bool Upload(HttpPostedFileBase image, string path)
        {
            if (image == null || image.ContentLength == 0)
            {                   
                return false;
            }
            image.SaveAs(path);
            if (File.Exists(path))
            {
                return true;
            }
            return false;
        }

        public static bool Delete(string path)
        {            
            if (File.Exists(path))
            {
                File.Delete(path);
                return true;
            }
            return false;
        }
    }
}