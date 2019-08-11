module App

open Elmish
open Elmish.React

open Fable.React



type Model = { 
    ComponentsModel : Components.Model 
    }

type Msg =
    | ComponentsMsg of Components.Msg

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
    div [] [
        Layout.HeaderSection dispatch
        Layout.MainSection [
            // Layout.ObjectCounter "Записей в базе данных" model.StationList.Length
            Components.StationList model.ComponentsModel (ComponentsMsg >> dispatch)
        ] 
        Layout.FooterSection [
            str "2019"
        ]
        // button [ OnClick (fun _ -> dispatch <| SetCurrentStation testStation1)]
        //     [ str "Switch station"]
    ]

Program.mkProgram init update view 
|> Program.withReactSynchronous "app"
|> Program.run