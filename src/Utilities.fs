namespace Fable.Etebase

open Fable.Core
open Fable.Core.JsInterop

module Utilities =
    let toBase64 (data: obj) : string = import "toBase64" "etebase"
    let fromBase64 (encoded: string) : 'a = import "fromBase64" "etebase"
