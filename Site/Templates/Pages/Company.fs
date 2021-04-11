namespace Site.Templates.Pages

open Giraffe.ViewEngine
open Site.Templates

/// Main page template
module Company =

    /// Renders HTML
    let view (lang: string) (path: string) =
        let pageTitle = "Index Page"

        [ h1 [] [ str pageTitle ] ]
        |> App.view pageTitle lang path
