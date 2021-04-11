namespace Site.Templates.Pages

open Giraffe.ViewEngine
open Site.Templates

/// Main page template
module Vink =

    /// Renders HTML
    let view (lang: string) (path: string) =
        let pageTitle = "Vink Page"

        [ h1 [] [ str pageTitle ] ]
        |> App.view pageTitle lang path
