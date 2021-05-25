# dotnet-generate
A .Net tool for generating files via the CLI to run similar to the way that the Angular CLI runs.

## Installation

Run this command to install the [NuGet](https://www.nuget.org/packages/dotnet-generate/) package to your global dotnet toolset:

`dotnet tool install --global dotnet-generate`

## Run

`dotnet generate <schematic> <path> [options]`

The file will be created in the current working directory of where the console is running unless a more specific path is provided.

## Arguments

| Argument | Description | Value Type |
|----------|-------------|------------|
| `schematic` | The type of file you want to create. This can either be a long or short value. Currently supported types, with short value in square brackets are: [c]lass, [i]nterface, [e]num | string |
| `path` | The path to the folder relative to your current working directory. If you want to go from the project root path start the value with ./. You do not need to enter in a file extension | string |

## Options

| Option (long) | Option (short) | Description | Value Type | Default |
|------|----|----|---|----|
| `--dry-run` | `-d` | If you want to see what would happen turn on the dry run function which will describe the process it would go through | bool | false |
| `--force` | `-f` | If true and the file path you entered exists, then the file will be overwritten, if false then the process will stop | bool | false |
| `--visbility` | `-v` | Set the visibility modifier for this file (public, private etc) | string | "" |
| `--abstract` | `-a` | Add the abstract modifier | bool | false |
| `--static` | `-s` | Add the static modifier | bool | false |
| `--inherits` | `-i` | Type the name of a type that you want this to inherit from | string | "" |
| `--list` | `-ls` | Show a list of the currently supported schematics | bool | false |