namespace Site.Templates.Pages

open Giraffe.ViewEngine
open Site.Templates

/// Main page template
module Contact =
    
    /// Renders HTML
    let view (lang: string) (path: string) =
        let pageTitle = "Contact Page"
        [
            h1 [] [ str pageTitle ]
        ] |> App.view pageTitle lang path

