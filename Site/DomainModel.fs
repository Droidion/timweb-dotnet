namespace Site.DomainModel

open System

type Timetable = {
    dateStart: DateTime
    dateFinish: DateTime option
    seminar: string
    city: string
    clients: string
}

type EventCounter = {
    name: string
    days: int
    games: int
}

type GameRating = {
    client: string
    date: DateTime
    city: string
    country: string
    teamsCounter: int
    rating: float
}

type Year = {
    year: int
}

type TimetableDirection = | Past = '<' | Future = '>'

type Client = {
    name: string
    logo: string
    seminarDays: int
    vinkDays: int
    clients: string
}