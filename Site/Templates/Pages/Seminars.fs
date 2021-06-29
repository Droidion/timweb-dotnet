module Site.Templates.Pages.Seminars

open Giraffe.ViewEngine
open Site.Templates

/// Renders HTML
let view (lang: string) (path: string) =
    let pageTitle = "Seminars Page"

    [ h1 [] [ str pageTitle ] ]
    |> App.view pageTitle lang path