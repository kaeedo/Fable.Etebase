namespace Fable.Etebase

open Fable.Core
open Fable.Core.JS
open Fable.Core.JsInterop

type MemberFetchOptions =
    { limit: int option
      iterator: string option }

type CollectionMemberListResponse<'a> =
    { iterator: string
      ``done``: bool
      data: 'a array }

type CollectionMember =
    { username: string
      accessLevel: CollectionAccessLevel }

type CollectionMemberManager =
    abstract modifyAccessLevel: username: string * accessLevel: CollectionAccessLevel -> Promise<obj>
    abstract remove: username: string -> Promise<obj>
    abstract leave: unit -> Promise<obj>
    abstract list: ?options: MemberFetchOptions -> Promise<CollectionMemberListResponse<CollectionMember>>

module CollectionMemberManager =
    [<Import("CollectionMemberManager", "Etebase")>]
    let collectionMemberManager: CollectionMemberManager =
        jsNative
