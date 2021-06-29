module Site.Templates.Pages.Vink

open Giraffe.ViewEngine
open Site.Templates

/// Renders HTML
let view (lang: string) (path: string) =
    let pageTitle = "Vink Page"

    [ h1 [] [ str pageTitle ] ]
    |> App.view pageTitle lang path
