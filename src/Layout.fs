module Layout

open Fable.React
open Fable.React.Props

type HTMLAttr = 
     | [<CompiledName("data-target")>] DataTarget of string
     | [<CompiledName("aria-label")>] AriaLabel of string
     interface IHTMLProp

let HeaderSection dispatch =
    header [] [ 
        nav [ ClassName "navbar navbar-expand-md navbar-dark fixed-top bg-dark"] [
            a [ ClassName "navbar-brand"; Href "#" ] [ 
                str "Нагрузки 2019" 
            ]
            button [ 
                ClassName "navbar-toggler" 
                Type "button"
                DataToggle "collapse"
                DataTarget "#navbarCollapse"
                AriaControls "navbarCollapse"
                AriaExpanded false
                AriaLabel "Toggle navigation"
            ] [ span [ ClassName "navbar-toggler-icon"] [] ]
            div [ ClassName "collapse navbar-collapse" 
                  Id "navbarCollapse" ] [ 
                ul [ ClassName "navbar-nav mr-auto"] [ 
                    li [ ClassName "nav-item active" ] [ 
                        a [ ClassName "nav-link"
                            Href "#" ] [ 
                            str "Главная "
                            span [ ClassName "sr-only" ] [ 
                                str "(current)" 
                            ]
                        ]
                    ]
                ]
            ]
        ] 
    ]

let MainSection (children : ReactElement list) =
    main [ ClassName "container mt-5"; Role "main" ] children

let FooterSection (children : ReactElement list) =
    footer [ ClassName "footer" ] [
        div [ ClassName "container" ] [
            span [ ClassName "text-muted" ] children
        ]
    ]

let ObjectCounter caption objectCount =
    div [ ClassName "card-body d-flex justify-content-between align-items-center" ] [
        str caption
        span [ ClassName "badge badge-primary badge-pill" ] [
            ofInt objectCount
        ]
    ]