open Saturn
open Microsoft.Extensions.Hosting
open Microsoft.AspNetCore.Builder
open Site.Router

/// Saturn app
let app =
    application {
        use_router mainRouter
        url "http://0.0.0.0:8080"
        use_static "static"
        use_gzip
        memory_cache

        app_config
            (fun app ->
                let env = Environment.getWebHostEnvironment app

                if (env.IsDevelopment()) then
                    app.UseDeveloperExceptionPage()
                else
                    app)
    }


[<EntryPoint>]
let main _ =
    run app
    0
