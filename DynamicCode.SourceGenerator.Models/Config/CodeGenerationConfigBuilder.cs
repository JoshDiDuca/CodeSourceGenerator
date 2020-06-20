using System.Collections.Generic;

namespace DynamicCode.SourceGenerator.Models.Config
{
    public class CodeGenerationConfigBuilder
    {
        public string Template { get; set; }
        public string HeaderTemplate { get; set; }
        public string OutputName { get; set; }
        public string InputMatcher { get; set; }
        public List<string> InputMatchers { get; set; }
        public List<string> InputIgnoreMatchers { get; set; }
        public List<string> Assemblies { get; set; }
        public List<string> IncludeLibraries { get; set; }
        public List<string> ExcludeLibraries { get; set; }
    }
}