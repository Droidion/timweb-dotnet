namespace Site.Templates.Pages

open Giraffe.GiraffeViewEngine
open Models
open Site.Database
open Site.Templates

/// Timetable page template
module Timetable =
    let private dateFormat = "dd.MM.yyyy"

    /// Renders HTML
    let view (lang: string) (year: int) =
        
        // Load data from DB
        let years =
            YearsProvider.list |> Async.RunSynchronously

        let timetableFuture =
            TimetableProvider.list lang year TimetableDirection.Future
            |> Async.RunSynchronously

        let timetablePast =
            TimetableProvider.list lang year TimetableDirection.Back
            |> Async.RunSynchronously

        let pageTitle = "Brands"

        let tableHeader =
            tr [] [
                td [] [ str "Date Start" ]
                td [] [ str "Date Finish" ]
                td [] [ str "Seminar" ]
                td [] [ str "City" ]
                td [] [ str "Clients" ]
            ]

        /// Renders timetable table
        let renderTable timetable =
            table [ _class "timetable" ] [
                tableHeader
                for el in timetable do
                    tr [] [
                        td [] [
                            el.dateStart.ToString dateFormat |> str
                        ]
                        td [] [
                            (match el.dateFinish with
                             | Some d -> d.ToString dateFormat
                             | None -> "")
                            |> str
                        ]
                        td [] [ el.seminar |> string |> str ]
                        td [] [ el.city |> string |> str ]
                        td [] [ el.clients |> string |> str ]
                    ]
            ]

        // HTML
        [ h1 [] [ str "Foo" ]
          p [] [ str $"Your language is {lang}" ]
          h2 [] [ str "Future Seminars" ]
          renderTable timetableFuture
          div [] [
              for year in years do
                  a [ _href $"/{lang}/timetable/{string year}" ] [
                      str (string year)
                  ]
          ]
          h2 [] [ str "Past Seminars" ]
          renderTable timetablePast ]
        |> App.view pageTitle
