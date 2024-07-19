using System;
using System.IO;
using JetBrains.Annotations;

namespace OpenMagic.Specifications.Helpers
{
    [UsedImplicitly]
    public class GivenData
    {
        public DirectoryInfo Directory;
        public FileInfo File;
        public int MaximumInt;
        public int MinimumInt;
        public object ParameterValue;
        public Type Type;
        public Uri Uri;
        public DateTime MaximumDateTime { get; set; }
    }
}