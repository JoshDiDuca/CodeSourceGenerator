using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace CodeSourceGenerator.Common
{
    public static class JSON
    {
        public static string Convert<T>(T model)
        {
            try
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));

                using (MemoryStream ms = new MemoryStream())
                {
                    serializer.WriteObject(ms, model);
                    return Encoding.Default.GetString(ms.ToArray());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading json:");
                Console.WriteLine(ex);
            }
            return default;
        }
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
                Console.WriteLine("Error writing json:");
                Console.WriteLine(ex);
            }
            return default;
        }

        private static Stream GenerateStreamFromString(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}
