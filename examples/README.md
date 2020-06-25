# Examples

## Summary

This folder contains examples of the framework in use. Since the library is still in development, these examples don't currently display the full capability of the source generator. As development progresses we will continue to add more.

## Console Example

The console example currently has the following template [configuration](https://github.com/JoshDiDuca/DynamicCode.SourceGenerator/blob/master/examples/ConsoleApp.Example/codegen.json):

# Documentation Generation

This source generation example generates the documentation for this library.

[Input](https://github.com/JoshDiDuca/DynamicCode.SourceGenerator/tree/master/DynamicCode.SourceGenerator.Models/RenderModels)

[Output](https://github.com/JoshDiDuca/DynamicCode.SourceGenerator/tree/master/examples/ConsoleApp.Example/Output/Documentation)

```json
{
  "InputMatchers": [ "DynamicCode.SourceGenerator.Models.RenderModels" ],
  "InputIgnoreMatchers": [ "Collection" ],
  "Assemblies": [ "DynamicCode.SourceGenerator.Models" ],
  "OutputName": "Output/Documentation/Objects.txt",
  "Template": "CodeGen/documentationModel.scriban"
}
```

# Basic Typescript

This source generation example generates a basic typescript file. 

```json
{
    "InputMatcher": "SampleTypescriptClass",
    "OutputName": "Output/Typescript/{{ name }}.ts",
    "Template": "CodeGen/typescriptModel.scriban"
}
```

