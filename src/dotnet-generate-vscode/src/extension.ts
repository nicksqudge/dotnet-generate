// The module 'vscode' contains the VS Code extensibility API
// Import the module and reference it with the alias vscode in your code below
import * as vscode from "vscode";

export function activate(context: vscode.ExtensionContext) {
  let disposable = vscode.commands.registerCommand(
    "dotnet-generate-vscode.addFile",
    async () => {
      let fileName = await vscode.window.showInputBox({
        placeHolder: "File name",
        prompt: "Please enter file name",
      });
      if (fileName) {
        // Use show quick pick <t> for selecting the right item for the output
        // Get the path to the folder selected
      }
    }
  );

  context.subscriptions.push(disposable);
}

// this method is called when your extension is deactivated
export function deactivate() {}
