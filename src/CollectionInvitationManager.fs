namespace Fable.Etebase

open Fable.Core
open Fable.Core.JS
open Fable.Core.JsInterop

type CollectionInvitationManager =
    abstract pubKey: byte array
    // listIncoming(options?: InvitationFetchOptions): Promise<import("./OnlineManagers").CollectionInvitationListResponse<import("./EncryptedModels").SignedInvitationRead>>;
    // listOutgoing(options?: InvitationFetchOptions): Promise<import("./OnlineManagers").CollectionInvitationListResponse<import("./EncryptedModels").SignedInvitationRead>>;
    // accept(invitation: SignedInvitation): Promise<{}>;
    // reject(invitation: SignedInvitation): Promise<{}>;
    // fetchUserProfile(username: string): Promise<import("./OnlineManagers").UserProfile>;
    // invite(col: Collection, username: string, pubkey: Uint8Array, accessLevel: CollectionAccessLevel): Promise<void>;
    // disinvite(invitation: SignedInvitation): Promise<{}>;

module CollectionInvitationManager =
    [<Import("CollectionInvitationManager", "Etebase")>]
    let collectionInvitationManager: CollectionInvitationManager = jsNative
