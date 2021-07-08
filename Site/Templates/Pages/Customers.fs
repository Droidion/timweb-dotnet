module Site.Templates.Pages.Customers

open Giraffe.ViewEngine
open Site.Clients
open Site.Templates
open Site.Localization

/// Renders HTML
let view (lang: string) (path: string) =
    let pageTitle = getTranslationSingular "Customers" lang
    let clients = getClients lang |> Async.RunSynchronously

    [ h1 [] [ str pageTitle ]
      div [ _class "flex-list" ] [
          for client in clients do
              div [ _class "client" ] [
                  div [ _class "brand-logo" ] [
                      img [ _src $"https://timseminar.ru/img/logos/{client.logo}" ]
                  ]
                  div [] [
                      h2 [] [ str $"{client.name}" ]

                      p [] [
                          let seminarDays = getTranslationPlural "SeminarDays" lang client.seminarDays
                          let vinks = getTranslationPlural "Vinks" lang client.vinkDays
                          let mainStr = $"{client.seminarDays} {seminarDays}"

                          str (
                              if client.vinkDays > 0 then
                                  $"{mainStr}, {client.vinkDays} {vinks}"
                              else
                                  mainStr
                          )
                      ]

                      p [ _class "pale-text" ] [
                          str $"{client.clients}"
                      ]
                  ]
              ]
      ] ]

    |> App.view pageTitle lang path
