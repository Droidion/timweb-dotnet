module Site.Templates.Pages.Geography

open Giraffe.ViewEngine
open Site.Templates

/// Renders HTML
let view (lang: string) (path: string) =
    let pageTitle = "Geography Page"

    [ h1 [] [ str pageTitle ] ]
    |> App.view pageTitle lang path
