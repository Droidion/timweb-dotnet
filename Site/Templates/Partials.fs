namespace Site.Templates

open System
open Giraffe.GiraffeViewEngine
open Site.Templates

/// Partial HTMLs
module Partials =
    
    /// Renders top menu item
    let private getTopMenuEl icon text =
        div [ _class "header__menu-item" ] [
            div [ _class "header__menu-icon" ] [ icon ]
            div [ _class "header__menu-name" ] [ str text ]
        ]
        
    let private getCurYear =
        DateTime.Now.Year
        
    let private menuItems =
        [
            ("Timetable", $"/en/timetable/{getCurYear}")
            ("Seminars", "/seminars")
            ("Customers", "/customers")
            ("Geography", "/geography")
            ("Feedback", "/feedback")
            ("Contact Us", "/contact")
            ("VINK Rating", "/vink-rating")
        ]

    /// Header
    let header =
        [ div [ _class "header__left" ] [
            div [ _class "header__logo" ] [ Svg.timLogo ]
            div [ _class "header__title" ] [
                div [ _class "header__name" ] [
                    str "TIM Group"
                ]
                div [ _class "header__about" ] [
                    str "Trainings & Computer Simulations"
                ]
            ]
          ]
          div [ _class "header__right" ] [
              getTopMenuEl Svg.companyIcon "Company"
              getTopMenuEl Svg.vinkIcon "Simulation VINK"
              getTopMenuEl Svg.negotiationsIcon "Simulation Negotiations"
          ] ]

    /// Footer
    let footer =
        [ div [] [ str $"© 2005–{getCurYear} TIM Group. All right reserved." ] ]
    
    let menu = div [ _class "main-menu" ] [
        for txt, link in menuItems do
            a [ _class "main-menu__el"; _href link ] [ str txt ]
    ]
