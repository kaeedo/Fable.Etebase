namespace Fable.Etebase

open Fable.Core
open Fable.Core.JS
open Fable.Core.JsInterop

type CollectionMemberManager =
    abstract derp:string
    // list(options?: MemberFetchOptions): Promise<import("./OnlineManagers").CollectionMemberListResponse<import("./OnlineManagers").CollectionMember>>;
    // remove(username: string): Promise<{}>;
    // leave(): Promise<{}>;
    // modifyAccessLevel(username: string, accessLevel: CollectionAccessLevel): Promise<{}>;

module CollectionMemberManager =
    [<Import("CollectionMemberManager", "Etebase")>]
    let collectionMemberManager: CollectionMemberManager = jsNative
