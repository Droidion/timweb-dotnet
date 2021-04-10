namespace Site.Database

open System.Data

/// Data provider for working with years
module YearsProvider =
    open Helpers

    /// Maps Dapper result to F# record
    let private mapper (reader: IDataReader) : int list =
        [ while reader.Read() do
              yield reader.GetInt32(reader.GetOrdinal "year") ]

    /// Returns the list of years
    let list : Async<int seq> =
        let sql = SqlRequests.years
        query<int> sql None mapper
