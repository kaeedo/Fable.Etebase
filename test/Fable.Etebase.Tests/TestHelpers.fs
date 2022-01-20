[<RequireQualifiedAccess>]
module TestHelpers

open Fable.Core
open Fable.Core.JsInterop

type TestUser =
    { Username: string
      Password: string
      Email: string }

type TestData =
    { Server: string
      User1: TestUser
      User2: TestUser }

let randomStr =
    let chars =
        "ABCDEFGHIJKLMNOPQRSTUVWUXYZabcdefghijklmnopqrstuvwxyz0123456789"

    let charsLen = chars.Length
    let random = System.Random()

    fun len ->
        let randomChars =
            [| for i in 0..len -> chars.[random.Next(charsLen)] |]

        new System.String(randomChars)

[<Global>]
let private process: obj = jsNative

let testData =
    let user1 =
        { TestUser.Username = "JessicaHyde"
          Password = "WhereAmI"
          Email = "jessicahyde@example.com" }

    let user2 =
        { TestUser.Username = "WilsonWilson"
          Password = "Mr. Rabbit"
          Email = "wilsonwilson@example.com" }

    let serverAddress =
        let ci: string = process?env?CI

        if ci.Length > 0 then
            "http://etebase-server:3735"
        else
            "http://172.18.125.43:3735"

    { TestData.Server = serverAddress
      User1 = user1
      User2 = user2 }
