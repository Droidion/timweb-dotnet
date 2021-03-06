module Site.Templates.Pages.Contact

open Giraffe.ViewEngine
open Site.Templates

/// Renders HTML
let view (lang: string) (path: string) =
    let pageTitle = "Contact Page"

    [ h1 [] [ str pageTitle ] ]
    |> App.view pageTitle lang path