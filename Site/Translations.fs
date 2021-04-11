namespace Site

open System 
open Legivel.Serialization

type Trans = Map<string,Map<string,string>> 

module Translations =
    
    let mutable private trans: Trans option = None
    
    /// Loads translations from YAML file
    let loadTranslations: unit =
        let yaml = IO.File.ReadAllText @"Translations.yaml"
        let parsed = Deserialize<Trans> yaml
        let foo: DeserializeResult<Trans> = parsed.[0]
        trans <- match foo with | Success res -> Some res.Data | _ -> None 

    /// Returns translation for given key and language
    let getTranslation (key: string) (lang: string) : string =
        match trans with
        | Some tr when Map.containsKey key tr ->
            if Map.containsKey lang tr.[key] then tr.[key].[lang] else ""
        | _ -> ""