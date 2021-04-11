namespace Site.Templates.Pages

open Giraffe.ViewEngine
open Site.Templates

/// Main page template
module Seminars =
    
    /// Renders HTML
    let view (lang: string) (path: string) =
        let pageTitle = "Seminars Page"
        [
            h1 [] [ str pageTitle ]
        ] |> App.view pageTitle lang path

