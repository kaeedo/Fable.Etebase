module CollectionManager.Test

open Fable.Core
open Fable.Jester
open Fable.Etebase
open Collection.Test

Jest.describe (
    "Collection Manager tests",
    (fun () ->
        Jest.test (
            "Should list collections",
            (promise {
                let! loggedIn =
                    Account.account.login (
                        TestHelpers.testData.User1.Username,
                        TestHelpers.testData.User1.Password,
                        TestHelpers.testData.Server
                    )

                let collectionManager =
                    loggedIn.getCollectionManager ()

                let item =
                    { CollectionItem.Name = TestHelpers.randomStr (5)
                      Description = TestHelpers.randomStr (20)
                      Color = "#0f0" }

                let! col1 = collectionManager.create ("fable.etebase.testCol", item, TestHelpers.randomStr (20))

                let! col2 = collectionManager.create ("fable.etebase.testCol", item, TestHelpers.randomStr (20))

                do! collectionManager.upload (col1)
                do! collectionManager.upload (col2)

                let! resultList = collectionManager.list ("fable.etebase.testCol")

                Jest
                    .expect(resultList.data.Length)
                    .toBeGreaterThan (1)
            })
        )

        Jest.test (
            "Should save and load to cache",
            (promise {
                let! loggedIn =
                    Account.account.login (
                        TestHelpers.testData.User1.Username,
                        TestHelpers.testData.User1.Password,
                        TestHelpers.testData.Server
                    )

                let collectionManager =
                    loggedIn.getCollectionManager ()

                let randomContent =
                    TestHelpers.randomStr (20)

                let item =
                    { CollectionItem.Name = TestHelpers.randomStr (5)
                      Description = TestHelpers.randomStr (20)
                      Color = "#0f0" }

                let! collection = collectionManager.create ("fable.etebase.testCol", item, randomContent)

                let cached =
                    collectionManager.cacheSave (collection)

                Jest.expect(cached.Length).toBeGreaterThan (0)

                let fromCache =
                    collectionManager.cacheLoad (cached)

                let! fromCacheContent = fromCache.getContentString ()
                let! collectionContent = collection.getContentString ()

                Jest
                    .expect(fromCacheContent)
                    .toEqual (collectionContent)
            })
        )

        Jest.test (
            "Should fetch single item",
            (promise {
                let! loggedIn =
                    Account.account.login (
                        TestHelpers.testData.User1.Username,
                        TestHelpers.testData.User1.Password,
                        TestHelpers.testData.Server
                    )

                let collectionManager =
                    loggedIn.getCollectionManager ()

                let randomContent =
                    TestHelpers.randomStr (20)

                let item =
                    { CollectionItem.Name = TestHelpers.randomStr (5)
                      Description = TestHelpers.randomStr (20)
                      Color = "#0f0" }

                let! collection = collectionManager.create ("fable.etebase.testCol", item, randomContent)

                do! collectionManager.upload (collection)

                let uid = collection.uid

                let! fetched = collectionManager.fetch (uid)

                Jest.expect(fetched.uid).toEqual (uid)
            })
        )

        Jest.test (
            "Should get item manager",
            (promise {
                let! loggedIn =
                    Account.account.login (
                        TestHelpers.testData.User1.Username,
                        TestHelpers.testData.User1.Password,
                        TestHelpers.testData.Server
                    )

                let collectionManager =
                    loggedIn.getCollectionManager ()

                let randomContent =
                    TestHelpers.randomStr (20)

                let item =
                    { CollectionItem.Name = TestHelpers.randomStr (5)
                      Description = TestHelpers.randomStr (20)
                      Color = "#0f0" }

                let! collection = collectionManager.create ("fable.etebase.testCol", item, randomContent)

                Jest
                    .expect(collectionManager.getItemManager (collection))
                    .toBeDefined ()
            })
        )

        Jest.test (
            "Should get collection member manager",
            (promise {
                let! loggedIn =
                    Account.account.login (
                        TestHelpers.testData.User1.Username,
                        TestHelpers.testData.User1.Password,
                        TestHelpers.testData.Server
                    )

                let collectionManager =
                    loggedIn.getCollectionManager ()

                let randomContent =
                    TestHelpers.randomStr (20)

                let item =
                    { CollectionItem.Name = TestHelpers.randomStr (5)
                      Description = TestHelpers.randomStr (20)
                      Color = "#0f0" }

                let! collection = collectionManager.create ("fable.etebase.testCol", item, randomContent)

                Jest
                    .expect(collectionManager.getMemberManager (collection))
                    .toBeDefined ()
            })
        )

        )
)
