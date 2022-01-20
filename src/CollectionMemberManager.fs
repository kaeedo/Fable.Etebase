namespace Fable.Etebase

open Fable.Core
open Fable.Core.JS
open Fable.Core.JsInterop

type CollectionMemberManager =
    abstract modifyAccessLevel: username: string * accessLevel: CollectionAccessLevel -> Promise<unit>
    abstract remove: username: string -> Promise<unit>
    abstract leave: unit -> Promise<unit>
    abstract list: ?options: FetchOptions -> Promise<ListResponse<CollectionMember>>

module CollectionMemberManager =
    [<Import("CollectionMemberManager", "etebase")>]
    let collectionMemberManager: CollectionMemberManager =
        jsNative
