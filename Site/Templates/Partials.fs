namespace Site.Templates

open System
open Giraffe.ViewEngine
open Site.Templates
open Site.Translations

/// Partial HTMLs
module Partials =

    /// Renders top menu item
    let private getTopMenuEl icon text url =
        a [ _href url; _class "dark" ] [
            div [ _class "header__menu-item" ] [
                div [ _class "header__menu-icon" ] [
                    icon
                ]
                div [ _class "header__menu-name" ] [
                    str text
                ]
            ]
        ]

    /// Constructs link for the given page with given language
    let private getLangLink (lang: string) (path: string) = $"/{lang}{path.[3..]}"

    /// Returns current year
    let private getCurYear = DateTime.Now.Year

    let private menuItems lang =
        [ (getTranslation "Timetable" lang, $"/{lang}/timetable/{getCurYear}")
          (getTranslation "Seminars" lang, $"/{lang}/seminars")
          (getTranslation "Customers" lang, $"/{lang}/customers")
          (getTranslation "Geography" lang, $"/{lang}/geography")
          (getTranslation "Feedback" lang, $"/{lang}/feedback")
          (getTranslation "Contact" lang, $"/{lang}/contact")
          (getTranslation "Rating" lang, $"/{lang}/rating") ]

    /// Header
    let header lang =
        [ a [ _href "/" ] [
            div [ _class "header__left" ] [
                img [ _src "/img/tim-logo.svg"; _class "header__logo" ]
                div [ _class "header__title" ] [
                    div [ _class "header__name" ] [
                        str <| getTranslation "CompanyName" lang
                    ]
                    div [ _class "header__about" ] [
                        str <| getTranslation "CompanyDescription" lang
                    ]
                ]
            ]
          ]

          div [ _class "header__right" ] [
              getTopMenuEl Svg.companyIcon (getTranslation "Company" lang) $"/{lang}/company"
              getTopMenuEl Svg.vinkIcon (getTranslation "Vink" lang) $"/{lang}/vink"
              getTopMenuEl Svg.negotiationsIcon (getTranslation "Talks" lang) $"/{lang}/talks"
          ] ]

    /// Footer
    let footer =
        [ div [] [
              str $"© 2005–{getCurYear} TIM Group. All right reserved."
          ] ]

    /// Main menu
    let menu (lang: string) =
        div [ _class "main-menu" ] [
            for txt, link in menuItems lang do
                a [ _class "main-menu__el dark"
                    _href link ] [
                    str txt
                ]
        ]

    /// Block for switching UI language
    let languageSwitcher (path: string) =
        div [ _class "lang-switcher" ] [
            div [ _class "lang-switcher__el" ] [
                a [ _href <| getLangLink "ru" path
                    _class "light" ] [
                    str "Русский"
                ]
            ]
            div [ _class "lang-switcher__el" ] [
                a [ _href <| getLangLink "en" path
                    _class "light" ] [
                    str "English"
                ]
            ]
        ]
