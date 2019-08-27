module Components

open Fable.React
open Fable.React.Props

open Elmish
open Browser.Dom

open Loads2019

type HTMLAttr = 
     | [<CompiledName("data-dismiss")>] DataDismiss of string
     | [<CompiledName("aria-label")>] AriaLabel of string
     | [<CompiledName("aria-hidden")>] AriaHidden of bool
     | [<CompiledName("aria-labelledby")>] AriaLabelledBy of string
     interface IHTMLProp

//-----------------------------------------------------------------------------
// Models, Msg, init and update
//-----------------------------------------------------------------------------

type StationListState =
    | EmptyStationList 
    | OkStationList of Types.Station list
    | ErrorStationList of string

type Model = { 
    CurrentStationList : StationListState
    CurrentStation : Types.Station option
    }

type Msg =
    | FetchData
    | OnFetchSuccess of Result<Types.Station list, string>
    | OnFetchError of exn
    | SetCurrentStation of Types.Station option
    | RecalcCurrentStation 
    | RestoreCurrentStation 
    | CloseCurrentStation

let init () : Model * Cmd<Msg> =
    { CurrentStationList = EmptyStationList
      CurrentStation = None }, Cmd.none

let update (msg : Msg) (model : Model) : Model * Cmd<Msg> =
    let tryFindStation (station' : Types.Station) : Types.Station option = 
        match model.CurrentStationList with
        | OkStationList stationList ->
            List.tryFind (fun (s : Types.Station) -> s.Name = station'.Name) stationList
        | _ -> None
    let restoreCurrentStation  =
        Option.bind tryFindStation 
    let recalcCurrentStation = 
        tryFindStation >> Option.map Fake.Station.fakeSamples |> Option.bind
    match msg with
    | FetchData ->
        model, Cmd.OfPromise.either Helpers.httpGet<Types.Station list> "stations.json" OnFetchSuccess OnFetchError
    | OnFetchSuccess mStationList ->
        match mStationList with
        | Ok stationList ->
            { model with CurrentStation = None; CurrentStationList = OkStationList stationList }, Cmd.none
        | Error e ->
            console.error e
            { model with CurrentStationList = ErrorStationList e }, Cmd.none
    | OnFetchError error ->
        console.error error
        { model with CurrentStationList = ErrorStationList error.Message }, Cmd.none
    | SetCurrentStation mStation ->
        { model with CurrentStation = mStation }, Cmd.none
    | RecalcCurrentStation ->
        model, Cmd.OfFunc.perform recalcCurrentStation model.CurrentStation SetCurrentStation
    | RestoreCurrentStation ->
        model, Cmd.OfFunc.perform restoreCurrentStation model.CurrentStation SetCurrentStation
    | CloseCurrentStation ->
        { model with CurrentStation = None }, Cmd.none

//-----------------------------------------------------------------------------
// Simple Components
//-----------------------------------------------------------------------------

let Line (line : Types.Line) =
    tr [] [ 
        td [] [ str line.Name ]
        ofList [ for sample in line.Samples -> 
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
                        button [ ClassName "btn btn-sm btn-success"
                                 OnClick (fun _ -> dispatch <| RecalcCurrentStation) ] 
                            [ str "Перерасчет" ]
                        button [ ClassName "btn btn-sm btn-secondary ml-1"
                                 OnClick (fun _ -> dispatch <| RestoreCurrentStation) ] 
                            [ str "Восстановить" ]
                        button [ ClassName "btn btn-sm btn-danger ml-1"
                                 OnClick (fun _ -> dispatch CloseCurrentStation) ] 
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
                       OnClick (fun _ -> 
                                    station 
                                    |> Some 
                                    |> SetCurrentStation 
                                    |> dispatch) ]
                [ str "Открыть" ]
            ]
        ]

let StationList (model : Model) dispatch =
    let stationsCount = 
        match model.CurrentStationList with
        | OkStationList stationList -> 
            stationList.Length
        | _ -> 0
    let stationList =
        match model.CurrentStationList with
            | OkStationList stationList ->
                ofList [ for station in stationList ->
                             match model.CurrentStation with
                             | Some currentStation' when currentStation'.Name = station.Name -> 
                                 Station currentStation' dispatch
                             | _ ->
                                 StationListItem station dispatch ]
              | EmptyStationList ->
                  div [ ClassName "card-body" ] [
                      p [ ClassName "card-text" ] [
                          str "Загрузка данных ..." 
                      ]
                  ]
              | ErrorStationList e ->
                  div [ ClassName "card-body" ] [
                      p [ ClassName "card-text text-warning" ] [
                          span [ ClassName "badge badge-danger" ] [
                              str "Error"
                          ]
                          str <| sprintf " %s" e  
                      ]
                  ]
    ofList [
        ObjectCounter "Записей в базе данных" stationsCount
        div [ ClassName "card mt-1"] [ stationList ]
    ]