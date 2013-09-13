using System;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OpenMagic.Tests
{
    [TestClass()]
    public sealed class Assembly
    {
        [AssemblyInitialize()]
        public static void AssemblyInit(TestContext context)
        {
            NLogHack.Execute();
        }

        public static DirectoryInfo GetPackagesFolder(string packageName)
        {
            var packagesFolder = Assembly.GetPackagesRootFolder();
            var folders = packagesFolder.GetDirectories(packageName + "*");

            if (folders.Count() == 1)
            {
                return folders.First();
            }
            else if (folders.Count() == 0)
            {
                throw new DirectoryNotFoundException(string.Format("Cannot find {1} package folder in {0}.", packagesFolder.FullName, packageName));
            }
            else
            {
                throw new IOException(string.Format("Cannot handle more than one {1} package folder in {0}.", packagesFolder.FullName, packageName));
            }
        }

        public static DirectoryInfo GetPackagesRootFolder()
        {
            var folder = new DirectoryInfo(Path.Combine(GetSolutionFolder().FullName, "Packages"));

            if (folder.Exists)
            {
                return folder;
            }

            throw new DirectoryNotFoundException(String.Format("Cannot find packages folder, {0}.", folder.FullName));
        }

        public static DirectoryInfo GetSolutionFolder()
        {
            var folder = new DirectoryInfo(@".\");

            for (int i = 0; i < 5; i++)
            {
                if (File.Exists(Path.Combine(folder.FullName, "OpenMagic.sln")))
                {
                    return folder;
                }

                folder = folder.Parent;
            }

            throw new DirectoryNotFoundException(string.Format("Cannot find solution folder. Started looking backwards from {0}.", (new DirectoryInfo(@".\")).FullName));
        }
    }
}