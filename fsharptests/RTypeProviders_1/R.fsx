

#r "../packages/\Deedle.2.0.4/lib/net45/Deedle.dll"
#r "../packages/Deedle.RPlugin.2.0.4/lib/net451/Deedle.RProvider.Plugin.dll"
#r "../packages/FSharp.Data.3.1.1/lib/net45/FSharp.Data.dll"
#r "../packages/R.NET.1.7.0/lib/net40/RDotNet.dll"
#r "../packages/R.NET.FSharp.1.7.0/lib/net40/RDotNet.FSharp.dll"
#r "../packages/RProvider.1.1.22/lib/net40/RProvider.dll"
#r "../packages/DynamicInterop.0.8.1/lib/netstandard1.2/DynamicInterop.dll"
#r "../packages/RProvider.1.1.22/lib/net40/RProvider.dll"
#r "../packages/RProvider.1.1.22/lib/net40/RProvider.Runtime.dll"

#load "RLib.fs"

open System
open FSharp.Data
open DynamicInterop
open System.Data
open RProvider
open RDotNet
open RProvider.``base``
open Deedle
open Deedle.RPlugin
open RProvider
open RProvider.datasets
open RProvider.stats
open RLib


// To print in FSI the results from the operations
do fsi.AddPrinter(fun (synexpr:RDotNet.SymbolicExpression) -> synexpr.Print())

let [<Literal>] file = __SOURCE_DIRECTORY__ + "\\Wholesale customers data.csv"
type WholeSaleCustomer = CsvProvider<"Wholesale customers data.csv">

let data = WholeSaleCustomer.Load(file)

let frame = Frame.ReadCsv(file, separators=",")
frame.RowCount
R.as_data_frame(frame)
R.colMeans(frame)
frame |> Stats.mean |> fun x -> x.Observations

let airQuality = __SOURCE_DIRECTORY__ + "\\airquality.csv"
let air = Frame.ReadCsv(airQuality, separators=";")
R.as_data_frame(air)
R.colMeans(air)

let (?) (x:SymbolicExpression) param = 
  let paramDict = 
    x.GetAttribute("names").AsList() 
    |> Seq.mapi (fun i n -> n.GetValue<string>(), i)  |> dict
  x.AsList().[paramDict.[param]]
  
let res = R.kmeans(x = frame, centers = 5, nstart=10)
   
    
R.eval(R.kmeans(x = frame, centers = 5, nstart=10))          

let aa = res?centers
(res?size).GetValue<int[]>() |> Array.sort
let bb = aa.AsNumericMatrix()

(res?cluster).GetValue<int[]>()
(res?centers).AsNumericMatrix().ToArray()
res.GetAttribute("centers").AsList()


