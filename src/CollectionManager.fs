namespace Fable.Etebase

open Fable.Core
open Fable.Core.JS
open Fable.Core.JsInterop

[<StringEnum; RequireQualifiedAccess>]
type PrefetchOption =
    | Auto
    | Medium

type FetchOptions =
    {| limit: int option
       stoken: string option
       prefetch: PrefetchOption option |}

type RemovedCollection = {| a: int |}

type CollectionListResult =
    {| data: Collection array
       removedMemberships: (RemovedCollection array) option
       stoken: string
       ``done``: bool |}

type CollectionManager =
    // fetch(colUid: base64, options?: FetchOptions): Promise<Collection>;
    // list(colType: string | string[], options?: FetchOptions): Promise<{
    //     data: Collection[];
    //     removedMemberships?: import("./OnlineManagers").RemovedCollection[] | undefined;
    //     stoken: string;
    //     done: boolean;
    // }>;
    // transaction(collection: Collection, options?: FetchOptions): Promise<void>;
    // cacheSave(collection: Collection, options?: {
    //     saveContent: boolean;
    // }): Uint8Array;
    // cacheLoad(cache: Uint8Array): Collection;
    // getItemManager(col_: Collection): ItemManager;
    // getMemberManager(col: Collection): CollectionMemberManager;

    abstract upload: collection: Collection * ?options: FetchOptions -> Promise<unit>
    abstract create<'a> : colType: string * meta: 'a * content: string -> Promise<Collection>
    abstract create<'a> : colType: string * meta: 'a * content: byte array -> Promise<Collection>
    abstract list: callType: string * ?options: FetchOptions -> Promise<CollectionListResult>
    abstract list: callType: string array * ?options: FetchOptions -> Promise<CollectionListResult>


module CollectionManager =
    [<Import("CollectionManager", "Etebase")>]
    let collectionManager: CollectionManager =
        jsNative
