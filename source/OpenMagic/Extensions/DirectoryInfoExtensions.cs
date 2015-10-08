﻿using System.IO;
using Anotar.LibLog;
using OpenMagic.Extensions.Collections.Generic;

namespace OpenMagic.Extensions
{
    public static class DirectoryInfoExtensions
    {
        /// <summary>
        ///     Forcibly deletes a directory.
        /// </summary>
        /// <param name="directory">
        ///     The directory to delete.
        /// </param>
        /// <remarks>
        ///     When attempting to deleting a directory with a .git it would throw
        ///     <see cref="System.UnauthorizedAccessException" />.
        ///     http://stackoverflow.com/questions/8821410/system-unauthorizedaccessexception-access-to-the-path-denied showed that
        ///     the hidden and/or read-only files in the .git directory where causing the exception. This methods sets attribute on
        ///     all files to normal before deleting the directory.
        /// </remarks>
        public static void ForceDelete(this DirectoryInfo directory)
        {
            LogTo.Trace("ForceDelete(directory: {0})", directory.FullName);

            // Setting all file attributes to normal will ensure directory.Delete(true) works.
            directory.SetFileAttributes(FileAttributes.Normal, true);

            // Now we can delete the directory without throw UnauthorizedAccessException.
            directory.Delete(true);
        }

        /// <summary>
        ///     Forcibly deletes a directory if it exists.
        /// </summary>
        /// <param name="directory">
        ///     The directory to delete.
        /// </param>
        public static void ForceDeleteIfExists(this DirectoryInfo directory)
        {
            LogTo.Trace("ForceDeleteIfExists(directory: {0})", directory.FullName);
            if (directory.Exists)
            {
                directory.ForceDelete();
            }
        }

        /// <summary>
        ///     Set file <paramref name="attributes" /> to each file in <paramref name="directory" />.
        /// </summary>
        /// <param name="directory">
        ///     The directory whose file attributes are to be updated.
        /// </param>
        /// <param name="attributes">
        ///     The required file attributes.
        /// </param>
        /// <param name="recursive">
        ///     <c>true</c> to set file attributes to files in <paramref name="directory" /> and its sub-directories.
        /// </param>
        public static void SetFileAttributes(this DirectoryInfo directory, FileAttributes attributes, bool recursive)
        {
            LogTo.Trace("SetFileAttributes(directory: {0}, attributes: {1}, recursive: {2})", directory.FullName, attributes, recursive);

            directory.EnumerateFiles().ForEach(f => f.Attributes = attributes);

            if (!recursive)
            {
                return;
            }

            directory.EnumerateDirectories().ForEach(d => d.SetFileAttributes(attributes, true));
        }
    }
}