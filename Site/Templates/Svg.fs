module Site.Templates.Svg

open Giraffe.ViewEngine

// Register SVG-specific tags
let private svg = tag "svg"
let private g = tag "g"
let private defs = tag "defs"
let private gradient = tag "linearGradient"
let private text = tag "text"
let private path = tag "path"
let private mask = tag "mask"
let private rect = voidTag "rect"
let private stop = voidTag "stop"

let private ruleEvenOdd = "evenodd"

type Color =
    | Black
    | White
    | Dark
    | OrangeDarker
    | OrangeBrighter

/// Constructs color as hex
let private makeColor (color: Color) : string =
    match color with
    | Black -> "#000"
    | White -> "#fff"
    | Dark -> "#252627"
    | OrangeDarker -> "#f36c24"
    | OrangeBrighter -> "#f9b816"

/// Constructs the base svg tag
let private getBaseSvg (viewBox: string) =
    svg [ attr "viewBox" viewBox
          attr "xmlns" "http://www.w3.org/2000/svg" ]

/// Constructs new path tag
let private makePath (fill: string) (d: string) (rule: string option) =
    let basePath = [ attr "d" d; attr "fill" fill ]

    let rules =
        match rule with
        | Some r ->
            basePath
            @ [ attr "clip-rule" r
                attr "fill-rule" r ]
        | None -> basePath

    path rules []

/// Company icon
let companyIcon =
    getBaseSvg
        "0 0 25.7 25.7"
        [ makePath
              (makeColor Color.Dark)
              "m8.11 9.11a2.08 2.08 0 0 0 1.19-.37l3.38 2a1.61 1.61 0 0 0 0 .17 2.1 2.1 0 1 0 4.05-.74l3.91-4.17 2.3 2 1.4-7.45-7.14 2.45 2 1.76-3.83 4.16a2.06 2.06 0 0 0 -.6-.09 2.09 2.09 0 0 0 -1.14.34l-3.43-2.05s0-.07 0-.11a2.1 2.1 0 1 0 -4.11.58l-2.79 2.56v2.52l4.04-3.67a2.09 2.09 0 0 0 .77.11zm0 7.52a2.09 2.09 0 0 0 1.48-.63l6 2.25a2.1 2.1 0 1 0 4-1l3.53-4 1.36 1.05.66-5-4.71 1.92 1.22.94-3.41 3.84a2.07 2.07 0 0 0 -2 .5l-6.08-2.27a2.1 2.1 0 0 0 -4.16.3v.17l-2.7 1.68v2.18l3.7-2.27a2.09 2.09 0 0 0 1.11.34zm-6.26 7.21h-.06v-23.84h-1.79v25.7h25.7v-1.86z"
              None ]

/// VINK Simulation icon
let vinkIcon =
    getBaseSvg
        "0 0 26.17 26.3"
        [ makePath
              (makeColor Color.Dark)
              "m22.19 3.8v10.9l-5.65-3v3.66l-7.95-3.68v4l-8.59-4v14.62h26.17v-22.5zm-1.38-2.29a3.46 3.46 0 0 1 2 .73 4.84 4.84 0 0 1 .6.52l.15.17.15-.12 1-.8a5.88 5.88 0 0 0 -1.09-1 5 5 0 0 0 -2.94-1 5.83 5.83 0 0 0 -3.98 1.99l1.06 1a4.33 4.33 0 0 1 3.05-1.49zm-11.25 3.66a5 5 0 0 0 2.94 1 5.85 5.85 0 0 0 4.1-2l-1.06-1.02a4.34 4.34 0 0 1 -3 1.54 3.47 3.47 0 0 1 -2-.73 4.64 4.64 0 0 1 -.6-.52l-.15-.16-1.2.92a5.81 5.81 0 0 0 .97.97zm.11-1.95-.17.13zm-5.13-1.71a3.47 3.47 0 0 1 2 .73 4.66 4.66 0 0 1 .6.52l.15.17 1.2-.92a5.81 5.81 0 0 0 -1.01-1.01 5 5 0 0 0 -2.94-1 5.83 5.83 0 0 0 -4.11 2l1.07 1a4.33 4.33 0 0 1 3.04-1.49zm3 1.33-.17.16z"
              None ]

/// Negotiations Simulation icon
let negotiationsIcon =
    getBaseSvg
        "0 0 16 11.92"
        [ makePath
            (makeColor Color.Dark)
            "m15.8 8.94a2 2 0 0 0 -1.28-1.16 11.49 11.49 0 0 1 -1.22-.55v-.96a1.78 1.78 0 0 0 .48-1.17c.23.07.47-.34.49-.56s0-.79-.31-.73a6 6 0 0 0 .08-1 1.89 1.89 0 0 0 -2-1.56 1.89 1.89 0 0 0 -2 1.56 5.84 5.84 0 0 0 .08 1c-.28-.06-.33.52-.31.73s.26.63.49.56a1.78 1.78 0 0 0 .48 1.17l-.78.65c.46.25 1.66.12 1.91.51.15.23.54.84.43 4.49h3.66a12.29 12.29 0 0 0 -.2-2.98z"
            None
          makePath
              (makeColor Color.Dark)
              "m10.78 8.6c-.33-.51-1.44-.83-2.54-1.29a12.68 12.68 0 0 1 -1.36-.62v-1.05a2 2 0 0 0 .54-1.31c.26.07.53-.38.54-.62s0-.88-.35-.81a6.65 6.65 0 0 0 .09-1.16 2.1 2.1 0 0 0 -2.2-1.74 2.1 2.1 0 0 0 -2.2 1.74 6.5 6.5 0 0 0 .09 1.15c-.31-.06-.39.58-.39.81s.28.7.54.62a2 2 0 0 0 .54 1.31v1.06a12.76 12.76 0 0 1 -1.36.62c-1.06.45-2.17.77-2.5 1.29a13.71 13.71 0 0 0 -.22 3.32h11a13.73 13.73 0 0 0 -.22-3.32z"
              None ]
