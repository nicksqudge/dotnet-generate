# dotnet-generate
A .Net tool for generating files via the CLI to run similar to the way that the Angular CLI runs.

`dotnet generate <schematic> [options]`

The file will be created in the current working directory of where the console is running unless a more specific path is provided.

## Arguments

|Argument|Description|ValueType|
|--------|-----------|---------|
|`schematic`|The sort of file you want to generate. Supported types: <li>Class (or c)</li><li>Interface (or i)</li><li>Enum (or e)</li> |`string`|

## Options

|Option|Description|ValueType|Default|
|------|-----------|---------|-------|
|`--dry-run`|Run through and reports activity without writing out results. *Alias -d*|boolean|false|
|`--force`|Force overwriting of existing files *Alias -f*|boolean|false|
|`--help`|Shows help message for this command in the console|boolean|false|

## Reference

https://docs.microsoft.com/en-us/dotnet/core/tools/global-tools-how-to-create
https://docs.microsoft.com/en-us/nuget/consume-packages/configuring-nuget-behavior 
https://angular.io/cli/generate