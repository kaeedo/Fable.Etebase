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


Jest.describe.only (
    "Collection Sharing tests",
    fun () ->
        Jest.test (
            "Should share collection",
            (promise {
                // Login as JessicaHyde and invite WilsonWilson
                let! jessicaHyde =
                    Account.account.login (
                        TestHelpers.testData.User1.Username,
                        TestHelpers.testData.User1.Password,
                        TestHelpers.testData.Server
                    )

                let jhInvitationManager =
                    jessicaHyde.getInvitationManager ()

                let! wilsonWilsonProfile = jhInvitationManager.fetchUserProfile (TestHelpers.testData.User2.Username)

                let collectionManager =
                    jessicaHyde.getCollectionManager ()


                let randomContent =
                    TestHelpers.randomStr (20)

                let item =
                    { CollectionItem.Name = TestHelpers.randomStr (5)
                      Description = TestHelpers.randomStr (20)
                      Color = "#0f0" }

                let! collection = collectionManager.create ("fable.etebase.testCol", item, randomContent)
                do! collectionManager.upload (collection)

                do!
                    jhInvitationManager.invite (
                        collection,
                        TestHelpers.testData.User2.Username,
                        wilsonWilsonProfile.pubkey,
                        CollectionAccessLevel.ReadOnly
                    )


                // Login as WilsonWilson and accept the invitation
                let! wilsonWilson =
                    Account.account.login (
                        TestHelpers.testData.User2.Username,
                        TestHelpers.testData.User2.Password,
                        TestHelpers.testData.Server
                    )

                let wwInvitationManager =
                    wilsonWilson.getInvitationManager ()

                let! invitations = wwInvitationManager.listIncoming ()
                do! wwInvitationManager.accept (invitations.data.[0])

                // Check collection membership
                let memberManager =
                    collectionManager.getMemberManager (collection)

                let! members = memberManager.list ()

                let members =
                    members.data |> Array.map (fun m -> m.username)

                Jest
                    .expect(members)
                    .toEqual (
                        [| TestHelpers.testData.User1.Username.ToLowerInvariant()
                           TestHelpers.testData.User2.Username.ToLowerInvariant() |]
                    )
            })
        )
)
