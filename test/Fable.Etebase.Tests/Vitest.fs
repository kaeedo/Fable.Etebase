module Vitest

open Fable.Core
open Fable.Core.JS
open Fable.Core.JsInterop

[<Import("*", from = "vitest")>]
let v: obj = jsNative
