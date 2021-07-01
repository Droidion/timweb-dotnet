namespace Site.Templates

open Giraffe.ViewEngine
open Site.Templates

/// Top-level layout template
module App =

    let private getLangLink (lang: string) (path: string) = $"/{lang}{path.[3..]}"

    /// Renders HTML
    let view (pageTitle: string) (lang: string) (path: string) (content: XmlNode list) =
        html [ _lang lang ] [
            head [] [
                title [] [ str pageTitle ]
                meta [ _charset "utf-8" ]
                meta [ _name "viewport"
                       _content "width=device-width, initial-scale=1.0" ]
                link [ _rel "apple-touch-icon"
                       _sizes "180x180"
                       _href "/apple-touch-icon.png" ]
                link [ _rel "icon"
                       _type "image/png"
                       _href "/favicon-32x32.png"
                       _sizes "32x32" ]
                link [ _rel "icon"
                       _type "image/png"
                       _href "/favicon-16x16.png"
                       _sizes "16x16" ]
                link [ _rel "manifest"
                       _href "/manifest.json" ]
                link [ _rel "mask-icon"
                       _href "safari-pinned-tab.svg"
                       _color "#5bbad5" ]
                link [ _rel "preload"
                       attr "as" "style"
                       _href "/main.css" ]
                link [ _rel "stylesheet"
                       _href "/main.css" ]
            ]
            body [] [
                div [ _class "layout" ] [
                    div [ _class "content-wrapper" ] [
                        header [ _class "header" ] (Partials.header lang)
                        Partials.menu lang
                        Partials.languageSwitcher path
                        section [ _class "content" ] content
                        footer [] Partials.footer
                    ]
                ]
            ]
        ]
