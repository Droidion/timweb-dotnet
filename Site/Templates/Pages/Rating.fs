namespace Site.Templates.Pages

open Giraffe.ViewEngine
open Site.Templates

/// Main page template
module Rating =
    
    /// Renders HTML
    let view (lang: string) (path: string) =
        let pageTitle = "Rating Page"
        [
            h1 [] [ str pageTitle ]
        ] |> App.view pageTitle lang path

