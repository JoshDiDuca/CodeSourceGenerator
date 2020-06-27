# Dynamic Code Source Generator

Dynamic code source generator is a visual studio analyzer which allows you to generate source files pre compilation with access to objects within the source code. The aim of this project is so it can be used for generating any type of source file easily from a template, whether it be typescript, html, javascript or even documentation.

*This project is built using .NET 5 preview since source generators are currently still a preview feature.*

## Features

- Templating using scriban
- Completely flexible templating
- Generate any type of file

## [Examples](https://github.com/JoshDiDuca/DynamicCode.SourceGenerator/tree/master/examples)
## [Compilation Objects](https://github.com/JoshDiDuca/DynamicCode.SourceGenerator/blob/master/Objects.md)

## To Do

- Scriban integration ✔
- File Watching (Waiting on preview release)
- Config File ✔
- - Basic Code Finding ✔
- - Output Name Template ✔
- - Multiple Output Templates ✔
- - Turn off adding source to compilation ✔
- - Http Template Loading
- - File Directory Template Loading
- - Pre Built Templates
- - Header File
- File Generation
- - Basic File ✔
- - Multiple templates in file ✔
- - Deleting files
- Template Integration
- - Source Objects Integration ✔ ([See more](https://github.com/JoshDiDuca/DynamicCode.SourceGenerator/blob/master/Objects.md))
- - Language Helper Functions/Names
- - - Typescript ✔
- - - Case functions (in progress)
- - - SQL?
- Examples
- - Typescript Models
- - Typescript Services
- - Markup Documentation ✔

## Additional Links

https://devblogs.microsoft.com/dotnet/introducing-c-source-generators/

https://github.com/dotnet/roslyn-sdk/tree/master/samples/CSharp/SourceGenerators

https://github.com/dotnet/roslyn/blob/master/docs/features/source-generators.md

https://github.com/dotnet/roslyn/blob/master/docs/features/source-generators.cookbook.md

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

## License
[MIT](https://choosealicense.com/licenses/mit/)
