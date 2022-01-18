namespace Fable.Etebase

open Fable.Core
open Fable.Core.JS
open Fable.Core.JsInterop

type Item =
    abstract derp: string
// verify(): boolean;
// setMeta<T>(meta: ItemMetadata<T>): void;
// getMeta<T>(): ItemMetadata<T>;
// setContent(content: Uint8Array | string): Promise<void>;
// getContent(outputFormat?: OutputFormat.Uint8Array): Promise<Uint8Array>;
// getContent(outputFormat?: OutputFormat.String): Promise<string>;
// delete(preserveContent?: boolean): void;
// get uid(): string;
// get etag(): string;
// get isDeleted(): boolean;
// get isMissingContent(): boolean;
// _clone(): Item;

module Item =
    [<Import("Item", "Etebase")>]
    let item: Item = jsNative
