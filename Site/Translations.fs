namespace Site


module Translations =

    let private translations =
        [ "CompanyName",
          [ "en", "TIM Group"
            "ru", "Группа ТИМ" ]
          |> Map.ofList
          "CompanyDescription",
          [ "en", "Trainings & Computer Modeling"
            "ru", "Тренинги и компьютерное моделирование" ]
          |> Map.ofList ]
        |> Map.ofList

    let getTranslation (key: string) (lang: string) : string =
        if Map.containsKey key translations
           && Map.containsKey lang translations.[key] then
            translations.[key].[lang]
        else
            ""
