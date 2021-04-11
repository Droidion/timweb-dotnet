namespace Site

open System
open Legivel.Serialization

type Trans = Map<string, Map<string, string>>

module Translations =

    let mutable private trans : Trans option = None

    /// Loads translations from YAML file
    let loadTranslations : unit =
        let transResult =
            @"Translations.yaml"
            |> IO.File.ReadAllText 
            |> Deserialize<Trans>
            |> List.head

        trans <-
            match transResult with
            | Success trans -> Some trans.Data
            | _ -> None

    /// Returns translation for given key and language
    let getTranslation (key: string) (lang: string) : string =
        match trans with
        | Some tr when Map.containsKey key tr ->
            if Map.containsKey lang tr.[key] then
                tr.[key].[lang]
            else
                ""
        | _ -> ""
