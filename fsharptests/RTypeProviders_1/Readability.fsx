
open System

let calcArea1param (f: decimal-> decimal) (input: string) =
    match input with
    | _ when fst (Decimal.TryParse(input)) -> f (decimal input) |> Some
    | _                                    -> None



let (|ParsedDecimal|NotDecimal|) (input: string) =
    match (Decimal.TryParse(input) |> fst) with
    | true ->  ParsedDecimal (decimal input)
    | false -> NotDecimal



let calcArea1paramV2b (f: decimal -> decimal) input =
    match input with
    | ParsedDecimal i -> f i |> Some
    | NotDecimal      -> None

type DecimalParsing =
    | ParsedDecimal of decimal
    | NotDecimal

let parseDecimal input =
    match (Decimal.TryParse(input) |> fst) with
    | true ->  ParsedDecimal (decimal input)
    | false -> NotDecimal


let calcArea1paramV2a (f: decimal -> decimal) input =
    match input with
    | ParsedDecimal i -> f i |> Some
    | NotDecimal      -> None

let completeSol2a (f: decimal -> decimal) input =
    input
    |> parseDecimal
    |> (calcArea1paramV2a f)

let completeSol2ab (f: decimal -> decimal) =
    parseDecimal >> (calcArea1paramV2a f)





    

let circle x = x * x * (decimal Math.PI)


calcArea1paramV2b circle "3"

completeSol2a circle "3"
(completeSol2ab circle) "3"