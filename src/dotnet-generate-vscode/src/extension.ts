// The module 'vscode' contains the VS Code extensibility API
// Import the module and reference it with the alias vscode in your code below
import path = require("path");
import * as cp from "child_process";
import * as vscode from "vscode";

interface ISchematic {
  label: string;
  value: string;
}

function _getFolder(target: vscode.Uri) {
  if (target) {
    return target;
  } else {
    if (vscode.window.activeTextEditor !== undefined) {
      return vscode.Uri.parse(
        path.dirname(vscode.window.activeTextEditor.document.uri.fsPath)
      );
    } else if (vscode.workspace.workspaceFolders !== undefined) {
      return vscode.workspace.workspaceFolders[0].uri;
    } else {
      return;
    }
  }
}

async function _pickSchematic() {
  const schematics: Array<ISchematic> = [
    {
      label: "Class",
      value: "class",
    },
    {
      label: "Interface",
      value: "interface",
    },
    {
      label: "Enum",
      value: "enum",
    },
    {
      label: "Interface & Class",
      value: "interfaceclass",
    },
  ];

  return await vscode.window.showQuickPick<ISchematic>(schematics);
}

async function _runCommand(
  schematic: ISchematic,
  folder: vscode.Uri,
  fileName: string
) {
  const command = `dotnet generate ${schematic?.value} ${fileName}`;

  console.log(command);
  cp.exec(
    command,
    {
      cwd: folder.path,
    },
    (err, stdout, stderr) => {
      console.log("stdout: " + stdout);
      console.log("stderr: " + stderr);
      if (err) {
        console.log("error: " + err);
      }
    }
  );
}

export function activate(context: vscode.ExtensionContext) {
  let disposable = vscode.commands.registerCommand(
    "dotnet-generate-vscode.addFile",
    async (target: vscode.Uri) => {
      const folder = _getFolder(target);
      if (!folder) return;

      let fileName = await vscode.window.showInputBox({
        placeHolder: "File name",
        prompt: "Please enter file name",
      });
      if (!fileName) return;

      const schematic = await _pickSchematic();
      if (!schematic) return;

      await _runCommand(schematic, folder, fileName);
    }
  );

  context.subscriptions.push(disposable);
}

// this method is called when your extension is deactivated
export function deactivate() {}
