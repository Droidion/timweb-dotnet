module Site.Translations

open System
open Legivel.Serialization

type Translations = Map<string, Map<string, string>>

let mutable private translations : Translations option = None

/// Loads translations from YAML file
let loadTranslations : unit =
    let transResult =
        @"Translations.yaml"
        |> IO.File.ReadAllText
        |> Deserialize<Translations>
        |> List.head

    translations <-
        match transResult with
        | Success trans -> Some trans.Data
        | _ -> None

/// Returns translation for given key and language
let getTranslation (key: string) (lang: string) : string =
    match translations with
    | Some tr when Map.containsKey key tr ->
        if Map.containsKey lang tr.[key] then
            tr.[key].[lang]
        else
            ""
    | _ -> ""
