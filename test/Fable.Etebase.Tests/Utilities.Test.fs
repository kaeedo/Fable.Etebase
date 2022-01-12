module Utilities.Test

open Fable.Jester
open Fable.Etebase

Jest.describe (
    "Base64 encoding",
    fun () ->
        Jest.test (
            "Should encode to base64",
            async {
                // Required for weird WASM behaviour
                do! Async.Sleep 500
                let email = "test+something@example.com"
                let encoded = Utilities.toBase64 (email)

                Jest
                    .expect(encoded)
                    .toEqual ("dGVzdCtzb21ldGhpbmdAZXhhbXBsZS5jb20")
            }
        )
)
