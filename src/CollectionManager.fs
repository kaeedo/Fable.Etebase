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

type CollectionListResult =
    {| data: Collection array
       removedMemberships: (RemovedCollection array) option
       stoken: string
       ``done``: bool |}

type CollectionManager =

    abstract getItemManager: collection: Collection -> ItemManager
    abstract getMemberManager: collection: Collection -> CollectionMemberManager
    abstract fetch: collectionUid: string * ?options: FetchOptions -> Promise<Collection>
    abstract cacheSave: collection: Collection * ?options: {| saveContent: bool |} -> byte array
    abstract cacheLoad: cache: byte array -> Collection
    abstract upload: collection: Collection * ?options: FetchOptions -> Promise<unit>
    abstract transaction: collection: Collection * ?options: FetchOptions -> Promise<unit>

    /// itemMetadat must extend the Fable.Etebase.ItemMetadata interface
    /// Do this via an F# class,
    /// Or using a record type:
    /// e.g.:
    /// type MyItem =
    ///     { Other: int
    ///       Name: string }
    ///         interface ItemMetadata with
    ///             member this.name = Some this.Name
    abstract create: colType: string * itemMetadata: #ItemMetadata * content: string -> Promise<Collection>

    /// itemMetadat must extend the Fable.Etebase.ItemMetadata interface
    /// Do this via an F# class,
    /// Or using a record type:
    /// e.g.:
    /// type MyItem =
    ///     { Other: int
    ///       Name: string }
    ///         interface ItemMetadata with
    ///             member this.name = Some this.Name
    abstract create: colType: string * itemMetadata: #ItemMetadata * content: byte array -> Promise<Collection>
    abstract list: callType: string * ?options: FetchOptions -> Promise<CollectionListResult>
    abstract list: callType: string array * ?options: FetchOptions -> Promise<CollectionListResult>

module CollectionManager =
    [<Import("CollectionManager", "Etebase")>]
    let collectionManager: CollectionManager =
        jsNative
