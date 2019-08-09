module App

open Fable.React
open Fable.React.Props

open Data
open Loads2019.Types


type Model = { 
    Count : int 
    CurrentStation : Station
    }

type Msg =
    | Increment
    | Decrement
    | IncrementBy of int
    | Reset 
    | SetCurrentStation of Station

let init () : Model =
    { Count = 42 
      CurrentStation = testStationDefault }

let update msg model : Model =
    match msg with
    | Increment ->
        { model with Count = model.Count + 1 }
    | Decrement ->
        { model with Count = model.Count - 1 }
    | IncrementBy step ->
        { model with Count = model.Count + step }
    | Reset ->
        init()
    | SetCurrentStation station ->
        { model with CurrentStation = station }

let renderLine (line : Line) dispatch =
    tr [] [ 
        td [] [ str line.Name ]
        ofList [ 
            for sample in line.Samples -> 
                ofList [
                        td [] [ (if sample > 0 then str "+" else str "-") ]
                        td [] [ ofInt sample ]
                       ] 
               ]
          ]


let renderSection (section : Section) dispatch =
    ofList [
        tr [] [ td [ ColSpan 5
                     ClassName 
                      "small text-muted font-italic font-weight-bold" ]
                   [ ofString "Секция" ]
              ]
        ofList [ for line in section.Lines 
            -> renderLine line dispatch ]
    ]

let renderStation model dispatch =
    div [ ClassName "card-body p-0" ]
        [ table [ ClassName 
                    "table table-sm table-bordered table-striped mb-0" ] 
            [ thead []
                [ tr [] 
                    [ th [ ClassName "small font-weight-bold" ] 
                        [ str "Линия" ]
                      th [ ClassName "span bange-pill bange-secondary" ] 
                        [ str "+/-" ]
                      th [ ClassName "small font-weight-bold" ] 
                        [ str "Нагрузка" ]
                      th [ ClassName "span bange-pill bange-secondary" ] 
                        [ str "+/-" ]
                      th [ ClassName "small font-weight-bold" ] 
                        [ str "Нагрузка" ]
                    ]
                ]
              tbody [] 
                [ for section in model.CurrentStation.Sections 
                    -> renderSection section dispatch ]
            ]
        ]

let view model dispatch =
    div []
        [
            button [ OnClick (fun _ -> dispatch Decrement) ] 
                [ str "-" ]
            h1 [] 
                [ ofInt model.Count ]
            button [ OnClick (fun _ -> dispatch Increment) ] 
                [ str "+" ]
            button [ OnClick (fun _ -> dispatch <| IncrementBy 5) ] 
                [ str "+5" ]
            button [ OnClick (fun _ -> dispatch Reset) ] 
                [ str "Reset" ]
            button [ OnClick (fun _ -> dispatch <| SetCurrentStation testStation1)]
                [ str "Switch station"]
            renderStation model dispatch
        ]

open Elmish
open Elmish.React

Program.mkSimple init update view 
|> Program.withReactSynchronous "app"
|> Program.run