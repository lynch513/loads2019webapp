module Components

open Fable.React
open Fable.React.Props

open Loads2019

//-----------------------------------------------------------------------------
// Simple Components
//-----------------------------------------------------------------------------

let Line (line : Types.Line) dispatch =
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

let Section (section : Types.Section) dispatch =
    ofList [
        tr [] [ td [ ColSpan 5
                     ClassName 
                      "small text-muted font-italic font-weight-bold" ]
                   [ ofString "Секция" ]
              ]
        ofList [ for line in section.Lines 
            -> Line line dispatch ]
    ]

let Station (station : Types.Station) dispatch =
    ofList [
        ul [ ClassName "list-group list-group-flush" ]
            [ li [ ClassName "list-group-item d-flex justify-content-between align-items-center" ]
                [ h5 [ ClassName "span badge badge-secondary" ]
                    [ str station.Name ]
                  div []
                    [ 
                        button [ ClassName "btn btn-sm btn-success" ] 
                            [ str "Перерасчет" ]
                        button [ ClassName "btn btn-sm btn-secondary ml-1" ] 
                            [ str "Восстановить" ]
                        button [ ClassName "btn btn-sm btn-danger ml-1" ] 
                            [ str "Закрыть" ]
                    ]
                ]
            ]
        div [ ClassName "card-body p-0" ]
            [ table [ ClassName 
                        "table table-sm table-bordered table-striped mb-0" ] 
                [ thead []
                    [ tr [] 
                        [ th [ ClassName "small font-weight-bold" ] 
                            [ str "Линия" ]
                          th [ ClassName "span badge badge-pill badge-secondary" ] 
                            [ str "+/-" ]
                          th [ ClassName "small font-weight-bold" ] 
                            [ str "Нагрузка" ]
                          th [ ClassName "span badge badge-pill badge-secondary" ] 
                            [ str "+/-" ]
                          th [ ClassName "small font-weight-bold" ] 
                            [ str "Нагрузка" ]
                        ]
                    ]
                  tbody [] 
                    [ for section in station.Sections 
                        -> Section section dispatch ]
                ]
            ]
    ]

let StationListItem (station : Types.Station) dispatch =
    ul [ ClassName "list-group list-group-flush" ]
        [ li [ ClassName "list-group-item d-flex justify-content-between align-items-center" ]
            [ h5 [ ClassName "span badge badge-secondary" ] 
                 [ str station.Name ] 
              button [ ClassName "btn btn-sm btn-info" ]
                [ str "Открыть" ]
            ]
        ]

//-----------------------------------------------------------------------------
// List Components
//-----------------------------------------------------------------------------

let StationList (currentStation : Types.Station) (stations : Types.Station list) dispatch =
    div [ ClassName "card mt-1"]
        [ for station in stations ->
            (if currentStation.Name = station.Name
             then Station station dispatch
             else StationListItem station dispatch) ]