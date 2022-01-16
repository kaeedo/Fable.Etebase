namespace Fable.Etebase

open Fable.Core
open Fable.Core.JS
open Fable.Core.JsInterop

type ItemManager =
    abstract derp:string
    // create<T>(meta: ItemMetadata<T>, content: Uint8Array | string): Promise<Item>;
    // fetch(itemUid: base64, options?: ItemFetchOptions): Promise<Item>;
    // list(options?: ItemFetchOptions): Promise<{
    //     data: Item[];
    //     stoken: string;
    //     done: boolean;
    // }>;
    // itemRevisions(item: Item, options?: RevisionsFetchOptions): Promise<{
    //     data: Item[];
    //     iterator: string;
    //     done: boolean;
    // }>;
    // fetchUpdates(items: Item[], options?: ItemFetchOptions): Promise<{
    //     data: Item[];
    //     stoken: string;
    //     done: boolean;
    // }>;
    // fetchMulti(items: base64[], options?: ItemFetchOptions): Promise<{
    //     data: Item[];
    //     stoken: string;
    //     done: boolean;
    // }>;
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
