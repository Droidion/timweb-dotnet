namespace Site.Templates.Pages

open System
open System.Globalization
open Giraffe.ViewEngine
open Models
open Site.Database
open Site.Templates
open Site.Translations

/// Timetable page template
module Timetable =
    let private dateFormat = "dd.MM.yyyy"

    let private addSelectedClass (pageYear: int) (buttonYear: int) : string =
        if pageYear = buttonYear then
            "button_selected"
        else
            ""

    let private getCulture lang =
        CultureInfo.CreateSpecificCulture(if lang = "ru" then "ru-RU" else "en-US")

    /// Returns formatted date string for all combinations of start and finish dates
    let formatYear (start: DateTime) (finish: DateTime option) (lang: string) : string =
        let culture = lang |> getCulture

        // Date format like 15 December
        let dateFormat = "d MMMM"

        // Add span tag to color year differently in the UI
        let formattedYear (year: int) : string = $"<span>{year}</span>"

        // Formats date with locale like 15 December <span>2022</span>, or 15 декабря <span>2022</span>
        let fullDate (year: DateTime) : string =
            $"{year.ToString(dateFormat, culture)} {formattedYear year.Year}"

        match (start, finish) with
        // Only start date, format it like 10 December 2025
        | start, None -> $"{fullDate start}"
        // Different years, format like 10 december 2025 – 5 January 2026
        | start, Some finish when start.Year <> finish.Year -> $"{fullDate start} — {fullDate finish}"
        // Same years
        | start, Some finish when start.Year = finish.Year ->
            match (start, finish) with
            // Different months, format like 28 April – 3 May 2025
            | start, finish when start.Month <> finish.Month -> $"{start.ToString(dateFormat, culture)} — {fullDate finish}"
            // Same month, format like 3–4 June 2025
            | start, finish when start.Month = finish.Month -> $"{start.Day}–{fullDate finish}"
            | _, _ -> ""
        | _, _ -> ""

    /// Renders HTML
    let view (lang: string) (year: int) (path: string) : XmlNode =

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
                    let curMonth =
                        el.dateStart.ToString("MMMM", lang |> getCulture)

                    if pastMonth <> curMonth then
                        pastMonth <- curMonth

                        div [ _class "table__row table__row-subtitle" ] [
                            div [ _class "table__cell table__cell-subtitle" ] [
                                curMonth |> str
                            ]
                        ]

                    div [ _class "table__row table__row-regular" ] [
                        div [ _class "table__cell" ] [
                            formatYear el.dateStart el.dateFinish lang
                            |> rawText
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
        [ h1 [] [ getTranslation "Timetable" lang |> str ]
          h2 [] [ getTranslation "FutureSeminars" lang |> str ]
          renderTable timetableFuture
          div [ _class "years-selector" ] [
              for el in years do
                  a [ _href $"/{lang}/timetable/{string el}" ] [
                      button [ _class $"years-selector__button button {addSelectedClass year el}" ] [
                          str (string el)
                      ]
                  ]
          ]
          h2 [] [ getTranslation "PastSeminars" lang |> str ]
          renderTable timetablePast ]
        |> App.view pageTitle lang path
