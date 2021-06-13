/******/ (() => { // webpackBootstrap
/******/ 	"use strict";
/******/ 	var __webpack_modules__ = ([
/* 0 */
/***/ (function(__unused_webpack_module, exports, __webpack_require__) {


var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
Object.defineProperty(exports, "__esModule", ({ value: true }));
exports.deactivate = exports.activate = void 0;
// The module 'vscode' contains the VS Code extensibility API
// Import the module and reference it with the alias vscode in your code below
const path = __webpack_require__(1);
const cp = __webpack_require__(2);
const vscode = __webpack_require__(3);
function _getFolder(target) {
    if (target) {
        return target;
    }
    else {
        if (vscode.window.activeTextEditor !== undefined) {
            return vscode.Uri.parse(path.dirname(vscode.window.activeTextEditor.document.uri.fsPath));
        }
        else if (vscode.workspace.workspaceFolders !== undefined) {
            return vscode.workspace.workspaceFolders[0].uri;
        }
        else {
            return;
        }
    }
}
function _pickSchematic() {
    return __awaiter(this, void 0, void 0, function* () {
        const schematics = [
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
        return yield vscode.window.showQuickPick(schematics);
    });
}
function _runCommand(schematic, folder, fileName) {
    return __awaiter(this, void 0, void 0, function* () {
        const command = `dotnet generate ${schematic === null || schematic === void 0 ? void 0 : schematic.value} ${fileName}`;
        console.log(command);
        cp.exec(command, {
            cwd: folder.path,
        }, (err, stdout, stderr) => {
            console.log("stdout: " + stdout);
            console.log("stderr: " + stderr);
            if (err) {
                console.log("error: " + err);
            }
        });
    });
}
function activate(context) {
    let disposable = vscode.commands.registerCommand("dotnet-generate-vscode.addFile", (target) => __awaiter(this, void 0, void 0, function* () {
        const folder = _getFolder(target);
        if (!folder)
            return;
        let fileName = yield vscode.window.showInputBox({
            placeHolder: "File name",
            prompt: "Please enter file name",
        });
        if (!fileName)
            return;
        const schematic = yield _pickSchematic();
        if (!schematic)
            return;
        yield _runCommand(schematic, folder, fileName);
    }));
    context.subscriptions.push(disposable);
}
exports.activate = activate;
// this method is called when your extension is deactivated
function deactivate() { }
exports.deactivate = deactivate;


/***/ }),
/* 1 */
/***/ ((module) => {

module.exports = require("path");;

/***/ }),
/* 2 */
/***/ ((module) => {

module.exports = require("child_process");;

/***/ }),
/* 3 */
/***/ ((module) => {

module.exports = require("vscode");;

/***/ })
/******/ 	]);
/************************************************************************/
/******/ 	// The module cache
/******/ 	var __webpack_module_cache__ = {};
/******/ 	
/******/ 	// The require function
/******/ 	function __webpack_require__(moduleId) {
/******/ 		// Check if module is in cache
/******/ 		var cachedModule = __webpack_module_cache__[moduleId];
/******/ 		if (cachedModule !== undefined) {
/******/ 			return cachedModule.exports;
/******/ 		}
/******/ 		// Create a new module (and put it into the cache)
/******/ 		var module = __webpack_module_cache__[moduleId] = {
/******/ 			// no module.id needed
/******/ 			// no module.loaded needed
/******/ 			exports: {}
/******/ 		};
/******/ 	
/******/ 		// Execute the module function
/******/ 		__webpack_modules__[moduleId].call(module.exports, module, module.exports, __webpack_require__);
/******/ 	
/******/ 		// Return the exports of the module
/******/ 		return module.exports;
/******/ 	}
/******/ 	
/************************************************************************/
/******/ 	
/******/ 	// startup
/******/ 	// Load entry module and return exports
/******/ 	// This entry module is referenced by other modules so it can't be inlined
/******/ 	var __webpack_exports__ = __webpack_require__(0);
/******/ 	module.exports = __webpack_exports__;
/******/ 	
/******/ })()
;
//# sourceMappingURL=extension.js.map