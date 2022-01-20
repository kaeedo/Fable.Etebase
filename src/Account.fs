namespace Fable.Etebase

open Fable.Core
open Fable.Core.JS
open Fable.Core.JsInterop

type Account =
    abstract serverUrl: string with get, set
    abstract user: LoginResponseUser with get, set
    abstract authToken: string option with get, set

    abstract getInvitationManager: unit -> CollectionInvitationManager
    abstract save: ?encryptionKey: byte array -> Promise<string>
    abstract getCollectionManager: unit -> CollectionManager
    abstract logout: unit -> Promise<unit>
    abstract fetchToken: unit -> Promise<unit>
    abstract changePassword: password: string -> Promise<unit>
    abstract getDashboardUrl: unit -> Promise<string>


type IAccount =
    abstract login: username: string * password: string * ?serverUrl: string -> Promise<Account>
    abstract signup: user: User * password: string * ?serverUrl: string -> Promise<Account>
    abstract isEtebaseServer: serverUrl: string -> Promise<bool>
    abstract restore: accountDataStored: string * ?encryptionKey: byte array -> Promise<Account>

module Account =
    [<Import("Account", "etebase")>]
    let account: IAccount = jsNative
