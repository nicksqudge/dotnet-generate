# dotnet-generate

A .Net tool for generating files via the CLI to run similar to the way that the Angular CLI runs.

## Installation

Run this command to install the [NuGet](https://www.nuget.org/packages/dotnet-generate/) package to your global dotnet toolset:

`dotnet tool install --global dotnet-generate`

## Run

`dotnet generate <schematic> <path> [options]`

The file will be created in the current working directory of where the console is running unless a more specific path is provided.

## Arguments

| Argument    | Description                                                                                                                                                                           | Value Type |
| ----------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ---------- |
| `schematic` | The type of file you want to create. This can either be a long or short value. Currently supported types, with short value in square brackets are: [c]lass, [i]nterface, [e]num       | string     |
| `path`      | The path to the folder relative to your current working directory. If you want to go from the project root path start the value with ./. You do not need to enter in a file extension | string     |

## Options

| Option (long) | Option (short) | Description                                                                                                                                      | Value Type | Default |
| ------------- | -------------- | ------------------------------------------------------------------------------------------------------------------------------------------------ | ---------- | ------- |
| `--dry-run`   | `-d`           | If you want to see what would happen turn on the dry run function which will describe the process it would go through                            | bool       | false   |
| `--force`     | `-f`           | If true and the file path you entered exists, then the file will be overwritten, if false then the process will stop                             | bool       | false   |
| `--public`    | `-p`           | Set the visibility of the file to public                                                                                                         | bool       | true    |
| `--private`   | `-pr`          | Set the visibility of the file to private                                                                                                        | bool       | false   |
| `--internal`  | `-in`          | Set the visibility of the file to internal                                                                                                       | bool       | false   |
| `--abstract`  | `-a`           | Add the abstract modifier                                                                                                                        | bool       | false   |
| `--static`    | `-s`           | Add the static modifier                                                                                                                          | bool       | false   |
| `--inherits`  | `-i`           | Type the name of a type that you want this to inherit from                                                                                       | string     | ""      |
| `--list`      | `-ls`          | Show a list of the currently supported schematics                                                                                                | bool       | false   |
| `--open`      | `-o`           | Run open command after creating file. By default it is appended to the end unless it contains {path} where the full path to the file is provided | string     | ""      |

# Change Log

## Version 1.1

- Added support for --open command
- Changed the visibility modifiers to be --public, --private and --internal for easier use
- Added new schematic or generating a class and an interface at once
- Fixed an issue with namespaces being wrong
- Added schematic detail to the --list command

_Please note: The open command is still experimental so if you have any issues please raise an Issue on Github_

# Future Development

[Go here for the roadmap of the project](https://trello.com/b/TYBhoSaF/dotnet-generate)
