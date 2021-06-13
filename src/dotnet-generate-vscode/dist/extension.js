/******/ (() => { // webpackBootstrap
/******/ 	var __webpack_modules__ = ([
/* 0 */
/***/ (function(__unused_webpack_module, exports, __webpack_require__) {

"use strict";

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
const promisify_child_process_1 = __webpack_require__(2);
const vscode = __webpack_require__(5);
function activate(context) {
    let disposable = vscode.commands.registerCommand("dotnet-generate-vscode.addFile", (target) => __awaiter(this, void 0, void 0, function* () {
        let folder = vscode.Uri.parse("");
        if (target) {
            folder = target;
        }
        else {
            if (vscode.window.activeTextEditor !== undefined) {
                folder = vscode.Uri.parse(path.dirname(vscode.window.activeTextEditor.document.uri.fsPath));
            }
            else if (vscode.workspace.workspaceFolders !== undefined) {
                folder = vscode.workspace.workspaceFolders[0].uri;
            }
            else {
                return;
            }
        }
        let fileName = yield vscode.window.showInputBox({
            placeHolder: "File name",
            prompt: "Please enter file name",
        });
        if (fileName) {
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
            let schematic = yield vscode.window.showQuickPick(schematics);
            if (schematic) {
                const finalPath = vscode.Uri.joinPath(folder, fileName).fsPath;
                const command = `dotnet generate ${schematic === null || schematic === void 0 ? void 0 : schematic.value} ${finalPath}`;
                try {
                    const { stdout } = yield promisify_child_process_1.exec(command);
                    if (stdout) {
                        return stdout.toString("utf8").split(/\r?\n/);
                    }
                    else {
                        return [];
                    }
                    vscode.window.showInformationMessage(command);
                }
                catch (e) {
                    vscode.window.showErrorMessage(e.message + e.stdout);
                }
            }
        }
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

"use strict";
module.exports = require("path");;

/***/ }),
/* 2 */
/***/ ((__unused_webpack_module, exports, __webpack_require__) => {

"use strict";


var _interopRequireDefault = __webpack_require__(3);

Object.defineProperty(exports, "__esModule", ({
  value: true
}));
exports.promisifyChildProcess = promisifyChildProcess;
exports.spawn = spawn;
exports.fork = fork;
exports.execFile = exports.exec = void 0;

var _child_process = _interopRequireDefault(__webpack_require__(4));

function joinChunks(chunks, encoding) {
  if (chunks[0] instanceof Buffer) {
    var buffer = Buffer.concat(chunks);
    if (encoding) return buffer.toString(encoding);
    return buffer;
  }

  return chunks.join('');
}

function promisifyChildProcess(child) {
  var options = arguments.length > 1 && arguments[1] !== undefined ? arguments[1] : {};

  var _promise = new Promise(function (resolve, reject) {
    var encoding = options.encoding,
        killSignal = options.killSignal;
    var captureStdio = encoding != null || options.maxBuffer != null;
    var maxBuffer = options.maxBuffer || 200 * 1024;
    var error;
    var bufferSize = 0;
    var stdoutChunks = [];
    var stderrChunks = [];

    var capture = function capture(chunks) {
      return function (data) {
        var remaining = maxBuffer - bufferSize;

        if (data.length > remaining) {
          error = new Error("maxBuffer size exceeded"); // $FlowFixMe

          child.kill(killSignal ? killSignal : 'SIGTERM');
          data = data.slice(0, remaining);
        }

        bufferSize += data.length;
        chunks.push(data);
      };
    };

    if (captureStdio) {
      if (child.stdout) child.stdout.on('data', capture(stdoutChunks));
      if (child.stderr) child.stderr.on('data', capture(stderrChunks));
    }

    child.on('error', reject);

    function done(code, signal) {
      if (!error) {
        if (code != null && code !== 0) {
          error = new Error("Process exited with code ".concat(code));
        } else if (signal != null) {
          error = new Error("Process was killed with ".concat(signal));
        }
      }

      function defineOutputs(obj) {
        if (captureStdio) {
          obj.stdout = joinChunks(stdoutChunks, encoding);
          obj.stderr = joinChunks(stderrChunks, encoding);
        } else {
          /* eslint-disable no-console */
          Object.defineProperties(obj, {
            stdout: {
              configurable: true,
              enumerable: true,
              get: function get() {
                console.error(new Error("To get stdout from a spawned or forked process, set the `encoding` or `maxBuffer` option").stack.replace(/^Error/, 'Warning'));
                return null;
              }
            },
            stderr: {
              configurable: true,
              enumerable: true,
              get: function get() {
                console.error(new Error("To get stderr from a spawned or forked process, set the `encoding` or `maxBuffer` option").stack.replace(/^Error/, 'Warning'));
                return null;
              }
            }
          });
          /* eslint-enable no-console */
        }
      }

      var output = {};
      defineOutputs(output);
      var finalError = error;

      if (finalError) {
        finalError.code = code;
        finalError.signal = signal;
        defineOutputs(finalError);
        reject(finalError);
      } else {
        resolve(output);
      }
    }

    child.on('close', done);
    child.on('exit', done);
  });

  return Object.create(child, {
    then: {
      value: _promise.then.bind(_promise)
    },
    catch: {
      value: _promise.catch.bind(_promise)
    }
  });
}

function spawn(command, args, options) {
  return promisifyChildProcess(_child_process.default.spawn(command, args, options), Array.isArray(args) ? options : args);
}

function fork(module, args, options) {
  return promisifyChildProcess(_child_process.default.fork(module, args, options), Array.isArray(args) ? options : args);
}

function promisifyExecMethod(method) {
  return function () {
    for (var _len = arguments.length, args = new Array(_len), _key = 0; _key < _len; _key++) {
      args[_key] = arguments[_key];
    }

    var child;

    var _promise = new Promise(function (resolve, reject) {
      child = method.apply(void 0, args.concat([function (err, stdout, stderr) {
        if (err) {
          err.stdout = stdout;
          err.stderr = stderr;
          reject(err);
        } else {
          resolve({
            stdout: stdout,
            stderr: stderr
          });
        }
      }]));
    });

    if (!child) {
      throw new Error('unexpected error: child has not been initialized');
    }

    return Object.create(child, {
      then: {
        value: _promise.then.bind(_promise)
      },
      catch: {
        value: _promise.catch.bind(_promise)
      }
    });
  };
}

var exec = promisifyExecMethod(_child_process.default.exec);
exports.exec = exec;
var execFile = promisifyExecMethod(_child_process.default.execFile);
exports.execFile = execFile;

/***/ }),
/* 3 */
/***/ ((module) => {

function _interopRequireDefault(obj) {
  return obj && obj.__esModule ? obj : {
    "default": obj
  };
}

module.exports = _interopRequireDefault;
module.exports.default = module.exports, module.exports.__esModule = true;

/***/ }),
/* 4 */
/***/ ((module) => {

"use strict";
module.exports = require("child_process");;

/***/ }),
/* 5 */
/***/ ((module) => {

"use strict";
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