namespace Site

open Saturn
open Site.Templates.Pages
open Microsoft.AspNetCore.Http

/// Saturn app controllers
module Controllers =

    /// Gets language id from request context
    let private browserLang (ctx: HttpContext) =
        let lang = (ctx.Request.Headers.["Accept-Language"] |> string).[..1]

        match lang with
        | "ru" -> "ru"
        | _ -> "en"

    /// Timetable page controller
    let timetableController lang =
        controller {
            show
                (fun ctx year ->
                    (Timetable.view lang year ctx.Request.Path.Value)
                    |> Controller.renderHtml ctx)
        }

    /// Timetable page controller (it's also index page)
    let companyController lang =
        let handler = fun (ctx: HttpContext) -> (Company.view lang ctx.Request.Path.Value) |> Controller.renderHtml ctx
        controller { index handler }

    /// Controller for choosing the language 
    let langController =
        controller {
            subController "/timetable" timetableController
            subController "/company" companyController
            index (fun ctx -> Controller.redirect ctx $"/{browserLang ctx}/company")
        }
