namespace Site.Database

open System.Threading.Tasks
open Models


/// Data provider for working with years
module YearsProvider =
    open Helpers
    
    /// Maps Dapper result to F# record
    let private mapper (read : RowReader) =
        {
            year = read.int "year"
        }

    /// Returns the list of years
    let list : Async<Year list> =
        let sql = SqlRequests.years
        query<Year> sql [] mapper
