module Site.Router

open Saturn
open Site.Controllers

/// Router is just a forward to parent controller
let mainRouter = router { forward "" langController }
