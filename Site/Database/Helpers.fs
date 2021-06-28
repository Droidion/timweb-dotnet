namespace Site.Database

open System.Data
open System.Threading.Tasks
open Npgsql.FSharp

/// Database-related helpers
module Helpers =

    /// Database connection
    let mutable conn = ""

    /// Returns language id to use in SQL requests
    let chooseLange (tryLang: string) =
        match tryLang with
        | "ru" -> "ru"
        | _ -> "en"

    // Makes simple SELECT to the database
    let query<'a>
        (sql: string)
        (data: (string * SqlValue) list)
        (mapper: RowReader -> 'a)
        : Async<'a list> =
            
        conn
        |> Sql.connect
        |> Sql.query sql
        |> Sql.parameters data
        |> Sql.executeAsync mapper
        |> Async.AwaitTask
