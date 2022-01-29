module ItemManager.Test

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


Jest.describe (
    "Item manager tests",
    fun () ->
        Jest.test (
            "Should create item",
            (promise {
                let! loggedIn =
                    Account.login (
                        TestHelpers.testData.User1.Username,
                        TestHelpers.testData.User1.Password,
                        TestHelpers.testData.Server
                    )

                let collectionManager =
                    loggedIn.getCollectionManager ()

                let randomCollectionContent =
                    TestHelpers.randomStr (20)

                let collectionItem =
                    { CollectionItem.Name = TestHelpers.randomStr (5)
                      Description = TestHelpers.randomStr (20)
                      Color = "#0f0" }

                let! collection =
                    collectionManager.create ("fable.etebase.testCol", collectionItem, randomCollectionContent)

                let itemManager =
                    collectionManager.getItemManager (collection)

                let randomItemContent =
                    TestHelpers.randomStr (20)

                let item =
                    { Item.Name = TestHelpers.randomStr (5)
                      Description = TestHelpers.randomStr (20)
                      ItemId = 42 }

                let! item = itemManager.create (item, randomItemContent)

                let! itemContent = item.getContentString ()

                Jest
                    .expect(itemContent)
                    .toEqual (randomItemContent)
            })
        )

        Jest.test (
            "Should save and load from cache",
            (promise {
                let! loggedIn =
                    Account.login (
                        TestHelpers.testData.User1.Username,
                        TestHelpers.testData.User1.Password,
                        TestHelpers.testData.Server
                    )

                let collectionManager =
                    loggedIn.getCollectionManager ()

                let randomCollectionContent =
                    TestHelpers.randomStr (20)

                let collectionItem =
                    { CollectionItem.Name = TestHelpers.randomStr (5)
                      Description = TestHelpers.randomStr (20)
                      Color = "#0f0" }

                let! collection =
                    collectionManager.create ("fable.etebase.testCol", collectionItem, randomCollectionContent)

                let itemManager =
                    collectionManager.getItemManager (collection)

                let randomItemContent =
                    TestHelpers.randomStr (20)

                let item =
                    { Item.Name = TestHelpers.randomStr (5)
                      Description = TestHelpers.randomStr (20)
                      ItemId = 42 }

                let! item = itemManager.create (item, randomItemContent)

                let cacheData = itemManager.cacheSave (item)

                let loadedItem =
                    itemManager.cacheLoad (cacheData)

                Jest.expect(loadedItem.uid).toEqual (item.uid)
            })
        )

        Jest.test (
            "Should upload and download content",
            (promise {
                let! loggedIn =
                    Account.login (
                        TestHelpers.testData.User1.Username,
                        TestHelpers.testData.User1.Password,
                        TestHelpers.testData.Server
                    )

                let collectionManager =
                    loggedIn.getCollectionManager ()

                let randomCollectionContent =
                    TestHelpers.randomStr (20)

                let collectionItem =
                    { CollectionItem.Name = TestHelpers.randomStr (5)
                      Description = TestHelpers.randomStr (20)
                      Color = "#0f0" }

                let! collection =
                    collectionManager.create ("fable.etebase.testCol", collectionItem, randomCollectionContent)

                do! collectionManager.upload (collection)

                let itemManager =
                    collectionManager.getItemManager (collection)

                let randomItemContent =
                    TestHelpers.randomStr (20)

                let item =
                    { Item.Name = TestHelpers.randomStr (5)
                      Description = TestHelpers.randomStr (20)
                      ItemId = 42 }

                let! item = itemManager.create (item, randomItemContent)
                let uid = item.uid

                do! itemManager.uploadContent (item)
                do! itemManager.downloadContent (item)

                Jest.expect(item.uid).toBe (uid)
            })
        )

        Jest.test (
            "Should list items",
            (promise {
                let! loggedIn =
                    Account.login (
                        TestHelpers.testData.User1.Username,
                        TestHelpers.testData.User1.Password,
                        TestHelpers.testData.Server
                    )

                let collectionManager =
                    loggedIn.getCollectionManager ()

                let randomCollectionContent =
                    TestHelpers.randomStr (20)

                let collectionItem =
                    { CollectionItem.Name = TestHelpers.randomStr (5)
                      Description = TestHelpers.randomStr (20)
                      Color = "#0f0" }

                let! collection =
                    collectionManager.create ("fable.etebase.testCol", collectionItem, randomCollectionContent)

                do! collectionManager.upload (collection)

                let itemManager =
                    collectionManager.getItemManager (collection)

                let randomItemContent =
                    TestHelpers.randomStr (20)

                let item =
                    { Item.Name = TestHelpers.randomStr (5)
                      Description = TestHelpers.randomStr (20)
                      ItemId = 42 }

                let! item = itemManager.create (item, randomItemContent)
                do! itemManager.batch ([| item |])

                let! list = itemManager.list ()

                Jest.expect(list.data).toHaveLength (1)
            })
        )
)
