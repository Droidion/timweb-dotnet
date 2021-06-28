namespace Site.Database

open System.Data
open System.Threading.Tasks
open Models

/// Data provider for working with timetable
module TimetableProvider =
    open Helpers

    /// Maps Dapper result to F# record
    let private mapper (read : RowReader) =
        { dateStart = read.dateTime "datestart"
          dateFinish = read.dateTimeOrNone "datefinish"
          seminar = read.text "seminar"
          city = read.text "city"
          clients = read.text "clients" }

    /// Returns the list of events
    let list (lang: string) (year: int) (direction: TimetableDirection) : Async<Timetable list> =
        let lang = chooseLange lang
        let sql = SqlRequests.getTimetable lang direction
        let data = [ "Year", Sql.int year ]
        query<Timetable> sql data mapper
