module Site.Templates.Pages.Talks

open Giraffe.ViewEngine
open Site.Templates

/// Renders HTML
let view (lang: string) (path: string) =
    let pageTitle = "Talks Page"

    [ h1 [] [ str pageTitle ] ]
    |> App.view pageTitle lang path
