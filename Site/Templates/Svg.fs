namespace Site.Templates

/// SVG images to be used inline in HTML templates
module Svg =
    open Giraffe.GiraffeViewEngine

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

    let ruleEvenOdd = "evenodd"

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
              attr "xmlns" "http://www.w3.org/2000/svg"
              attr "xmlns:xlink" "http://www.w3.org/1999/xlink" ]

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


    /// TIM Group logo
    let timLogo =
        getBaseSvg
            "0 0 756 334"
            [ makePath (makeColor Color.Black) "m520 45h236v244h-236z" None
              makePath (makeColor Color.OrangeBrighter) "m638.004 218-64.004-142 64-50.9961z" (Some ruleEvenOdd)
              makePath (makeColor Color.OrangeDarker) "m638 218 64-142.0039-64-50.9961z" (Some ruleEvenOdd)
              makePath (makeColor Color.Black) "m260 45h236v244h-236z" None
              g [ attr "clip-rule" ruleEvenOdd
                  attr "fill-rule" ruleEvenOdd ] [
                  makePath (makeColor Color.OrangeBrighter) "m313.746 191.173v-146.0001l74-20z" None
                  makePath (makeColor Color.OrangeDarker) "m313.462 191.427 108.499-97.693-34.652-68.3754z" None
                  makePath (makeColor Color.OrangeDarker) "m441.399 143.408v146l-74 20z" None
                  makePath (makeColor Color.OrangeBrighter) "m441.682 143.153-108.499 97.694 34.653 68.375z" None
              ]
              makePath (makeColor Color.Black) "m0 116h236v172h-236z" None
              makePath (makeColor Color.Black) "m118 25h118v74h-118z" None
              makePath (makeColor Color.OrangeBrighter) "m0 25h118v74h-118z" None
              makePath
                  (makeColor Color.Black)
                  "m52.9961 46 .0039 32h-5c-.5523 0-1 .4477-1 1v6.0039c0 .5523.4477 1 1 1h23c.5523 0 1-.4477 1-1v-6.0076c0-.5523-.4477-1-1-1h-5v-31.9963h5.0016c.5523 0 1-.4477 1-1v-6c0-.5523-.4477-1-1-1h-23.0016c-.5523 0-1 .4477-1 1v6c0 .5523.4477 1 1 1z"
                  (Some ruleEvenOdd)
              makePath
                  (makeColor Color.White)
                  "m158 59.1159c0-10.5713 4.382-21.1157 17.737-21.1159 13.356-.0002 17.738 10.0962 17.738 21.1159 0 6.1479-1.118 12.5177-4.562 16.9575l6.265 3.993c.598.3815.62 1.2477.041 1.6586l-5.708 4.0539c-.233.165-.524.2218-.793.1274-.705-.2475-2.243-.904-4.323-2.4545-1.191-.8878-1.974-1.749-2.482-2.4561-1.792.5896-3.84.9167-6.176.9254-14.148.0525-17.737-12.2339-17.737-22.8052zm10.98.2923c0-6.912 1.67-13.8063 6.757-13.8065 5.088-.0001 6.757 6.6014 6.757 13.8065 0 7.2052-1.367 14.8767-6.757 14.911-5.389.0344-6.757-7.999-6.757-14.911z"
                  (Some ruleEvenOdd)
              makePath (makeColor Color.OrangeDarker) "m117.996 116 64.004 142-64 50.996z" (Some ruleEvenOdd)
              makePath (makeColor Color.OrangeBrighter) "m118 116-64 142.004 64 50.996z" (Some ruleEvenOdd) ]

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
