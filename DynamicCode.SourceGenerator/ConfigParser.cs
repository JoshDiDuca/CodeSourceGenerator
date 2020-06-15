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
            try
            {
                var file = File.ReadAllText("codegen.json");
                using (var stream = GenerateStreamFromString(file))
                {
                    if (new DataContractJsonSerializer(typeof(T)).ReadObject(stream) is T parsed)
                        return parsed;
                    else return default;
                }
            }
            catch (Exception ex) {
                Console.WriteLine("Error finding file:");
                Console.WriteLine(ex);
            }
            return default;
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
