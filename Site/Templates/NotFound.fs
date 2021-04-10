namespace Site.Templates

open Giraffe.GiraffeViewEngine
open Site.Templates

/// Page Not Found template
module NotFound =
    
    /// Renders HTML
    let view =
        let pageTitle = "Page not found"

        [ h1 [] [ str pageTitle ]
          p [] [ str "We are sorry" ] ]
        |> App.view pageTitle
