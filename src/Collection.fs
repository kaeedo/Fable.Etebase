namespace Fable.Etebase

open Fable.Core
open Fable.Core.JS
open Fable.Core.JsInterop

type OutputFormat =
    | Uint8Array = 0
    | String = 1

type Collection =
    abstract etag: string
    abstract uid: string
    abstract isDeleted: bool
    abstract stoken: string option

    [<Emit("$0.getContent(0)")>]
    abstract getContentBytes: unit -> Promise<byte array>

    [<Emit("$0.getContent(1)")>]
    abstract getContentString: unit -> Promise<string>

    //abstract getContent<'a when 'a : enum<int>>

    abstract setContent: content: string -> Promise<unit>
    abstract setContent: content: byte array -> Promise<unit>

    abstract getCollectionType: unit -> string
    abstract verify: unit -> bool
    abstract delete: ?preserveContent: bool -> unit
// setMeta<T>(meta: ItemMetadata<T>): void;
// getMeta<T>(): ItemMetadata<T>;
// get accessLevel(): CollectionAccessLevel;
// get item(): Item;

module Collection =
    [<Import("Collection", "Etebase")>]
    let collection: Collection = jsNative
