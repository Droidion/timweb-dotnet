module Site.Templates.Pages.Rating

open Giraffe.ViewEngine
open Site.Templates

/// Renders HTML
let view (lang: string) (path: string) =
    let pageTitle = "Rating Page"

    [ h1 [] [ str pageTitle ] ]
    |> App.view pageTitle lang path