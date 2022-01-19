[<RequireQualifiedAccess>]
module TestHelpers

open Fable.Core
open Fable.Core.JS
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


// let username = "JessicaHyde"
// let password = "Mr. Rabbit"
// let email = "jessicahyde@example.com"
// let server = "http://172.18.122.191:3735"

let testData =
    // let testData =
    //     importDefault ("./testData.mjs")

    // let user1 =
    //     { TestUser.Username = testData?user1?username
    //       Password = testData?user1?password
    //       Email = testData?user1?email }

    // let user2 =
    //     { TestUser.Username = testData?user2?username
    //       Password = testData?user2?password
    //       Email = testData?user2?email }

    // { TestData.Server = testData?server
    //   User1 = user1
    //   User2 = user2 }

    let user1 =
        { TestUser.Username = "JessicaHyde"
          Password = "WhereAmI"
          Email = "jessicahyde@example.com" }

    let user2 =
        { TestUser.Username = "WilsonWilson"
          Password = "Mr. Rabbit"
          Email = "wilsonwilson@example.com" }

    { TestData.Server = "http://172.18.122.191:3735"
      User1 = user1
      User2 = user2 }
