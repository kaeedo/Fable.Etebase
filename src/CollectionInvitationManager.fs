namespace Fable.Etebase

open Fable.Core
open Fable.Core.JS
open Fable.Core.JsInterop

type CollectionInvitationManager =
    abstract pubKey: byte array
    abstract disinvite: invitation: SignedInvitation -> Promise<unit>
    abstract accept: invitation: SignedInvitation -> Promise<unit>
    abstract reject: invitation: SignedInvitation -> Promise<unit>

    abstract invite:
        collection: Collection * username: string * pubkey: byte array * accessLevel: CollectionAccessLevel ->
            Promise<unit>

    abstract fetchUserProfile: username: string -> Promise<UserProfile>

    abstract listIncoming: ?options: FetchOptions -> Promise<ListResponse<SignedInvitation>>

    abstract listOutgoing: ?options: FetchOptions -> Promise<ListResponse<SignedInvitation>>
