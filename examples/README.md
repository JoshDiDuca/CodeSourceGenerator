# Examples

## Summary

This folder contains examples of the framework in use. Since the library is still in development, I will continue to add more examples as I develop.

# Web UI Example

The web UI example currently has the following configuration [file](https://github.com/JoshDiDuca/DynamicCode.SourceGenerator/blob/master/examples/WebUI.Example/codegen.json):

## Services Generation

This source generation example generates typescript services for the web ui's controller.

[Input](https://github.com/JoshDiDuca/DynamicCode.SourceGenerator/tree/master/examples/WebUI.Example/Controllers)

[Template](https://github.com/JoshDiDuca/DynamicCode.SourceGenerator/blob/master/examples/WebUI.Example/Typescript/Templates/typescriptService.scriban)

[Output](https://github.com/JoshDiDuca/DynamicCode.SourceGenerator/tree/master/examples/WebUI.Example/Typescript/Generated/Services)

```json
{
    "Input": {
        "Template": "Typescript/Templates/typescriptService.scriban",
        "InputMatcher": "WebUI.Example.Controllers"
    },
    "Output": {
        "OutputPathTemplate": "Typescript/Generated/Services/{{ name | string.replace 'Controller' 'Service' }}.ts"
    }
}
```


## Models Generation

This source generation example generates typescript models for the web ui.

[Input](https://github.com/JoshDiDuca/DynamicCode.SourceGenerator/tree/master/examples/WebUI.Example/Models)

[Template](https://github.com/JoshDiDuca/DynamicCode.SourceGenerator/blob/master/examples/WebUI.Example/Typescript/Templates/typescriptModel.scriban)

[Output](https://github.com/JoshDiDuca/DynamicCode.SourceGenerator/tree/master/examples/WebUI.Example/Typescript/Generated/Models)

```json
{
    "Input": {
        "Template": "Typescript/Templates/typescriptModel.scriban",
        "InputMatcher": "WebUI.Example.Models"
    },
    "Output": {
        "OutputPathTemplate": "Typescript/Generated/Models/{{ name }}.ts"
    }
}
```

# Console Example

The console example currently has the following configuration [file](https://github.com/JoshDiDuca/DynamicCode.SourceGenerator/blob/master/examples/ConsoleApp.Example/codegen.json):

## Documentation Generation

This source generation example generates the documentation for this library.

[Input](https://github.com/JoshDiDuca/DynamicCode.SourceGenerator/tree/master/DynamicCode.SourceGenerator.Models/RenderModels)

[Template](https://github.com/JoshDiDuca/DynamicCode.SourceGenerator/blob/master/examples/ConsoleApp.Example/CodeGen/documentationModel.scriban)

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
