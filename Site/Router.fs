namespace Site

open Saturn
open Giraffe
open Site.Templates
open Site.Controllers

/// Saturn app routers
module Router =
    /// The main router (only one for now) 
    let main = router {
        get "/" (htmlView Index.view)
        forwardf "/%s/timetable/%i" timetableController
    }

