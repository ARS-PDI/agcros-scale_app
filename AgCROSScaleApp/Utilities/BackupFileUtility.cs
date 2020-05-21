using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AgCROSScaleApp.Utilities
{
    class BackupFileUtility
    {

        /*
         * TODO: 
         * 1) check if backup file(s) exist
         * 2) if > 3 replicates, delete oldest, create new
         * 3) name something identifying: bck_yyyyMMddmmss?
         */
        public static void CreateBackupFile(string fileName)
        {
            if (!File.Exists(fileName))
                return; // dont backup a file that doesn't exist...
            var dir = Path.GetDirectoryName(fileName);
            var dirInfo = new DirectoryInfo(dir);
            var fName = Path.GetFileNameWithoutExtension(fileName);
            var ext = Path.GetExtension(fileName);
            var backs = dirInfo.GetFiles($"*{fName}_bck_*");
            if (backs.Length >= 3)
            {
                //delete oldest
                File.Delete(backs.OrderBy(fi => fi.CreationTime).First().FullName);
            }
            
            var tmpName = Path.Combine(dir, $"{fName}_bck_{DateTime.Now:yyyyMMddHHmmss}{ext}");
            File.Copy(fileName, tmpName);
        }

    }
}
