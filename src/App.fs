module App

open Elmish
open Elmish.React

open Fable.React

type Model = { 
    ComponentsModel : Components.Model 
    }

type Msg =
    | ComponentsMsg of Components.Msg

let fetchData _ =
    let sub dispatch =
        dispatch <| ComponentsMsg Components.Msg.FetchData |> ignore
    Cmd.ofSub sub

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

let view (model : Model) (dispatch : Msg -> unit) =
    ofList [
        Layout.HeaderSection dispatch
        Layout.MainSection [
            Components.StationList model.ComponentsModel (ComponentsMsg >> dispatch)
        ] 
        Layout.FooterSection [
            str "2019"
        ]
    ]

Program.mkProgram init update view 
|> Program.withReactSynchronous "app"
|> Program.withSubscription fetchData
|> Program.run