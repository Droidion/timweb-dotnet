module Site.Clients

open FSharp.Json
open Npgsql.FSharp
open Site.Persistence
open Site.DomainModel

let private getClientsQuery (lang: string) : string =
    $"SELECT b.name_{lang}                                        AS name,
             b.logo                                               as logo,
             SUM(COALESCE((e.date_finish - e.date_start + 1), 1)) AS seminar_days,
             COUNT(vink_date)                                     AS vink_days,
             JSONB_AGG(distinct c.name_{lang})                    AS clients
    FROM brands b
             JOIN clients c ON c.brand_id = b.id
             JOIN events_clients ec ON c.id = ec.client_id
             JOIN events e ON ec.event_id = e.id
    GROUP BY b.name_{lang}, b.logo
    ORDER BY seminar_days DESC, vink_days DESC"

let private mapper (read: RowReader) : Client =
    { name = read.text "name"
      logo = read.text "logo"
      seminarDays = read.int "seminar_days"
      vinkDays = read.int "vink_days"
      clients = "clients" |> read.text |> Json.deserialize<string list> |> String.concat ", " }
    
let getClients (lang: string) : Async<Client list> =
    pgConn
    |> Sql.query (getClientsQuery lang)
    |> Sql.executeAsync mapper
    |> Async.AwaitTask   