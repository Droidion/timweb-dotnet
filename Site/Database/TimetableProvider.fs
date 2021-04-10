namespace Site.Database

open System.Data
open Models

/// Data provider for working with timetable
module TimetableProvider =
    open Helpers

    /// Maps Dapper result to F# record
    let private mapper (reader: IDataReader) : Timetable list =
        [ while reader.Read() do
              yield
                  { dateStart = reader.GetDateTime(reader.GetOrdinal "datestart")
                    dateFinish =
                        if reader.IsDBNull(reader.GetOrdinal "datefinish") then
                            None
                        else
                            reader.GetDateTime(reader.GetOrdinal "datefinish")
                            |> Some
                    seminar = reader.GetString(reader.GetOrdinal "seminar")
                    city = reader.GetString(reader.GetOrdinal "city")
                    clients = reader.GetString(reader.GetOrdinal "clients") } ]

    /// Returns the list of events
    let list (lang: string) (year: int) (direction: TimetableDirection) : Async<Timetable seq> =
        let lang = chooseLange lang
        let sql = SqlRequests.getTimetable lang direction
        let data = dict [ "Year", box year ]
        query<Timetable> sql (Some data) mapper
