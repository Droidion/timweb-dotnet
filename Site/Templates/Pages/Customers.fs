module Site.Templates.Pages.Customers

open Giraffe.ViewEngine
open Site.Templates

/// Renders HTML
let view (lang: string) (path: string) =
    let pageTitle = "Customers Page"

    [ h1 [] [ str pageTitle ] ]
    |> App.view pageTitle lang path