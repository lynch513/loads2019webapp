module Components

open Fable.React
open Fable.React.Props

open Elmish

open Data
open Loads2019

//-----------------------------------------------------------------------------
// Models, Msg, init and update
//-----------------------------------------------------------------------------

type Model = { 
    StationList : Types.Station list
    CurrentStation : Types.Station option
    }

type Msg =
    | ResetCurrentStation
    | SetCurrentStation of Types.Station

let init () : Model * Cmd<Msg> =
    { StationList = testStations
      CurrentStation = None }, Cmd.none

let update (msg : Msg) (model : Model) : Model * Cmd<Msg> =
    match msg with
    | ResetCurrentStation ->
        { model with CurrentStation = None }, Cmd.none
    | SetCurrentStation station ->
        { model with CurrentStation = Some station }, Cmd.none

//-----------------------------------------------------------------------------
// Simple Components
//-----------------------------------------------------------------------------

let Line (line : Types.Line) =
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

let Section (section : Types.Section) =
    ofList [
        tr [] [ td [ ColSpan 5
                     ClassName 
                      "small text-muted font-italic font-weight-bold" ]
                   [ ofString "Секция" ]
              ]
        ofList [ for line in section.Lines 
            -> Line line ]
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
                        button [ ClassName "btn btn-sm btn-danger ml-1"
                                 OnClick (fun _ -> dispatch <| ResetCurrentStation) ] 
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
                        -> Section section ]
                ]
            ]
    ]

let ObjectCounter caption objectCount =
    div [ ClassName "card-body d-flex justify-content-between align-items-center" ] [
        str caption
        span [ ClassName "badge badge-primary badge-pill" ] [
            ofInt objectCount
        ]

    ]

//-----------------------------------------------------------------------------
// List Components
//-----------------------------------------------------------------------------

let StationListItem (station : Types.Station) dispatch =
    ul [ ClassName "list-group list-group-flush" ]
        [ li [ ClassName "list-group-item d-flex justify-content-between align-items-center" ]
            [ h5 [ ClassName "span badge badge-secondary" ] 
                 [ str station.Name ] 
              button [ ClassName "btn btn-sm btn-info"
                       OnClick (fun _ -> dispatch <| SetCurrentStation station) ]
                [ str "Открыть" ]
            ]
        ]

let StationList (model : Model) dispatch =
    ofList [
        ObjectCounter "Записей в базе данных" model.StationList.Length
        div [ ClassName "card mt-1"]
            [ for station in model.StationList ->
                match model.CurrentStation with
                | Some currentStation' when currentStation'.Name = station.Name -> 
                    Station station dispatch
                | _ ->
                    StationListItem station dispatch
            ]
    ]