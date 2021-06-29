module Site.Utilities.Dates

open System
open Site.Language.Helpers

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