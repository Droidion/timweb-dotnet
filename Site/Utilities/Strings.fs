module Site.Utilities.Strings

open System.Globalization

let getCulture lang =
    CultureInfo.CreateSpecificCulture(if lang = "ru" then "ru-RU" else "en-US")
    
/// Cleans up language identifier for SQL queries.
/// Makes sure it's one of the expected variants.
let coerceLanguage (lang: string) = if lang = "ru" then lang else "en"