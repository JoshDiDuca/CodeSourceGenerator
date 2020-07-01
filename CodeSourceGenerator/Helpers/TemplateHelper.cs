using CodeSourceGenerator.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CodeSourceGenerator.Helpers
{
    public static class TemplateHelper
    {
        public static string FindTemplateFile(string value)
        {
            return File.ReadAllText(value);
        }
    }
}
