using System;
using System.IO;

namespace OpenMagic.Specifications.Helpers
{
    public class GivenData
    {
        public Uri Uri;
        public FileInfo File;
        public object ParameterValue;
        public DirectoryInfo Directory;
        public Type Type;
        public int MinimumInt;
        public int MaximumInt;
    }
}