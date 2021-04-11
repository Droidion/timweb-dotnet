open Saturn
open Microsoft.Extensions.Hosting
open Microsoft.AspNetCore.Builder
open FSharp.Configuration
open Site
open Site.Database
open Site.Translations

// Load db connection string from file and store in memory
type Settings = YamlConfig<"Config.yaml">
let config = Settings()
Helpers.conn <- config.DB.ConnectionString
loadTranslations

/// Saturn app
let app =
    application {
        use_router Router.main
        url "http://0.0.0.0:8080"
        use_static "static"
        use_gzip
        memory_cache
        app_config (fun app ->
            let env = Environment.getWebHostEnvironment app
            if (env.IsDevelopment()) then app.UseDeveloperExceptionPage() else app)
    }


[<EntryPoint>]
let main _ =
    run app
    0
