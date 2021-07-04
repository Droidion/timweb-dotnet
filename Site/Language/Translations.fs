module Site.Language.Translations

open System
open Legivel.Serialization

let private loadTranslation<'a> (filename: string) : 'a =
    match filename |> IO.File.ReadAllText |> Deserialize<'a> |> List.head with
    | Success trans -> trans.Data
    | _ -> "Could not load translations" |> ArgumentException |> raise

let private translationsSingular : Map<string, Map<string, string>> =
    loadTranslation @"Language/TranslationsSingular.yaml"

let private translationsPlural : Map<string, Map<string, string list>> =
    loadTranslation @"Language/TranslationsPlural.yaml"

let private getNumIndex (quantity: int) (lang: string) : int =
    match lang with
    | "ru" ->
        match quantity with
        | q when q >= 11 && q <= 14 -> 2
        | q ->
            match q % 10 with
            | 1 -> 0
            | q when q >= 2 && q <= 4 -> 1
            | _ -> 2
    | _ -> if quantity = 1 then 0 else 1

/// Translation for given key and language
let getTranslationSingular (key: string) (lang: string) : string =
    try
        translationsSingular.[key].[lang]
    with
        | _ -> ""

/// Translation for given key and language for which can have forms based on quantity
let getTranslationPlural (key: string) (lang: string) (quantity: int) : string =
    try
        translationsPlural.[key].[lang].[getNumIndex quantity lang]
    with
        | _ -> ""
