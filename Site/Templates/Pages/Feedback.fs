module Site.Templates.Pages.Feedback

open Giraffe.ViewEngine
open Site.Templates

/// Renders HTML
let view (lang: string) (path: string) =
    let pageTitle = "Feedback Page"

    [ h1 [] [ str pageTitle ] ]
    |> App.view pageTitle lang path