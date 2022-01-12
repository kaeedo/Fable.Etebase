module Utilities.Test

open Fable.Jester
open Fable.Etebase

Jest.describe (
    "Base64 encoding",
    fun () ->
        Jest.beforeAll (
            async {
                // Required for weird WASM behaviour
                do! Async.Sleep 500
            }
        )

        Jest.test (
            "Should encode to base64",
            fun () ->
                let toEncode = [| 1uy; 2uy; 3uy |]
                let encoded = Utilities.toBase64 (toEncode)

                Jest.expect(encoded).toEqual ("AQID")
        )

        Jest.test (
            "Should decode from base64",
            fun () ->
                let encoded = "AQID"
                let decoded = Utilities.fromBase64 (encoded)

                Jest
                    .expect(decoded)
                    .toMatchObject ([| 1uy; 2uy; 3uy |])
        )

        Jest.test (
            "Should generate randomy bytes",
            fun () ->
                let randomBytes = Utilities.randomBytes 32
                Jest.expect(randomBytes).toHaveLength (32)
        )
)
