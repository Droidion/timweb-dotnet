namespace Site

open Saturn
open Site.Controllers

/// Saturn app routers
module Router =
    /// Router is just a forward to parent controller 
    let main = router {
        forward "" langController
    }

