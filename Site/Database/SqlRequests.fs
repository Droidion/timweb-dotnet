namespace Site.Database

open Models

/// SQL requests
module SqlRequests =

    /// Returns SQL request for getting timetable
    let getTimetable (lang: string) (direction: TimetableDirection) =
        let yearClause =
            if direction = TimetableDirection.Back then
                "AND EXTRACT(YEAR FROM e.date_start) = @Year"
            else
                ""

        $"SELECT e.date_start                 datestart,
                 e.date_finish                datefinish,
                 s.name_{lang}                seminar,
                 c.name_{lang}                city,
                 STRING_AGG(c2.name_{lang}, ', ') clients
          FROM events e
                 INNER JOIN cities c ON c.id = e.city_id
                 INNER JOIN seminars s ON e.seminar_id = s.id
                 INNER JOIN events_clients ec ON e.id = ec.event_id
                 INNER JOIN clients c2 ON ec.client_id = c2.id
          WHERE e.date_start {unbox<char> direction} now() {yearClause}
          GROUP BY e.date_start, e.date_finish, s.name_{lang}, c.name_{lang}
          ORDER BY date_start desc;"

    /// SQL request for getting list of years
    let years = "SELECT EXTRACT(YEAR FROM date_start)::INTEGER AS year
                 FROM events
                 GROUP BY year
                 ORDER BY year DESC;"
