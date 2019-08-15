module App

open Elmish
open Elmish.React

open Fable.React
open Fable.React.Props
open Loads2019

type Model = { 
    ComponentsModel : Components.Model 
    }

type Msg =
    | ComponentsMsg of Components.Msg
    | FetchMsg
    | OnFetchSuccess of Result<Types.Station, string>
    | OnFetchError of exn

let init () : Model * Cmd<Msg> =
    let res, cmd = Components.init()
    { ComponentsModel = res }, 
        Cmd.map ComponentsMsg cmd

let update (msg : Msg) (model : Model) : Model * Cmd<Msg> =
    match msg with
    | ComponentsMsg msg' ->
        let res, cmd = Components.update msg' model.ComponentsModel
        { model with ComponentsModel = res },
            Cmd.map ComponentsMsg cmd
    | FetchMsg ->
        model, Cmd.OfPromise.either Helpers.httpGet<Types.Station> "data.json" OnFetchSuccess OnFetchError
    | OnFetchSuccess mStation ->
       match mStation with
       | Ok station ->
           printfn "%A" station
           model, Cmd.none
       | Error e ->
           printfn "%s" e
           model, Cmd.none
    | OnFetchError error ->
        Browser.Dom.console.error error
        model, Cmd.none



let view (model : Model) (dispatch : Msg -> unit) =
    ofList [
        Layout.HeaderSection dispatch
        Layout.MainSection [
            // Layout.ObjectCounter "Записей в базе данных" model.StationList.Length
            Components.StationList model.ComponentsModel (ComponentsMsg >> dispatch)
        ] 
        button [ OnClick (fun _ -> dispatch FetchMsg)] [ str "Fetch" ]
        Layout.FooterSection [
            str "2019"
        ]
        // button [ OnClick (fun _ -> dispatch <| SetCurrentStation testStation1)]
        //     [ str "Switch station"]
    ]

Program.mkProgram init update view 
|> Program.withReactSynchronous "app"
|> Program.run