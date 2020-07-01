using System.Collections.Generic;

namespace CodeSourceGenerator.Models.Config
{
    public class CodeGenerationConfigBuilderInput
    {
        public string Template { get; set; }
        public string HeaderTemplate { get; set; }
        public string InputMatcher { get; set; }
        public List<string> InputMatchers { get; set; }
        public List<string> InputIgnoreMatchers { get; set; }
        public List<string> Assemblies { get; set; }
    }
}