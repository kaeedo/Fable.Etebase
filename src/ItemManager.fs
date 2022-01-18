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
//abstract batch:
// batch(items: Item[], deps?: Item[] | null, options?: ItemFetchOptions): Promise<void>;
// transaction(items: Item[], deps?: Item[] | null, options?: ItemFetchOptions): Promise<void>;
// uploadContent(item: Item): Promise<void>;
// downloadContent(item: Item): Promise<void>;
// subscribeChanges(cb: (data: CollectionItemListResponse<Item>) => void, options?: ItemFetchOptions): Promise<WebSocketHandle>;
// cacheSave(item: Item, options?: {
//     saveContent: boolean;
// }): Uint8Array;
// cacheLoad(cache: Uint8Array): Item;

module ItemManager =
    [<Import("ItemManager", "Etebase")>]
    let itemManager: ItemManager = jsNative
