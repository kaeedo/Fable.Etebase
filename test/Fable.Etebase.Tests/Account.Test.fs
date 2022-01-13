module Account.Test

open Fable.Jester
open Fable.Etebase

let username = ""
let password = ""
let email = ""

Jest.describe (
    "Account tests",
    fun () ->

        Jest.test.skip (
            // TODO: Something something cross origin null
            "Should be valid server",
            (promise {
                do!
                    Jest
                        .expect(Account.account.isEtebaseServer ("https://www.duckduckgo.com/"))
                        .resolves.toBeFalsy ()

                do!
                    Jest
                        .expect(Account.account.isEtebaseServer ("https://api.etebase.com"))
                        .resolves.toBeTruthy ()
            })

        )

        Jest.test (
            "Should login",
            (promise {
                let! loggedIn = Account.account.login (username, password)

                Jest
                    .expect(loggedIn.serverUrl)
                    .toEqual ("https://api.etebase.com")

                Jest
                    .expect(loggedIn.user.pubkey)
                    .toHaveLength (32)

                Jest
                    .expect(loggedIn.user.encryptedContent)
                    .toHaveLength (104)

                Jest
                    .expect(loggedIn.user.username)
                    .toEqual (username)

                Jest.expect(loggedIn.user.email).toEqual (email)

                do! loggedIn.logout ()
            })
        )

        Jest.test.skip (
            // TODO: run tests against localhost
            "Should Signup",
            (promise {
                let user: Fable.Etebase.User =
                    {| email = email
                       username = Utilities.toBase64 (email) |}

                let! response = Account.account.signup (user, password)

                Fable.Core.JS.console.log (response)

                Jest.expect("loggedIn.user.email").toEqual (email)
            })

        )

        Jest.test (
            "Should get dashboard url",
            (promise {
                let! loggedIn = Account.account.login (username, password)

                let! url = loggedIn.getDashboardUrl ()

                Jest
                    .expect(url.Substring(0, 53))
                    .toEqual ("https://dashboard.etebase.com/user/partner/dashboard/")

                do! loggedIn.logout ()
            })

        )

        Jest.test (
            "Should logout",
            (promise {
                let! loggedIn = Account.account.login (username, password)

                Jest.expect(loggedIn.authToken).not.toBeNull ()

                do! loggedIn.logout ()

                Jest.expect(loggedIn.authToken).toBeNull ()
            })

        )

        Jest.test (
            "Should fetch token",
            (promise {
                let! loggedIn = Account.account.login (username, password)

                Jest.expect(loggedIn.authToken).not.toBeNull ()
                loggedIn.authToken <- None
                Jest.expect(loggedIn.authToken).toBeUndefined ()

                do! loggedIn.fetchToken ()

                Jest.expect(loggedIn.authToken).not.toBeNull ()

                do! loggedIn.logout ()
            })

        )
)
