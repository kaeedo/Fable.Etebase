namespace Fable.Etebase

open Fable.Core

type OutputFormat =
    | Uint8Array = 0
    | String = 1

type CollectionAccessLevel =
    | ReadOnly = 0
    | Admin = 1
    | ReadWrite = 2

type RemovedCollection = { uid: string }

type CacheSaveOptions = { saveContent: bool }

type ItemMetadata =
    abstract ``type``: string option
    abstract name: string option
    abstract mtime: string option
    abstract description: string option
    abstract color: string option

type FetchOptions =
    { limit: int option
      iterator: string option }

type CollectionMember =
    { username: string
      accessLevel: CollectionAccessLevel }

[<StringEnum; RequireQualifiedAccess>]
type PrefetchOption =
    | Auto
    | Medium

type CollectionManagerFetchOptions =
    { limit: int option
      stoken: string option
      prefetch: PrefetchOption option }

type ItemFetchOptions =
    { withCollection: bool option
      stoken: string option
      prefetch: PrefetchOption option }

type RevisionsFetchOptions =
    { prefetch: PrefetchOption option
      iterator: string option
      limit: int option }

type CollectionListResult<'a> =
    { data: 'a array
      removedMemberships: (RemovedCollection array) option
      stoken: string
      ``done``: bool }

type ItemListResult<'a> =
    { data: 'a array
      stoken: string
      ``done``: bool }

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

type ListResponse<'a> =
    { iterator: string
      ``done``: bool
      data: 'a array }

type User = { username: string; email: string }

type LoginResponseUser =
    { username: string
      email: string
      pubkey: byte array
      encryptedContent: byte array }
