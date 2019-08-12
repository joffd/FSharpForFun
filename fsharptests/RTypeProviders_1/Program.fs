// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.

// Data file from: https://archive.ics.uci.edu/ml/datasets/Wholesale+customers
open System
open FSharp.Data
open FSharp.Data.TypeProviders
open System.Data
open RProvider
open RDotNet
open RProvider.``base``
open Deedle
open Deedle.RPlugin

open RProvider.graphics
open RProvider.grDevices
open RProvider.datasets
open RProvider.stats



module RLibEx =
    // R part
    let [<Literal>] file = __SOURCE_DIRECTORY__ + "\\Wholesale customers data.csv"
    type WholeSaleCustomer = CsvProvider<"Wholesale customers data.csv">
    
    let data = WholeSaleCustomer.Load(file)

    let frame = Frame.ReadCsv(file, separators=",")

    let rDF = R.as_data_frame(frame)
    R.assign("df", rDF)
    R.colMeans(frame)

    //RProvider.``base``.R.summary(data.Rows |> Seq.toArray)
    let df = [| for r in data.Rows -> float r.Region |]


    // basic test if RProvider works correctly
    R.mean([1;2;3;4])
    // val it : RDotNet.SymbolicExpression = [1] 2.5
    
    // testing graphics
    R.x11()
    
    // Calculate sin using the R 'sin' function
    // (converting results to 'float') and plot it
    [ for x in 0.0 .. 0.1 .. 3.14 -> 
        R.sin(x).GetValue<float>() ]
    |> R.plot
    
    // Plot the data from the standard 'Nile' data set
    R.plot(R.Nile)

[<EntryPoint>]
let main argv = 
    
    
    let a = RLibEx.rDF

    R.kmeans(x = a, centers = 5)
    R.colMeans(a)

    let exit = Console.ReadLine()
    
    printfn "%A" argv
    0 // return an integer exit code
