namespace Fable.Etebase

open Fable.Core
open Fable.Core.JS
open Fable.Core.JsInterop

type Item =
    abstract uid: string
    abstract etag: string
    abstract isDeleted: bool
    abstract isMissingContent: bool

    [<Emit("$0.getContent(0)")>]
    abstract getContentBytes: unit -> Promise<byte array>

    [<Emit("$0.getContent(1)")>]
    abstract getContentString: unit -> Promise<string>

    abstract setMeta: metadata: #ItemMetadata -> unit
    abstract getMeta<'a when 'a :> ItemMetadata> : unit -> 'a
    abstract verify: unit -> bool
    abstract setContent: content: string -> Promise<unit>
    abstract setContent: content: byte array -> Promise<unit>
    abstract delete: ?preserveContent: bool -> unit

module Item =
    [<Import("Item", "etebase")>]
    let item: Item = jsNative
