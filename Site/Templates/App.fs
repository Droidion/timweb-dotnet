namespace Site.Templates

open Giraffe.GiraffeViewEngine
open Site.Templates

/// Top-level layout template
module App =
    
    /// Renders HTML
    let view (pageTitle: string) (content: XmlNode list) =
        html [] [
            head [] [
                title [] [ str pageTitle ]
                link [ _rel "stylesheet"
                       _href "/main.css" ]
            ]
            body [] [
                div [ _class "layout" ] [
                    div [ _class "content-wrapper" ] [
                        header [ _class "header" ] Partials.header
                        Partials.menu
                        section [ _class "content" ] content
                        footer [] Partials.footer
                    ]
                ]
            ]
        ]
