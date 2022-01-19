module Utilities.Test

open Fable.Jester
open Fable.Etebase

Jest.describe.only (
    "Base64 encoding",
    fun () ->
        Jest.beforeAll (promise { do! Utilities.ready })

        Jest.test (
            "Should encode to base64",
            fun () ->
                let toEncode = [| 1uy; 2uy; 3uy |]
                let encoded = Utilities.toBase64 (toEncode)

                Fable.Core.JS.console.log(TestHelpers.testData.User1.Username)
                Fable.Core.JS.console.log(TestHelpers.testData.User2.Username)

                Jest.expect(encoded).toEqual ("AQID")
        )

        Jest.test (
            "Should decode from base64",
            fun () ->
                let encoded = "AQID"
                let decoded = Utilities.fromBase64 (encoded)

                Fable.Core.JS.console.log(TestHelpers.testData.User1.Password)
                Fable.Core.JS.console.log(TestHelpers.testData.User2.Password)

                Jest
                    .expect(decoded)
                    .toMatchObject ([| 1uy; 2uy; 3uy |])
        )

        Jest.test (
            "Should generate random bytes",
            fun () ->
                let randomBytes = Utilities.randomBytes 32

                Fable.Core.JS.console.log(TestHelpers.testData.User1.Email)
                Fable.Core.JS.console.log(TestHelpers.testData.User2.Email)

                Jest.expect(randomBytes).toHaveLength (32)
        )
)
