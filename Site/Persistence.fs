module Site.Persistence

open System
open Npgsql.FSharp
open StackExchange.Redis

/// Created Redis connection pool
let private redisPool : ConnectionMultiplexer option =
    try
        let pool =
            ConnectionMultiplexer.Connect(Environment.GetEnvironmentVariable("RedisConnectionString"))

        Some pool
    with ex -> raise ex
    
/// Creates redis connection
let private redisConn : IDatabase option =
    match redisPool with
    | Some r ->
        try
            let db = r.GetDatabase()
            Some db
        with ex -> raise ex
    | None -> None
    
/// Gets value from Redis by key
let getRedis (key: string) : string option =
    match redisConn with
    | Some db ->
        let value = db.StringGet(RedisKey key)
        match value.HasValue with
        | true -> value |> string |> Some
        | false -> None
    | None -> None    
    
/// Saves key-value pair to Redis
let setRedis (key: string) (value: string) (life: TimeSpan) : bool =
    match redisConn with
    | Some db -> db.StringSet(RedisKey key, RedisValue value, life)
    | None -> false    
    
/// Creates Postgres connection
let pgConn : Sql.SqlProps =
    Environment.GetEnvironmentVariable("PostgresConnectionString")
    |> Sql.connect