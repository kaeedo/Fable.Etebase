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


Jest.describe.only (
    "Item manager tests",
    fun () ->
        Jest.test (
            "Should create item",
            (promise {
                let! loggedIn = Account.account.login (TestHelpers.username, TestHelpers.password, TestHelpers.server)

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
)
