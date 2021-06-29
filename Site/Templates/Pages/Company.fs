module Site.Templates.Pages.Company

open Giraffe.ViewEngine
open Site.Templates

/// Renders HTML
let view (lang: string) (path: string) =
    let pageTitle = "Index Page"

    [ h1 [] [ str pageTitle ] ]
    |> App.view pageTitle lang path