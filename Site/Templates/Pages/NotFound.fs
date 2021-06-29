module Site.Templates.Pages.NotFound

open Giraffe.ViewEngine
open Site.Templates

/// Renders HTML
let view lang path =
    let pageTitle = "Page not found"

    [ h1 [] [ str pageTitle ]
      p [] [ str "We are sorry" ] ]
    |> App.view pageTitle lang path