namespace Site.Templates

open System
open Giraffe.GiraffeViewEngine
open Site.Templates
open Site.Translations

/// Partial HTMLs
module Partials =

    /// Renders top menu item
    let private getTopMenuEl icon text url =
        a [ _href url ] [
            div [ _class "header__menu-item" ] [
                div [ _class "header__menu-icon" ] [
                    icon
                ]
                div [ _class "header__menu-name" ] [
                    str text
                ]
            ]
        ]


    let private getCurYear = DateTime.Now.Year

    let private menuItems lang =
        [ ("Timetable", $"/{lang}/timetable/{getCurYear}")
          ("Seminars", "/seminars")
          ("Customers", "/customers")
          ("Geography", "/geography")
          ("Feedback", "/feedback")
          ("Contact Us", "/contact")
          ("VINK Rating", "/vink-rating") ]

    /// Header
    let header lang =
        [ a [ _href "/" ] [
            div [ _class "header__left" ] [
                div [ _class "header__logo" ] [
                    Svg.timLogo
                ]
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
              getTopMenuEl Svg.companyIcon "Company" $"/{lang}/company"
              getTopMenuEl Svg.vinkIcon "Simulation VINK" $"/{lang}/vink"
              getTopMenuEl Svg.negotiationsIcon "Simulation Negotiations" $"/{lang}/negotiations"
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
                a [ _class "main-menu__el"; _href link ] [
                    str txt
                ]
        ]
