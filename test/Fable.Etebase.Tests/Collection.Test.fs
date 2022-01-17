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


Jest.describe (
    "Collection tests",
    fun () ->
        Jest.test (
            "Should something",
            (promise {
                let! loggedIn = Account.account.login (TestHelpers.username, TestHelpers.password, TestHelpers.server)

                let collectionManager =
                    loggedIn.getCollectionManager ()

                let randomContent =
                    TestHelpers.randomStr (20)

                let item =
                    { CollectionItem.Name = TestHelpers.randomStr (5)
                      Description = TestHelpers.randomStr (20)
                      Color = "#0f0" }

                let! collection = collectionManager.create ("fable.etebase.testCol", item, randomContent)

                Jest.expect(1).toBeFalsy ()
            })
        )
)
