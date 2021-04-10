namespace Site

open Saturn
open Site.Templates

/// Saturn app controllers
module Controllers =
    
    /// Timetable page controller
    let timetableController (lang, year) = controller {
        index (fun ctx -> (Timetable.view lang year) |> Controller.renderHtml ctx)
    }
