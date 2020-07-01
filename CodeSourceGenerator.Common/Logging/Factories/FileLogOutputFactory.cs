using CodeSourceGenerator.Commmon.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Text;

namespace CodeSourceGenerator.Common.Logging.Factories
{
    public class FileLogOutputFactory : BaseLogOutputFactory, ILogOuputFactory
    {
        public string FilePath { get; set; }

        public FileLogOutputFactory(string key, string filePath, LogScope? includedInScope, LogScope? excludeScope, string additionalKey = null) : base(key, includedInScope, excludeScope, additionalKey)
        {
            FilePath = filePath;
        }

        public override bool IsInScope(LogModel model) => base.IsInScope(model);

        static readonly char[] invalidFileNameChars = Path.GetInvalidFileNameChars();

        public override bool Log(LogModel model)
        {
            try
            {
                string additionalKey = string.Empty;
                if (!string.IsNullOrEmpty(model.AdditionalKey))
                    additionalKey = model.AdditionalKey;
                else if (!string.IsNullOrEmpty(AdditionalKey))
                    additionalKey = AdditionalKey;
                else additionalKey = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");

                string fileName = $"{Key}_{additionalKey}.txt";

                string validFilename = new string(fileName.Where(ch => !invalidFileNameChars.Contains(ch)).ToArray()).Replace(" ", "_").ToLower();
                string filePath = Path.Combine(FilePath, validFilename);

                FileHelper.WriteFile(filePath, $"{GetScopeString(model.Scopes) ?? "Log"}: {model.Title} - {model.Message}", true);

                if (model.Exception != null)
                    FileHelper.WriteFile(filePath, $"Exception: {model.Title} - {model.Message}", true);

                return base.Log(model);
            }
            catch (Exception)
            {
                return false; 
            }
        }
    }
}
