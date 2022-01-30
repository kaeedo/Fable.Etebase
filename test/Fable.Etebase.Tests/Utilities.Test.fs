module Utilities.Test

open Expect
open Expect.Dom
open WebTestRunner
open Fable.Etebase

describe "Utilities tests" <| fun () ->
    it "Should encode to base64" <| fun () ->
        promise {
            do! Utilities.ready

            let toEncode = [| 1uy; 2uy; 3uy |]
            let encoded = Utilities.toBase64 (toEncode)

            encoded
            |> Expect.equal "AQID"
        }

    it "Should decode from base64" <| fun () ->
        promise {
            do! Utilities.ready

            let encoded = "AQID"
            let decoded = Utilities.fromBase64 (encoded)

            decoded
            |> Expect.equal [| 1uy; 2uy; 3uy |]
        }

    it "Should generate random bytes" <| fun () ->
        promise {
            do! Utilities.ready

            let randomBytes = Utilities.randomBytes 32

            randomBytes.Length
            |> Expect.equal 32
        }
