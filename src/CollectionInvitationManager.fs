namespace Fable.Etebase

open Fable.Core
open Fable.Core.JS
open Fable.Core.JsInterop

type SignedInvitation =
    { uid: string
      version: int
      username: string
      collection: string
      accessLevel: CollectionAccessLevel
      signedEncryptionKey: byte array
      fromUsername: string option
      fromPubkey: byte array }

type UserProfile = { pubkey: byte array }

type SignedInvitationRead =
    { uid: string
      version: int
      username: string
      Collection: string
      accessLevel: CollectionAccessLevel
      signedEncryptionKey: byte array
      fromUsername: string option
      fromPubkey: byte array }

type CollectionInvitationListResponse<'a> =
    { iterator: string
      ``done``: bool
      data: 'a array }

type InvitationFetchOptions =
    { limit: int option
      iterator: string option }

type CollectionInvitationManager =
    abstract pubKey: byte array
    abstract disinvite: invitation: SignedInvitation -> Promise<obj>
    abstract accept: invitation: SignedInvitation -> Promise<obj>
    abstract reject: invitation: SignedInvitation -> Promise<obj>

    abstract invite:
        collection: Collection * username: string * pubkey: byte array * accessLevel: CollectionAccessLevel ->
            Promise<unit>

    abstract fetchUserProfile: username: string -> Promise<UserProfile>

    abstract listIncoming:
        ?options: InvitationFetchOptions -> Promise<CollectionInvitationListResponse<SignedInvitationRead>>

    abstract listOutgoing:
        ?options: InvitationFetchOptions -> Promise<CollectionInvitationListResponse<SignedInvitationRead>>

module CollectionInvitationManager =
    [<Import("CollectionInvitationManager", "Etebase")>]
    let collectionInvitationManager: CollectionInvitationManager =
        jsNative
