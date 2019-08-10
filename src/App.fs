module App

open Fable.React
open Fable.React.Props

open Data
open Layout
open Components
open Loads2019.Types


type Model = { 
    StationList : Station list
    CurrentStation : Station
    }

type Msg =
    | SetCurrentStation of Station

let init () : Model =
    { StationList = testStations
      CurrentStation = testStationDefault }

let update msg model : Model =
    match msg with
    | SetCurrentStation station ->
        { model with CurrentStation = station }


let view model dispatch =
    div [] [
        HeaderSection dispatch
        MainSection [
            ObjectCounter "Записей в базе данных" model.StationList.Length
            StationList model.CurrentStation model.StationList dispatch
        ] 
        FooterSection [
            str "2019"
        ]
        // button [ OnClick (fun _ -> dispatch <| SetCurrentStation testStation1)]
        //     [ str "Switch station"]
    ]

open Elmish
open Elmish.React

Program.mkSimple init update view 
|> Program.withReactSynchronous "app"
|> Program.run