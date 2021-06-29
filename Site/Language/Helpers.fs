module Site.Language.Helpers

open System.Globalization

let getCulture (lang: string) : CultureInfo =
    CultureInfo.CreateSpecificCulture(if lang = "ru" then "ru-RU" else "en-US")

/// Cleans up language identifier for SQL queries.
/// Makes sure it's one of the expected variants.
let coerceLanguage (lang: string) : string = if lang = "ru" then lang else "en"
