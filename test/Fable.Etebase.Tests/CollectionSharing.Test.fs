module CollectionSharing.Test

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


Jest.describe.skip (
    "Collection Sharing tests",
    fun () ->
        Jest.test (
            "Should modify access level",
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

                let memberManager =
                    collectionManager.getMemberManager (collection)

                Jest
                    .expect(collection.accessLevel)
                    .toEqual (CollectionAccessLevel.Admin)

                let! _ = memberManager.modifyAccessLevel ("TestHelpers.username", CollectionAccessLevel.ReadOnly)

                Jest.expect(collection.accessLevel).toEqual (5)


            })
        )
)
