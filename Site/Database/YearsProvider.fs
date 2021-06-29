module Site.Database.YearsProvider

open Models
open Npgsql.FSharp
open Site.Database.Helpers


/// SQL request for getting list of years
let private query = "SELECT EXTRACT(YEAR FROM date_start)::INTEGER AS year
             FROM events
             GROUP BY year
             ORDER BY year DESC;"

/// Maps SQL query result to F# record
let private mapper (read: RowReader) : Year = { year = read.int "year" }

/// Returns the list of years
let getYears : Async<Year list> =
    connectDb
    |> Sql.query query
    |> Sql.executeAsync mapper
    |> Async.AwaitTask
