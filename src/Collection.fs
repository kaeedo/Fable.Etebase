namespace Fable.Etebase

open Fable.Core
open Fable.Core.JS
open Fable.Core.JsInterop

type OutputFormat =
    | Uint8Array = 0
    | String = 1

type CollectionAccessLevel =
    | ReadOnly = 0
    | Admin = 1
    | ReadWrite = 2

type RemovedCollection = {| uid: string |}

type ItemMetadata =
    abstract ``type``: string option
    abstract name: string option
    abstract mtime: string option
    abstract description: string option
    abstract color: string option

type Collection =
    abstract etag: string
    abstract uid: string
    abstract isDeleted: bool
    abstract stoken: string option
    abstract item: Item
    abstract accessLevel: CollectionAccessLevel

    [<Emit("$0.getContent(0)")>]
    abstract getContentBytes: unit -> Promise<byte array>

    [<Emit("$0.getContent(1)")>]
    abstract getContentString: unit -> Promise<string>

    abstract setContent: content: string -> Promise<unit>
    abstract setContent: content: byte array -> Promise<unit>
    abstract getCollectionType: unit -> string
    abstract verify: unit -> bool
    abstract delete: ?preserveContent: bool -> unit
    abstract setMeta: metadata: #ItemMetadata -> unit
    abstract getMeta<'a when 'a :> ItemMetadata> : unit -> 'a

module Collection =
    [<Import("Collection", "Etebase")>]
    let collection: Collection = jsNative
