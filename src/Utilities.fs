namespace Fable.Etebase

open Fable.Core
open Fable.Core.JS
open Fable.Core.JsInterop

module Utilities =
    let toBase64 (data: obj) : string = import "toBase64" "etebase"
    let fromBase64 (encoded: string) : byte array = import "fromBase64" "etebase"
    let randomBytes (length: int): byte array = import "randomBytes" "etebase"

    // let ready : Promise<unit> = import "ready" "etebase"

    // /// Call Utilities.ready if no Account instance has been instantiated
    // let randomBytesDelayed (length: int): Promise<byte array> =
    //     promise {
    //         do! ready
    //         return randomBytes length
    //     }

