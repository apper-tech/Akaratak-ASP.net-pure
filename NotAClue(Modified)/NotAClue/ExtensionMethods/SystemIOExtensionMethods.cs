using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web.UI;
using System.Security;
using System.Web;
using System.Data.SqlClient;

namespace NotAClue
{
    public static class SystemIOExtensionMethods
    {
        public static string GetMimeType(this FileInfo file)
        {
            string mime = "application/octetstream";
            Microsoft.Win32.RegistryKey rk = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(file.Extension);

            if (rk != null && rk.GetValue("Content Type") != null)
                mime = rk.GetValue("Content Type").ToString();
            
            return mime;
        }
        /// <summary>
        /// Redirects the tot file.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="relativeFilePath">The relative file path.</param>
        /// <param name="showError">if set to <c>true</c> [show error].</param>
        /// <param name="inline">if set to <c>true</c> [inline].</param>
        public static void RedirectTotFile(this Page page, String relativeFilePath, Boolean showError = true, Boolean inline = true)
        {
            // set default status code
            var statusCode = 200;

            // get full file path
            var filePath = page.Server.MapPath(relativeFilePath);

            // is in-line (open in browser) or attachment download.
            String fileType = inline ? "inline" : "attachment";

            var file = new FileInfo(filePath);
            // check file exists
            if (!file.Exists)
                throw new FileNotFoundException(String.Format("File '{0}' does not exist", file.Name));

            try
            {
                page.Response.Clear();
                page.Response.ClearContent();
                page.Response.ClearHeaders();
                page.Response.Cookies.Clear();

                // turn caching on
                page.Response.AppendHeader("Pragma", "cache");

                // set cache expiry time
                page.Response.AppendHeader("Expires", "10");

                // declare file length
                page.Response.AppendHeader("Content-Length", file.Length.ToString());

                page.Response.Buffer = true;

                string strHeaderText = String.Format("{0}; filename=\"{1}\"; size={2}; creation-date={3:R}; modification-date={4:R}; read-date={5:R}",
                    fileType,
                    file.Name,
                    file.Length,
                    file.CreationTime,
                    file.LastAccessTime,
                    DateTime.Now);

                // use attachment for download and in-line for viewing
                page.Response.AppendHeader("Content-Disposition", strHeaderText);

                // Get correct mime type based on Extension
                var mimeType = file.GetMimeType();

                //Set the appropriate ContentType.
                page.Response.ContentType = inline ? mimeType : "application/octet-stream";

                // get file as byte stream
                FileStream myFileStream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read);
                long FileSize = myFileStream.Length;
                byte[] Buffer = new byte[(int)FileSize];
                myFileStream.Read(Buffer, 0, (int)FileSize);
                myFileStream.Close();

                //Write the file directly to the HTTP content output stream.
                page.Response.BinaryWrite(Buffer);
            }
            // Note: exception variable left for debug purposes
            // Useful status codes http://en.wikipedia.org/wiki/List_of_HTTP_status_codes
            catch (FileNotFoundException)
            {
                // 404 Not Found - The requested resource could not be found but may be available again in the future.
                // Subsequent requests by the client are permissible
                statusCode = 404;
            }
            catch (DirectoryNotFoundException)
            {
                // 404 Not Found - The requested resource could not be found but may be available again in the future.
                // Subsequent requests by the client are permissible
                statusCode = 404;
            }
            catch (PathTooLongException)
            {
                // 404 Not Found - The requested resource could not be found but may be available again in the future.
                // Subsequent requests by the client are permissible
                statusCode = 404;
            }
            catch (IOException)
            {
                // 404 Not Found - The requested resource could not be found but may be available again in the future.
                // Subsequent requests by the client are permissible
                statusCode = 404;
            }
            catch (NotSupportedException)
            {
                // 400 Bad page.Request  - The request contains bad syntax or cannot be fulfilled.                
                statusCode = 400;
            }
            catch (UnauthorizedAccessException)
            {
                // 403 Forbidden - The request was a legal request, but the server is refusing to respond to it.
                // Unlike a 401 Unauthorized response, authenticating will make no difference.
                statusCode = 403;
            }
            catch (SecurityException)
            {
                // 403 Forbidden - The request was a legal request, but the server is refusing to respond to it.
                // Unlike a 401 Unauthorized response, authenticating will make no difference.
                statusCode = 403;
            }
            catch (HttpException)
            {
                // 400 Bad page.Request  - The request contains bad syntax or cannot be fulfilled.                
                statusCode = 400;
            }
            catch (Exception)
            {
                // 500 Internal Server Error - A generic error message, given when no more specific message is suitable
                statusCode = 500;
            }
            finally
            {
                // if errors enabled
                if (showError)
                    page.Response.StatusCode = statusCode;

                page.Response.Flush();
                page.Response.End();
            }
        }

        /// <summary>
        /// Saves Byte array to file.
        /// </summary>
        /// <param name="byteArray">The byte array.</param>
        /// <param name="fileName">Name of the _ file.</param>
        /// <returns></returns>
        public static bool SaveToFile(this Byte[] byteArray, String fileName)
        {
            try
            {
                // Open file for reading
                var fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write);

                // Writes a block of bytes to this stream using data from a byte array.
                fileStream.Write(byteArray, 0, byteArray.Length);

                // close file stream
                fileStream.Close();

                return true;
            }
            catch
            {
                // error occurred, return false
                return false;
            }
        }

        /// <summary>
        /// Deletes all files in directory.
        /// </summary>
        /// <param name="directoryInfo">The directory info.</param>
        /// <returns></returns>
        public static Boolean DeleteAllFilesInDirectory(this DirectoryInfo directoryInfo)
        {
            // delete files in upload folder
            var files = directoryInfo.GetFiles();
            if (files.Count() > 0)
            {
                try
                {
                    foreach (var file in files)
                    {
                        File.Delete(file.FullName);
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
