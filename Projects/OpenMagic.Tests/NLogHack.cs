using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace OpenMagic.Tests
{
    public static class NLogHack
    {
        public static void Execute()
        {
            CopyNLogFile(GetCommonNLogFileInfo());
            CopyNLogFile(GetNLogFileInfo());
        }

        private static void CopyNLogFile(FileInfo source)
        {
            var destination = Path.GetFullPath(Path.Combine(@".\", source.Name));

            Console.WriteLine("CopyTo: " + destination);

            source.CopyTo(destination, overwrite: true);
        }

        private static FileInfo GetCommonNLogFileInfo()
        {
            var fileInfo = new FileInfo(Path.Combine(GetCommonNLogPackagesFolder().FullName, @"lib\net40\Common.Logging.NLog20.dll"));

            if (fileInfo.Exists)
            {
                return fileInfo;
            }

            throw new FileNotFoundException(string.Format("Cannot find {0}.", fileInfo.FullName), fileInfo.FullName);
        }

        private static FileInfo GetNLogFileInfo()
        {
            var fileInfo = new FileInfo(Path.Combine(GetNLogPackagesFolder().FullName, @"lib\net40\NLog.dll"));

            if (fileInfo.Exists)
            {
                return fileInfo;
            }

            throw new FileNotFoundException(string.Format("Cannot find {0}.", fileInfo.FullName), fileInfo.FullName);
        }

        private static DirectoryInfo GetCommonNLogPackagesFolder()
        {
            var packagesFolder = Assembly.GetPackagesRootFolder();
            var folders = packagesFolder.GetDirectories("Common.Logging.NLog20*");

            if (folders.Count() == 1)
            {
                return folders.First();
            }
            else if (folders.Count() == 0)
            {
                throw new DirectoryNotFoundException(string.Format("Cannot find Common.Logging.NLog20 packages folder in {0}.", packagesFolder.FullName));
            }
            else
            {
                throw new IOException(string.Format("Cannot handle more than one Common.Logging.NLog20 packages folder in {0}.", packagesFolder.FullName));
            }
        }

        private static DirectoryInfo GetNLogPackagesFolder()
        {
            var packagesFolder = Assembly.GetPackagesRootFolder();
            var folders = packagesFolder.GetDirectories("NLog*");

            if (folders.Count() == 1)
            {
                return folders.First();
            }
            else if (folders.Count() == 0)
            {
                throw new DirectoryNotFoundException(string.Format("Cannot find NLog packages folder in {0}.", packagesFolder.FullName));
            }
            else
            {
                throw new IOException(string.Format("Cannot handle more than one NLog packages folder in {0}.", packagesFolder.FullName));
            }
        }


    }
}
