module Collection.Test

open Fable.Core
open Fable.Jester
open Fable.Etebase

type CollectionItem =
    { Name: string
      Description: string
      Color: string }
    interface ItemMetadata with
        member this.name = Some this.Name
        member this.description = Some this.Description
        member this.color = Some this.Color
        member this.mtime = None
        member this.``type`` = None

type CollectionItemOther =
    { Name: string
      Description: string
      Extra: int
      Color: string }
    interface ItemMetadata with
        member this.name = Some this.Name
        member this.description = Some this.Description
        member this.color = Some this.Color
        member this.mtime = None
        member this.``type`` = None

Jest.describe (
    "Collection tests",
    fun () ->
        Jest.test (
            "Should get collection type",
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

                let collectionType =
                    collection.getCollectionType ()

                Jest
                    .expect(collectionType)
                    .toEqual ("fable.etebase.testCol")
            })
        )

        Jest.test (
            "Should verify",
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

                Jest.expect(collection.verify ()).toBe (true)
            })
        )

        Jest.test (
            "Should delete",
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

                collection.delete ()

                Jest.expect(collection.isDeleted).toBe (true)
            })
        )

        Jest.test (
            "Should set and get meta data",
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

                let otherMetadata =
                    { CollectionItemOther.Color = "#f0f"
                      Description = TestHelpers.randomStr (30)
                      Name = "New Item"
                      Extra = 1 }

                collection.setMeta (otherMetadata)

                let metaData: CollectionItemOther =
                    collection.getMeta ()

                Jest.expect(metaData.Extra).toBe (1)
            })
        )

        Jest.test (
            "Should create and get content string",
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

                let! collectionContent = collection.getContentString ()

                Jest
                    .expect(collectionContent)
                    .toEqual (randomContent)
            })
        )
)
