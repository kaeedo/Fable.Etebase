namespace Fable.Etebase

open Fable.Core
open Fable.Core.JS
open Fable.Core.JsInterop

//https://github.com/fable-compiler/fable3-samples/blob/main/interop/public/MyClass.js
//https://github.com/fable-compiler/fable3-samples/blob/main/interop/src/App.fs

type User = {| username: string; email: string |}

type LoginResponseUser =
    {| username: string
       email: string
       pubkey: byte array
       encryptedContent: byte array |}


type Account =
    abstract serverUrl: string with get, set
    abstract user: LoginResponseUser with get, set
    abstract authToken: string option with get, set

    //save(encryptionKey_?: Uint8Array): Promise<base64>;
    //getCollectionManager(): CollectionManager;
    //getInvitationManager(): CollectionInvitationManager;
    abstract logout: unit -> Promise<unit>
    abstract fetchToken: unit -> Promise<unit>
    abstract changePassword: string -> Promise<unit>
    abstract getDashboardUrl: unit -> Promise<string>


type IAccount =
    abstract login: username: string * password: string * ?serverUrl: string -> Promise<Account>
    abstract signup: user: User * password: string * ?serverUrl: string -> Promise<Account>
    abstract isEtebaseServer: serverUrl: string -> Promise<bool>
// static restore(accountDataStored_: base64, encryptionKey_?: Uint8Array): Promise<Account>;

module Account =
    [<Import("Account", "Etebase")>]
    let account: IAccount = jsNative
