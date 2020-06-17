using DynamicCode.SourceGenerator.Common;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace DynamicCode.SourceGenerator
{
    public class ConfigParser
    {
        public static T GetConfig<T>()
        {
            var file = File.ReadAllText("codegen.json");
            return JSON.Parse<T>(file);
        }
        public static Stream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }

}
