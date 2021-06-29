module Site.Web.Router

open Saturn
open Site.Web.Controllers

/// Router is just a forward to parent controller
let main = router { forward "" langController }
