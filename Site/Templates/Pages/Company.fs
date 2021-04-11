namespace Site.Templates.Pages

open Giraffe.GiraffeViewEngine
open Site.Templates

/// Main page template
module Company =
    
    /// Renders HTML
    let view (lang: string) =
        let pageTitle = "Index Page"
        [
            h1 [] [ str pageTitle ]
            p [] [ str "Hello world!" ]
        ] |> App.view pageTitle

