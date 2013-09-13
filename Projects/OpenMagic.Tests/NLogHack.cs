using System;
using System.IO;

namespace OpenMagic.Tests
{
    /// <summary>
    /// 13 Sep 2013
    /// 
    /// For some reason NLog related DLLs are not copied when using the build script. Maybe Visual Studio 2013 bug.
    /// </summary>
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
            return GetFileInfo(Path.Combine(GetCommonNLogPackagesFolder().FullName, @"lib\net40\Common.Logging.NLog20.dll"));
        }

        private static FileInfo GetNLogFileInfo()
        {
            return GetFileInfo(Path.Combine(GetNLogPackagesFolder().FullName, @"lib\net40\NLog.dll"));
        }

        private static FileInfo GetFileInfo(string fileName)
        {
            var fileInfo = new FileInfo(fileName);

            if (fileInfo.Exists)
            {
                return fileInfo;
            }

            throw new FileNotFoundException(string.Format("Cannot find {0}.", fileInfo.FullName), fileInfo.FullName);
        }

        private static DirectoryInfo GetCommonNLogPackagesFolder()
        {
            return Assembly.GetPackagesFolder("Common.Logging.NLog20");
        }

        private static DirectoryInfo GetNLogPackagesFolder()
        {
            return Assembly.GetPackagesFolder("NLog*");
        }
    }
}
