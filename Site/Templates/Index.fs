namespace Site.Templates

open Giraffe.GiraffeViewEngine
open Site.Templates

/// Main page template
module Index =
    
    /// Renders HTML
    let view =
        let pageTitle = "Index Page"
        [
            h1 [] [ str pageTitle ]
            p [] [ str "Hello world!" ]
        ] |> App.view pageTitle

