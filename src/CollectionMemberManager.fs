namespace Fable.Etebase

open Fable.Core
open Fable.Core.JS
open Fable.Core.JsInterop

type CollectionMemberManager =
    abstract modifyAccessLevel: username: string * accessLevel: CollectionAccessLevel -> Promise<obj>
    abstract remove: username: string -> Promise<obj>
    abstract leave: unit -> Promise<obj>
    abstract list: ?options: FetchOptions -> Promise<ListResponse<CollectionMember>>

module CollectionMemberManager =
    [<Import("CollectionMemberManager", "Etebase")>]
    let collectionMemberManager: CollectionMemberManager =
        jsNative
