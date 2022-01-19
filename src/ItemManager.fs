namespace Fable.Etebase

open Fable.Core
open Fable.Core.JS
open Fable.Core.JsInterop

type ItemManager =
    /// itemMetadata must implement the Fable.Etebase.ItemMetadata interface
    abstract create: metadata: #ItemMetadata * content: string -> Promise<Item>
    /// itemMetadata must implement the Fable.Etebase.ItemMetadata interface
    abstract create: metadata: #ItemMetadata * content: byte array -> Promise<Item>
    abstract fetch: itemUid: string * ?options: ItemFetchOptions -> Promise<Item>
    abstract list: ?options: ItemFetchOptions -> Promise<ItemListResult<Item>>
    abstract itemRevision: item: Item * ?options: RevisionsFetchOptions -> Promise<ListResponse<Item>>
    abstract fetchUpdates: items: Item array * ?options: ItemFetchOptions -> Promise<ItemListResult<Item>>
    abstract fetchMulti: items: string array * ?options: ItemFetchOptions -> Promise<ItemListResult<Item>>
    abstract batch: items: Item array * ?dependencies: Item array * ?options: ItemFetchOptions -> Promise<unit>
    abstract transaction: items: Item array * ?dependencies: Item array * ?options: ItemFetchOptions -> Promise<unit>
    abstract uploadContent: item: Item -> Promise<unit>
    abstract downloadContent: item: Item -> Promise<unit>

    abstract subscribeChanges:
        callback: (ItemListResult<Item> -> unit) * ?options: ItemFetchOptions -> Promise<WebSocketHandle>

    abstract cacheSave: item: Item * ?options: CacheSaveOptions -> byte array
    abstract cacheLoad: cache: byte array -> Item

module ItemManager =
    [<Import("ItemManager", "Etebase")>]
    let itemManager: ItemManager = jsNative
