namespace Fable.Etebase

open Fable.Core
open Fable.Core.JS
open Fable.Core.JsInterop

//https://github.com/fable-compiler/fable3-samples/blob/main/interop/public/MyClass.js
//https://github.com/fable-compiler/fable3-samples/blob/main/interop/src/App.fs

type Account =
    abstract login: username: string * password: string * ?serverUrl: string -> Promise<Account>
    abstract isEtebaseServer: serverUrl: string -> Promise<bool>

module Account =
    let account: Account = jsNative
