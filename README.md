# Code Source Generator

Code source generator is a visual studio analyzer which allows you to generate source files with access to objects within the solution. The aim of this project is so it can be used for generating any type of source file easily from a template, whether it be typescript, html, javascript or even documentation.

*This project is built using .NET 5 preview and since source generators are currently still a preview feature. You will need the latest version of preview .NET 5, the latest preview of Visual Studio with the 'C# and Visual Basic Roslyn compilers' individual component installed in order to run this solution.*

Please note:
This project has been put on hold until .NET 5 releases more features but this project does work with .NET 5 preview projects and is still a very eligant solution for code generation within your projects.

## Features

- Templating using [scriban](https://github.com/lunet-io/scriban/)
- Completely flexible generating
- Access to all compilation objects
- Generate any type of file
- Documentation examples
- Typescript examples

## [Examples](https://github.com/JoshDiDuca/CodeSourceGenerator/tree/master/examples)
## [Compilation Objects](https://github.com/JoshDiDuca/CodeSourceGenerator/master/OBJECTS.md)
## [To Do](https://github.com/JoshDiDuca/CodeSourceGenerator/blob/master/TODO.md)

## Additional Links

https://devblogs.microsoft.com/dotnet/introducing-c-source-generators/

https://github.com/dotnet/roslyn-sdk/tree/master/samples/CSharp/SourceGenerators

https://github.com/dotnet/roslyn/blob/master/docs/features/source-generators.md

https://github.com/dotnet/roslyn/blob/master/docs/features/source-generators.cookbook.md

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

## License
[MIT](https://choosealicense.com/licenses/mit/)
CodeSourceGenerator
