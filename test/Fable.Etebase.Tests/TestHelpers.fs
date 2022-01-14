[<RequireQualifiedAccess>]
module TestHelpers

let randomStr =
    let chars =
        "ABCDEFGHIJKLMNOPQRSTUVWUXYZabcdefghijklmnopqrstuvwxyz0123456789"

    let charsLen = chars.Length
    let random = System.Random()

    fun len ->
        let randomChars =
            [| for i in 0..len -> chars.[random.Next(charsLen)] |]

        new System.String(randomChars)

let username = "JessicaHyde"
let password = "Mr. Rabbit"
let email = "jessicahyde@example.com"
let server = "http://172.20.4.120:3735"
