using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

namespace NotAClue.Web
{
    public static class FileExtensionMethods
    {
        /// <summary>
        /// Gets the file extension from a string.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>file extension of the filename</returns>
        public static String GetFileExtension(this String fileName)
        {
            return fileName.Substring(fileName.LastIndexOf(".") + 1).ToLower();
        }

        /// <summary>
        /// Replaces the file extension.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="newExt">The new ext.</param>
        /// <returns></returns>
        public static String ReplaceFileExtension(this String fileName, String newExt)
        {
            var ext = fileName.GetFileExtension().ToLower();
            return fileName.Replace(ext, newExt);
        }

        /// <summary>
        /// Gets the name of the file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static String GetFileName(this String fileName)
        {
            String pathSeperator = fileName.GetPathSeperator();
            return fileName.Substring(fileName.LastIndexOf(pathSeperator) + 1);
        }

        /// <summary>
        /// Gets the file name title.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public static String GetFileNameTitle(this String fileName)
        {
            String pathSeperator = fileName.GetPathSeperator();
            return fileName.Substring(fileName.LastIndexOf(pathSeperator) + 1,
                (fileName.LastIndexOf(".") - 1) - fileName.LastIndexOf(pathSeperator));
        }
    }
}
