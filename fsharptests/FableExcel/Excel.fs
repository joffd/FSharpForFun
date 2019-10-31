


module Excel

    open System
    open Elmish
    open Elmish.React
    open Fable.Helpers.React
    open Fable.Helpers.React.Props
    open Fable.Core.JsInterop
    open Fable.Import

    type Position = char * int

    type Expr =
        | Number of int
        | Reference of Position
        | Binary of Expr * char * Expr

    type Sheet = Map<Position, string>

    type Event =
        | UpdateValue of Position * string
        | StartEdit of Position

    type State =
        { Rows : int list 
          Cols : char list
          Active : Position option
          Cells : Sheet }

    let update msg state =
        match msg with
        | StartEdit(pos) ->
            { state with Active = Some pos }, Cmd.none



