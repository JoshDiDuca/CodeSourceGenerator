using DynamicCode.SourceGenerator.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DynamicCode.SourceGenerator.Commmon.Helpers
{
    public static class FileHelper
    {
        #region Write

        public static void WriteFile(string fileName, string content, bool append = false)
        {
            if (!Directory.Exists(Path.GetDirectoryName(fileName)))
                Directory.CreateDirectory(Path.GetDirectoryName(fileName));

            if (!append)
            {
                if (File.Exists(fileName))
                    File.Delete(fileName);

                File.WriteAllText(fileName, content);
            }
            else
            {
                File.AppendAllText(fileName, content);
            }
        }

        #endregion Write

        #region Read

        public static string FindTemplateFile(string value)
        {
            return FindFile(value);
        }

        public static string FindFile(string value)
        {
            return File.ReadAllText(value);
        }

        #endregion Read

        #region Delete

        public static void DeleteFile(string value)
        {
            if (File.Exists(value))
                File.Delete(value);
        }

        #endregion Delete
    }
}
