module Site.Templates.Pages.Timetable

open Giraffe.ViewEngine
open Site.DomainModel
open Site.Utilities.Dates
open Site.Years
open Site.Timetable
open Site.Localization
open Site.Templates

let private addSelectedClass (pageYear: int) (buttonYear: int) : string =
    if pageYear = buttonYear then
        "button_selected"
    else
        ""

/// Renders HTML
let view (lang: string) (year: int) (path: string) : XmlNode =

    // Load data from DB
    let years =
        getYears
        |> Async.RunSynchronously
        |> List.map (fun el -> el.year)

    let timetableFuture =
        getTimetable lang year TimetableDirection.Future
        |> Async.RunSynchronously

    let timetablePast =
        getTimetable lang year TimetableDirection.Past
        |> Async.RunSynchronously

    let pageTitle = getTranslationSingular "Timetable" lang

    let tableHeader =
        div [ _class "table__header" ] [
            div [ _class "table__cell" ] [
                getTranslationSingular "Date" lang |> str
            ]
            div [ _class "table__cell" ] [
                getTranslationSingular "Seminar" lang |> str
            ]
            div [ _class "table__cell" ] [
                getTranslationSingular "City" lang |> str
            ]
            div [ _class "table__cell" ] [
                getTranslationSingular "Clients" lang |> str
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
    [ h1 [] [ pageTitle |> str ]
      h2 [] [
          getTranslationSingular "FutureSeminars" lang |> str
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
          getTranslationSingular "PastSeminars" lang |> str
      ]
      renderTable timetablePast ]
    |> App.view pageTitle lang path
