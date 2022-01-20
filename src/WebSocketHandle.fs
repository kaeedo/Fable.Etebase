namespace Fable.Etebase

open Fable.Core
open Fable.Core.JS
open Fable.Core.JsInterop

type WebSocketHandle =
    abstract connected: bool
    abstract unsubscribe: unit -> Promise<unit>

module WebSocketHandle =
    [<Import("WebSocketHandle", "etebase")>]
    let webSocketHandle: WebSocketHandle =
        jsNative
