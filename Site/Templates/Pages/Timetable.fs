module Site.Templates.Pages.Timetable

open Giraffe.ViewEngine
open Models
open Site.Utilities.Dates
open Site.Database
open Site.Templates
open Site.Language.Helpers
open Site.Language.Translations

let private addSelectedClass (pageYear: int) (buttonYear: int) : string =
    if pageYear = buttonYear then
        "button_selected"
    else
        ""

/// Renders HTML
let view (lang: string) (year: int) (path: string) : XmlNode =

    // Load data from DB
    let years =
        YearsProvider.getYears
        |> Async.RunSynchronously
        |> List.map (fun el -> el.year)

    let timetableFuture =
        TimetableProvider.getTimetable lang year TimetableDirection.Future
        |> Async.RunSynchronously

    let timetablePast =
        TimetableProvider.getTimetable lang year TimetableDirection.Past
        |> Async.RunSynchronously

    let pageTitle = "Brands"

    let tableHeader =
        div [ _class "table__header" ] [
            div [ _class "table__cell" ] [
                getTranslation "Date" lang |> str
            ]
            div [ _class "table__cell" ] [
                getTranslation "Seminar" lang |> str
            ]
            div [ _class "table__cell" ] [
                getTranslation "City" lang |> str
            ]
            div [ _class "table__cell" ] [
                getTranslation "Clients" lang |> str
            ]
        ]

    /// Renders timetable table
    let renderTable timetable =
        let mutable pastMonth = ""

        div [ _class "table" ] [
            tableHeader
            for el in timetable do
                let curMonth = el.dateStart.ToString("MMMM", lang |> getCulture)

                if pastMonth <> curMonth then
                    pastMonth <- curMonth

                    div [ _class "table__row table__row-subtitle" ] [
                        div [ _class "table__cell table__cell-subtitle" ] [
                            curMonth |> str
                        ]
                    ]

                div [ _class "table__row table__row-regular" ] [
                    div [ _class "table__cell" ] [
                        formatYear el.dateStart el.dateFinish lang |> rawText
                    ]
                    div [ _class "table__cell" ] [
                        el.seminar |> string |> str
                    ]
                    div [ _class "table__cell" ] [
                        el.city |> string |> str
                    ]
                    div [ _class "table__cell" ] [
                        el.clients |> string |> str
                    ]
                ]
        ]

    // HTML
    [ h1 [] [
        getTranslation "Timetable" lang |> str
      ]
      h2 [] [
          getTranslation "FutureSeminars" lang |> str
      ]
      renderTable timetableFuture
      div [ _class "years-selector" ] [
          for el in years do
              a [ _href $"/{lang}/timetable/{string el}" ] [
                  button [ _class $"years-selector__button button {addSelectedClass year el}" ] [
                      str (string el)
                  ]
              ]
      ]
      h2 [] [
          getTranslation "PastSeminars" lang |> str
      ]
      renderTable timetablePast ]
    |> App.view pageTitle lang path