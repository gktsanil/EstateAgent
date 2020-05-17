using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EstateAgent.MvcUI.Helper
{
    public static class PictureHelper
    {
        public static FileStream FileStreamOperation(string fileName)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
            var stream = new FileStream(path, FileMode.Create);
            return stream;
        }

        public static string GetRandomFileNameWithExtension()
        {
            var fileName = Path.GetRandomFileName();
            return Path.ChangeExtension(fileName, ".jpg");
        }

        
    }
}
