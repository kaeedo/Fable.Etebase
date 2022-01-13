module Account.Test

open Fable.Jester
open Fable.Etebase

Jest.describe (
    "Account tests",
    fun () ->
        Jest.test (
            "Should be valid server",
            (promise {
                do!
                    Jest
                        .expect(Account.account.isEtebaseServer ("https://www.duckduckgo.com/"))
                        .resolves.toBeFalsy ()

                do!
                    Jest
                        .expect(Account.account.isEtebaseServer ("https://api.etebase.com/partner/etesync/"))
                        .resolves.toBeTruthy ()
            })

        )
)
