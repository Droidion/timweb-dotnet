namespace Models

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

type Brand = {
    id: int
    logo: string
    name_in: string
    name_ru: string
}

type Year = {
    year: int
}

type TimetableDirection = | Back = '<' | Future = '>'