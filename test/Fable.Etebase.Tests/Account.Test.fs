module Account.Test

open Fable.Jester
open Fable.Etebase


Jest.describe.skip (
    "Account tests",
    fun () ->
        Jest.test (
            "Should be valid server",
            (promise {
                do!
                    Jest
                        .expect(Account.account.isEtebaseServer (TestHelpers.server))
                        .resolves.toBeTruthy ()
            })

        )

        Jest.test (
            "Should login",
            (promise {
                let! loggedIn = Account.account.login (TestHelpers.username, TestHelpers.password, TestHelpers.server)

                Jest
                    .expect(loggedIn.serverUrl)
                    .toEqual (TestHelpers.server)

                Jest
                    .expect(loggedIn.user.pubkey)
                    .toHaveLength (32)

                Jest
                    .expect(loggedIn.user.encryptedContent)
                    .toHaveLength (104)

                Jest
                    .expect(loggedIn.user.username.ToLowerInvariant())
                    .toEqual (TestHelpers.username.ToLowerInvariant())

                Jest
                    .expect(loggedIn.user.email)
                    .toEqual (TestHelpers.email)

                do! loggedIn.logout ()
            })
        )

        Jest.test (
            "Should Signup",
            (promise {
                let randomUsername =
                    TestHelpers.randomStr (15)

                let randomEmail =
                    TestHelpers.randomStr (10) + "@example.com"

                let randomPassword =
                    TestHelpers.randomStr (15)

                let randomUser: Fable.Etebase.User =
                    {| email = randomEmail
                       username = randomUsername |}

                let! response = Account.account.signup (randomUser, randomPassword, TestHelpers.server)

                Jest
                    .expect(response.user.email.ToLowerInvariant())
                    .toEqual (randomEmail.ToLowerInvariant())
            })

        )

        Jest.test (
            "Should logout",
            (promise {
                let! loggedIn = Account.account.login (TestHelpers.username, TestHelpers.password, TestHelpers.server)

                Jest.expect(loggedIn.authToken).not.toBeNull ()

                do! loggedIn.logout ()

                Jest.expect(loggedIn.authToken).toBeNull ()
            })

        )

        Jest.test (
            "Should change password",
            (promise {
                let randomUsername =
                    TestHelpers.randomStr (15)

                let randomEmail =
                    TestHelpers.randomStr (10) + "@example.com"

                let randomPassword =
                    TestHelpers.randomStr (15)

                let randomUser: Fable.Etebase.User =
                    {| email = randomEmail
                       username = randomUsername |}

                let! response = Account.account.signup (randomUser, randomPassword, TestHelpers.server)

                Jest
                    .expect(response.user.email.ToLowerInvariant())
                    .toEqual (randomEmail.ToLowerInvariant())

                let newRandomPassword =
                    TestHelpers.randomStr (15)

                do! response.changePassword (newRandomPassword)
                do! response.logout ()

                let! loggedInWithNewPassowrd =
                    Account.account.login (randomUsername, newRandomPassword, TestHelpers.server)

                Jest
                    .expect(loggedInWithNewPassowrd.user.username)
                    .toEqual (randomUsername.ToLowerInvariant())
            })

        )

        Jest.test (
            "Should fetch token",
            (promise {
                let! loggedIn = Account.account.login (TestHelpers.username, TestHelpers.password, TestHelpers.server)

                Jest.expect(loggedIn.authToken).not.toBeNull ()
                loggedIn.authToken <- None
                Jest.expect(loggedIn.authToken).toBeUndefined ()

                do! loggedIn.fetchToken ()

                Jest.expect(loggedIn.authToken).not.toBeNull ()

                do! loggedIn.logout ()
            })

        )

        Jest.test (
            "Should get dashboard url",
            (promise {
                let! loggedIn = Account.account.login (TestHelpers.username, TestHelpers.password, TestHelpers.server)

                let! getDashboardResponse =
                    (loggedIn.getDashboardUrl ())
                        .catch (fun t -> t.ToString())

                // We expect an error message of "No dashborad" since we're using a test container
                Jest
                    .expect(getDashboardResponse)
                    .toEqual ("HTTPError: 400 This server doesn't have a user dashboard.")
            })
        )

        Jest.test (
            "Should save and restore session",
            (promise {
                let! loggedIn = Account.account.login (TestHelpers.username, TestHelpers.password, TestHelpers.server)

                let encryptionKey =
                    Utilities.randomBytes (32)

                let! savedSession = loggedIn.save (encryptionKey)

                Jest
                    .expect(savedSession.Length)
                    .toBeGreaterThan (1)

                let! restored = Account.account.restore (savedSession, encryptionKey)

                Jest
                    .expect(restored.user.username)
                    .toEqual (TestHelpers.username.ToLowerInvariant())
            })
        )

        Jest.test (
            "Should get collection manager",
            (promise {
                let! loggedIn = Account.account.login (TestHelpers.username, TestHelpers.password, TestHelpers.server)

                Jest
                    .expect(loggedIn.getCollectionManager ())
                    .toBeDefined ()
            })
        )

        Jest.test (
            "Should get invitation manager",
            (promise {
                let! loggedIn = Account.account.login (TestHelpers.username, TestHelpers.password, TestHelpers.server)

                Jest
                    .expect(loggedIn.getCollectionManager ())
                    .toBeDefined ()
            })
        )
)
