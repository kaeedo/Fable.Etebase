namespace Fable.Etebase

open Fable.Core
open Fable.Core.JS
open Fable.Core.JsInterop

type CollectionInvitationManager =
    abstract pubKey: byte array
    abstract disinvite: invitation: SignedInvitation -> Promise<obj>
    abstract accept: invitation: SignedInvitation -> Promise<obj>
    abstract reject: invitation: SignedInvitation -> Promise<obj>

    abstract invite:
        collection: Collection * username: string * pubkey: byte array * accessLevel: CollectionAccessLevel ->
            Promise<unit>

    abstract fetchUserProfile: username: string -> Promise<UserProfile>

    abstract listIncoming: ?options: FetchOptions -> Promise<ListResponse<SignedInvitationRead>>

    abstract listOutgoing: ?options: FetchOptions -> Promise<ListResponse<SignedInvitationRead>>

module CollectionInvitationManager =
    [<Import("CollectionInvitationManager", "Etebase")>]
    let collectionInvitationManager: CollectionInvitationManager =
        jsNative
