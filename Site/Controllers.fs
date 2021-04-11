namespace Site

open Saturn
open Site.Templates.Pages
open Microsoft.AspNetCore.Http

/// Saturn app controllers
module Controllers =

    /// Gets language id from request context
    let private browserLang (ctx: HttpContext) =
        let lang =
            (ctx.Request.Headers.["Accept-Language"] |> string).[..1]

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
        let handler =
            fun (ctx: HttpContext) ->
                (Company.view lang ctx.Request.Path.Value)
                |> Controller.renderHtml ctx

        controller { index handler }

    /// Timetable page controller
    let seminarsController lang =
        let handler =
            fun (ctx: HttpContext) ->
                (Seminars.view lang ctx.Request.Path.Value)
                |> Controller.renderHtml ctx

        controller { index handler }

    /// Timetable page controller
    let customersController lang =
        let handler =
            fun (ctx: HttpContext) ->
                (Customers.view lang ctx.Request.Path.Value)
                |> Controller.renderHtml ctx

        controller { index handler }

    /// Timetable page controller
    let geographyController lang =
        let handler =
            fun (ctx: HttpContext) ->
                (Geography.view lang ctx.Request.Path.Value)
                |> Controller.renderHtml ctx

        controller { index handler }

    /// Timetable page controller
    let feedbackController lang =
        let handler =
            fun (ctx: HttpContext) ->
                (Feedback.view lang ctx.Request.Path.Value)
                |> Controller.renderHtml ctx

        controller { index handler }

    /// Timetable page controller
    let contactController lang =
        let handler =
            fun (ctx: HttpContext) ->
                (Contact.view lang ctx.Request.Path.Value)
                |> Controller.renderHtml ctx

        controller { index handler }

    /// Timetable page controller
    let ratingController lang =
        let handler =
            fun (ctx: HttpContext) ->
                (Rating.view lang ctx.Request.Path.Value)
                |> Controller.renderHtml ctx

        controller { index handler }

    /// Timetable page controller
    let vinkController lang =
        let handler =
            fun (ctx: HttpContext) ->
                (Vink.view lang ctx.Request.Path.Value)
                |> Controller.renderHtml ctx

        controller { index handler }

    /// Timetable page controller
    let talksController lang =
        let handler =
            fun (ctx: HttpContext) ->
                (Talks.view lang ctx.Request.Path.Value)
                |> Controller.renderHtml ctx

        controller { index handler }

    /// Controller for choosing the language
    let langController =
        controller {
            subController "/timetable" timetableController
            subController "/company" companyController
            subController "/seminars" seminarsController
            subController "/customers" customersController
            subController "/geography" geographyController
            subController "/feedback" feedbackController
            subController "/contact" contactController
            subController "/rating" ratingController
            subController "/vink" vinkController
            subController "/talks" talksController

            index (fun ctx -> Controller.redirect ctx $"/{browserLang ctx}/company")
        }
