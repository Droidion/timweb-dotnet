module Site.Database.TimetableProvider

open System.Data
open Models
open Npgsql.FSharp
open Site.Utilities.Strings
open Site.Database.Helpers

/// Returns SQL request for getting timetable
let private getTimetableSql (lang: string) (direction: TimetableDirection) : string =
    // Add year selection clause for past timetable requests
    let yearClause =
        if direction = TimetableDirection.Past then
            "AND EXTRACT(YEAR FROM e.date_start) = @Year"
        else
            ""

    $"SELECT e.date_start                     datestart,
             e.date_finish                    datefinish,
             s.name_{lang}                    seminar,
             c.name_{lang}                    city,
             STRING_AGG(c2.name_{lang}, ', ') clients
      FROM events e
             INNER JOIN cities c ON c.id = e.city_id
             INNER JOIN seminars s ON e.seminar_id = s.id
             INNER JOIN events_clients ec ON e.id = ec.event_id
             INNER JOIN clients c2 ON ec.client_id = c2.id
      WHERE e.date_start {unbox<char> direction} now() {yearClause}
      GROUP BY e.date_start, e.date_finish, s.name_{lang}, c.name_{lang}
      ORDER BY date_start desc;"

/// Maps SQL query result to F# record
let private mapper (read: RowReader) : Timetable =
    { dateStart = read.dateTime "datestart"
      dateFinish = read.dateTimeOrNone "datefinish"
      seminar = read.text "seminar"
      city = read.text "city"
      clients = read.text "clients" }

/// Returns the list of events
let getTimetable (lang: string) (year: int) (direction: TimetableDirection) : Async<Timetable list> =
    connectDb
    |> Sql.query (getTimetableSql (coerceLanguage lang) direction)
    |> Sql.parameters [ "Year", Sql.int year ]
    |> Sql.executeAsync mapper
    |> Async.AwaitTask