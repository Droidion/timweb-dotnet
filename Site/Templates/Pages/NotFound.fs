namespace Site.Templates.Pages

open Giraffe.ViewEngine
open Site.Templates

/// Page Not Found template
module NotFound =
    
    /// Renders HTML
    let view lang path =
        let pageTitle = "Page not found"

        [ h1 [] [ str pageTitle ]
          p [] [ str "We are sorry" ] ]
        |> App.view pageTitle lang path
