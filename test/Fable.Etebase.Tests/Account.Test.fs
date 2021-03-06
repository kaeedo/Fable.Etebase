module Account.Test

open Fable.Jester
open Fable.Etebase


Jest.describe (
    "Account tests",
    fun () ->
        Jest.beforeEach (promise { do! Promise.sleep 500 })

        Jest.test (
            "Should be valid server",
            (promise {
                do!
                    Jest
                        .expect(Account.isEtebaseServer (TestHelpers.testData.Server))
                        .resolves.toBeTruthy ()
            })
        )

        Jest.test (
            "Should login",
            (promise {
                let! loggedIn =
                    Account.login (
                        TestHelpers.testData.User1.Username,
                        TestHelpers.testData.User1.Password,
                        TestHelpers.testData.Server
                    )

                Jest
                    .expect(loggedIn.serverUrl)
                    .toEqual (TestHelpers.testData.Server)

                Jest
                    .expect(loggedIn.user.pubkey)
                    .toHaveLength (32)

                Jest
                    .expect(loggedIn.user.encryptedContent)
                    .toHaveLength (104)

                Jest
                    .expect(loggedIn.user.username.ToLowerInvariant())
                    .toEqual (TestHelpers.testData.User1.Username.ToLowerInvariant())

                Jest
                    .expect(loggedIn.user.email)
                    .toEqual (TestHelpers.testData.User1.Email)

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

                let randomUser =
                    { Fable.Etebase.User.email = randomEmail
                      username = randomUsername }

                let! response = Account.signup (randomUser, randomPassword, TestHelpers.testData.Server)

                Jest
                    .expect(response.user.email.ToLowerInvariant())
                    .toEqual (randomEmail.ToLowerInvariant())
            })

        )

        Jest.test (
            "Should logout",
            (promise {
                let! loggedIn =
                    Account.login (
                        TestHelpers.testData.User1.Username,
                        TestHelpers.testData.User1.Password,
                        TestHelpers.testData.Server
                    )

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

                let randomUser =
                    { User.email = randomEmail
                      username = randomUsername }

                let! response = Account.signup (randomUser, randomPassword, TestHelpers.testData.Server)
                do! Promise.sleep 200

                Jest
                    .expect(response.user.email.ToLowerInvariant())
                    .toEqual (randomEmail.ToLowerInvariant())

                let newRandomPassword =
                    TestHelpers.randomStr (15)

                do! response.changePassword (newRandomPassword)
                do! Promise.sleep 200

                do! response.logout ()
                do! Promise.sleep 200

                let! loggedInWithNewPassowrd =
                    Account.login (randomUsername, newRandomPassword, TestHelpers.testData.Server)

                Jest
                    .expect(loggedInWithNewPassowrd.user.username)
                    .toEqual (randomUsername.ToLowerInvariant())
            })

        )

        Jest.test (
            "Should fetch token",
            (promise {
                let! loggedIn =
                    Account.login (
                        TestHelpers.testData.User1.Username,
                        TestHelpers.testData.User1.Password,
                        TestHelpers.testData.Server
                    )

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
                let! loggedIn =
                    Account.login (
                        TestHelpers.testData.User1.Username,
                        TestHelpers.testData.User1.Password,
                        TestHelpers.testData.Server
                    )

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
                let! loggedIn =
                    Account.login (
                        TestHelpers.testData.User1.Username,
                        TestHelpers.testData.User1.Password,
                        TestHelpers.testData.Server
                    )

                let encryptionKey =
                    Utilities.randomBytes (32)

                let! savedSession = loggedIn.save (encryptionKey)

                Jest
                    .expect(savedSession.Length)
                    .toBeGreaterThan (1)

                let! restored = Account.restore (savedSession, encryptionKey)

                Jest
                    .expect(restored.user.username)
                    .toEqual (TestHelpers.testData.User1.Username.ToLowerInvariant())
            })
        )

        Jest.test (
            "Should get collection manager",
            (promise {
                let! loggedIn =
                    Account.login (
                        TestHelpers.testData.User1.Username,
                        TestHelpers.testData.User1.Password,
                        TestHelpers.testData.Server
                    )

                Jest
                    .expect(loggedIn.getCollectionManager ())
                    .toBeDefined ()
            })
        )

        Jest.test (
            "Should get invitation manager",
            (promise {
                let! loggedIn =
                    Account.login (
                        TestHelpers.testData.User1.Username,
                        TestHelpers.testData.User1.Password,
                        TestHelpers.testData.Server
                    )

                Jest
                    .expect(loggedIn.getCollectionManager ())
                    .toBeDefined ()
            })
        )
)
