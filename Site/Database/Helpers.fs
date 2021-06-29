module Site.Database.Helpers

open Npgsql.FSharp

let connectDb : Sql.SqlProps =
    System.Environment.GetEnvironmentVariable("ConnectionString")
    |> Sql.connect