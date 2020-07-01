using System.Collections.Generic;

namespace CodeSourceGenerator.Models.Config
{
    public class CodeGenerationConfigBuilderOutput
    {
        public string OutputPathTemplate { get; set; }
        public List<string> OutputPathTemplates { get; set; }
        public bool AddToCompilation { get; set; } = true;
    }
}