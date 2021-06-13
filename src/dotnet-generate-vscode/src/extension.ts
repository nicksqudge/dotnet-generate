// The module 'vscode' contains the VS Code extensibility API
// Import the module and reference it with the alias vscode in your code below
import path = require("path");
import { exec } from "promisify-child-process";
import * as vscode from "vscode";

interface ISchematic {
  label: string;
  value: string;
}

export function activate(context: vscode.ExtensionContext) {
  let disposable = vscode.commands.registerCommand(
    "dotnet-generate-vscode.addFile",
    async (target: vscode.Uri) => {
      let folder: vscode.Uri = vscode.Uri.parse("");

      if (target) {
        folder = target;
      } else {
        if (vscode.window.activeTextEditor !== undefined) {
          folder = vscode.Uri.parse(
            path.dirname(vscode.window.activeTextEditor.document.uri.fsPath)
          );
        } else if (vscode.workspace.workspaceFolders !== undefined) {
          folder = vscode.workspace.workspaceFolders[0].uri;
        } else {
          return;
        }
      }

      let fileName = await vscode.window.showInputBox({
        placeHolder: "File name",
        prompt: "Please enter file name",
      });
      if (fileName) {
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

        let schematic = await vscode.window.showQuickPick<ISchematic>(
          schematics
        );

        if (schematic) {
          const finalPath = vscode.Uri.joinPath(folder, fileName).fsPath;

          const command = `dotnet generate ${schematic?.value} ${finalPath}`;

          try {
            const { stdout } = await exec(command);
            if (stdout) {
              return stdout.toString("utf8").split(/\r?\n/);
            } else {
              return [];
            }
            vscode.window.showInformationMessage(command);
          } catch (e) {
            vscode.window.showErrorMessage(e.message + e.stdout);
          }
        }
      }
    }
  );

  context.subscriptions.push(disposable);
}

// this method is called when your extension is deactivated
export function deactivate() {}
