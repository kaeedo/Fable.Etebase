namespace Fable.Etebase

open Fable.Core
open Fable.Core.JS
open Fable.Core.JsInterop

type CollectionManager =

    abstract getItemManager: collection: Collection -> ItemManager
    abstract getMemberManager: collection: Collection -> CollectionMemberManager
    abstract fetch: collectionUid: string * ?options: CollectionManagerFetchOptions -> Promise<Collection>
    abstract cacheSave: collection: Collection * ?options: CacheSaveOptions -> byte array
    abstract cacheLoad: cache: byte array -> Collection
    abstract upload: collection: Collection * ?options: CollectionManagerFetchOptions -> Promise<unit>
    abstract transaction: collection: Collection * ?options: CollectionManagerFetchOptions -> Promise<unit>

    /// itemMetadata must implement the Fable.Etebase.ItemMetadata interface
    abstract create: colType: string * itemMetadata: #ItemMetadata * content: string -> Promise<Collection>

    /// itemMetadata must implement the Fable.Etebase.ItemMetadata interface
    abstract create: colType: string * itemMetadata: #ItemMetadata * content: byte array -> Promise<Collection>

    abstract list:
        callType: string * ?options: CollectionManagerFetchOptions -> Promise<CollectionListResult<Collection>>

    abstract list:
        callType: string array * ?options: CollectionManagerFetchOptions -> Promise<CollectionListResult<Collection>>

module CollectionManager =
    [<Import("CollectionManager", "Etebase")>]
    let collectionManager: CollectionManager =
        jsNative
