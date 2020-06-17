using System;
using System.IO;
using System.Runtime.Serialization.Json;

namespace DynamicCode.SourceGenerator.Common
{
    public static class JSON
    {
        public static T Parse<T>(string content)
        {
            try
            {
                if (new DataContractJsonSerializer(typeof(T)).ReadObject(GenerateStreamFromString(content)) is T parsed)
                    return parsed;
                else return default;
            }
            catch (Exception ex)
            {
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
