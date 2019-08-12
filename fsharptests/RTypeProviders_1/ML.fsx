

#r "../packages/Microsoft.ML.1.3.1/lib/netstandard2.0/Microsoft.ML.Core.dll"
#r "../packages/Microsoft.ML.1.3.1/lib/netstandard2.0/Microsoft.ML.Data.dll"
#r "../packages/Microsoft.ML.DataView.1.3.1/lib/netstandard2.0/Microsoft.ML.DataView.dll"
#r "../packages/FSharp.Data.3.1.1/lib/net45/FSharp.Data.dll"
#r "../packages/\Deedle.2.0.4/lib/net45/Deedle.dll"

open System
open System.Data
open FSharp.Data
open System.IO
open Deedle
open Microsoft.ML
open Microsoft.ML.Data
open Microsoft.ML.Trainers
open Microsoft.ML.Runtime



let mlContext = MLContext(seed = Nullable 1)

let [<Literal>] file = __SOURCE_DIRECTORY__ + "\\Wholesale customers data.csv"
type WholeSaleCustomer = CsvProvider<"Wholesale customers data.csv">

let data = WholeSaleCustomer.Load(file)
let frame = Frame.ReadCsv(file, separators=",")

let fullData1 = 
    mlContext.Data.LoadFromEnumerable
    

// STEP 1: Common data loading configuration
let fullData2 = 
    
    mlContext.Data.LoadFromTextFile(file,
        hasHeader = true,
        separatorChar = '\t',
        columns =
            [|
                TextLoader.Column("Label", DataKind.Single, 0)
                TextLoader.Column("SepalLength", DataKind.Single, 1)
                TextLoader.Column("SepalWidth", DataKind.Single, 2)
                TextLoader.Column("PetalLength", DataKind.Single, 3)
                TextLoader.Column("PetalWidth", DataKind.Single, 4)
            |]
    )


//Split dataset in two parts: TrainingDataset (80%) and TestDataset (20%)
let trainingDataView, testingDataView = 
    let split = mlContext.Data.TrainTestSplit(fullData2, testFraction = 0.2)
    split.TrainSet, split.TestSet