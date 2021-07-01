module Site.Templates.Pages.Customers

open Giraffe.ViewEngine
open Site.Database
open Site.Templates

/// Renders HTML
let view (lang: string) (path: string) =
    let pageTitle = "Customers Page"
    
    let clients =
        ClientsProvider.getClients lang
        |> Async.RunSynchronously

    [ h1 [] [ str pageTitle ]
      for client in clients do
          h2 [] [str $"{client.name}"]
          div [] [str $"{client.seminarDays} seminar days"]
          div [] [str $"{client.vinkDays} VINKs"]
          div [] [str $"{client.clients}"]]
    |> App.view pageTitle lang path