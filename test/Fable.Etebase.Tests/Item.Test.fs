module Item.Test

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

type Item =
    { Name: string
      ItemId: int
      Description: string }
    interface ItemMetadata with
        member this.name = Some this.Name
        member this.description = Some this.Description
        member this.color = None
        member this.mtime = None
        member this.``type`` = None

type ItemOther =
    { Name: string
      Extra: string
      Description: string }
    interface ItemMetadata with
        member this.name = Some this.Name
        member this.description = Some this.Description
        member this.color = None
        member this.mtime = None
        member this.``type`` = None

Jest.describe (
    "item tests",
    fun () ->
        Jest.test (
            "Should verify",
            (promise {
                let! loggedIn =
                    Account.login (
                        TestHelpers.testData.User1.Username,
                        TestHelpers.testData.User1.Password,
                        TestHelpers.testData.Server
                    )

                let collectionManager =
                    loggedIn.getCollectionManager ()

                let randomContent =
                    TestHelpers.randomStr (20)

                let collectionItem =
                    { CollectionItem.Name = TestHelpers.randomStr (5)
                      Description = TestHelpers.randomStr (20)
                      Color = "#0f0" }

                let! collection = collectionManager.create ("fable.etebase.testCol", collectionItem, randomContent)

                let itemManager =
                    collectionManager.getItemManager (collection)

                let item =
                    { Item.Name = TestHelpers.randomStr (5)
                      Description = TestHelpers.randomStr (20)
                      ItemId = 42 }

                let! createdItem = itemManager.create (item, TestHelpers.randomStr (20))

                do! collectionManager.upload (collection)

                Jest.expect(createdItem.verify ()).toBe (true)
            })
        )

        Jest.test (
            "Should delete",
            (promise {
                let! loggedIn =
                    Account.login (
                        TestHelpers.testData.User1.Username,
                        TestHelpers.testData.User1.Password,
                        TestHelpers.testData.Server
                    )

                let collectionManager =
                    loggedIn.getCollectionManager ()

                let randomContent =
                    TestHelpers.randomStr (20)

                let collectionItem =
                    { CollectionItem.Name = TestHelpers.randomStr (5)
                      Description = TestHelpers.randomStr (20)
                      Color = "#0f0" }

                let! collection = collectionManager.create ("fable.etebase.testCol", collectionItem, randomContent)

                let itemManager =
                    collectionManager.getItemManager (collection)

                let item =
                    { Item.Name = TestHelpers.randomStr (5)
                      Description = TestHelpers.randomStr (20)
                      ItemId = 42 }

                let! createdItem = itemManager.create (item, TestHelpers.randomStr (20))

                do! collectionManager.upload (collection)

                createdItem.delete ()

                Jest.expect(createdItem.isDeleted).toBe (true)
            })
        )

        Jest.test (
            "Should set and get meta data",
            (promise {
                let! loggedIn =
                    Account.login (
                        TestHelpers.testData.User1.Username,
                        TestHelpers.testData.User1.Password,
                        TestHelpers.testData.Server
                    )

                let collectionManager =
                    loggedIn.getCollectionManager ()

                let randomContent =
                    TestHelpers.randomStr (20)

                let collectionItem =
                    { CollectionItem.Name = TestHelpers.randomStr (5)
                      Description = TestHelpers.randomStr (20)
                      Color = "#0f0" }

                let! collection = collectionManager.create ("fable.etebase.testCol", collectionItem, randomContent)

                let itemManager =
                    collectionManager.getItemManager (collection)

                let item =
                    { Item.Name = TestHelpers.randomStr (5)
                      Description = TestHelpers.randomStr (20)
                      ItemId = 42 }

                let! createdItem = itemManager.create (item, TestHelpers.randomStr (20))

                do! collectionManager.upload (collection)

                let otherMetadata =
                    { ItemOther.Description = TestHelpers.randomStr (30)
                      Name = "New Item"
                      Extra = "1fe" }

                createdItem.setMeta (otherMetadata)

                let metaData: ItemOther =
                    createdItem.getMeta ()

                Jest.expect(metaData.Extra).toBe ("1fe")
            })
        )

        Jest.test (
            "Should get content",
            (promise {
                let! loggedIn =
                    Account.login (
                        TestHelpers.testData.User1.Username,
                        TestHelpers.testData.User1.Password,
                        TestHelpers.testData.Server
                    )

                let collectionManager =
                    loggedIn.getCollectionManager ()

                let randomContent =
                    TestHelpers.randomStr (20)

                let collectionItem =
                    { CollectionItem.Name = TestHelpers.randomStr (5)
                      Description = TestHelpers.randomStr (20)
                      Color = "#0f0" }

                let! collection = collectionManager.create ("fable.etebase.testCol", collectionItem, randomContent)

                let itemManager =
                    collectionManager.getItemManager (collection)

                let item =
                    { Item.Name = TestHelpers.randomStr (5)
                      Description = TestHelpers.randomStr (20)
                      ItemId = 42 }

                let randomContent =
                    TestHelpers.randomStr (20)

                let! createdItem = itemManager.create (item, randomContent)

                do! collectionManager.upload (collection)

                let! content = createdItem.getContentString ()

                Jest.expect(content).toBe (randomContent)
            })
        )
)
