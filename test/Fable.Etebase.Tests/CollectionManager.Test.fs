module CollectionManager.Test

open Fable.Core
open Fable.Jester
open Fable.Etebase

Jest.describe.only (
    "Collection Manager tests",
    (fun () ->
        Jest.test (
            "Should create and get content string",
            (promise {
                let! loggedIn = Account.account.login (TestHelpers.username, TestHelpers.password, TestHelpers.server)

                let collectionManager =
                    loggedIn.getCollectionManager ()

                let randomContent =
                    TestHelpers.randomStr (20)

                let! collection =
                    collectionManager.create (
                        "fable.etebase.testCol",
                        Some
                            {| name = TestHelpers.randomStr (5)
                               description = TestHelpers.randomStr (20)
                               color = "#0f0" |},
                        randomContent
                    )

                let! collectionContent = collection.getContentString ()

                Jest
                    .expect(collectionContent)
                    .toEqual (randomContent)
            })
        )

        Jest.test (
            "Should list collections",
            (promise {
                let! loggedIn = Account.account.login (TestHelpers.username, TestHelpers.password, TestHelpers.server)

                let collectionManager =
                    loggedIn.getCollectionManager ()

                let! col1 =
                    collectionManager.create (
                        "fable.etebase.testCol",
                        Some
                            {| name = TestHelpers.randomStr (5)
                               description = TestHelpers.randomStr (20)
                               color = "#0f0" |},
                        TestHelpers.randomStr (20)
                    )

                let! col2 =
                    collectionManager.create (
                        "fable.etebase.testCol",
                        Some
                            {| name = TestHelpers.randomStr (5)
                               description = TestHelpers.randomStr (20)
                               color = "#0f0" |},
                        TestHelpers.randomStr (20)
                    )

                do! collectionManager.upload (col1)
                do! collectionManager.upload (col2)

                let! resultList = collectionManager.list ("fable.etebase.testCol")

                Jest
                    .expect(resultList.data.Length)
                    .toBeGreaterThan (1)
            })
        ))
)
