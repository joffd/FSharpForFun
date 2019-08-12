module Collections

    open Deedle
    open Microsoft.ML.TimeSeries
    open System
    open MathNet.Numerics.LinearAlgebra
    open MathNet.Numerics.Distributions

    let sigmoid x = 1. / (1. + exp(-x))
    let rnd = System.Random()

    let print s i = printfn "%s: %i" s i

    let array1 i = 
        let rnd = System.Random()
        Array.init i (fun _ -> rnd.NextDouble())
    let list1 i= 
        let rnd = System.Random()
        List.init i (fun _ -> rnd.NextDouble())
    let seq1 i = 
        let rnd = System.Random()
        Seq.init i (fun _ -> rnd.NextDouble())
    let series1 i = 
        let rnd = System.Random()
        Series.ofValues (Seq.init i (fun _ -> rnd.NextDouble()))
    let vectorFromArray i = 
        let rnd = System.Random()
        CreateVector.DenseOfArray (Array.init i (fun _ -> rnd.NextDouble())) //vector (Seq.init i (fun _ -> rnd.NextDouble()))
    let vectorCreate i =           
        let d = new ContinuousUniform(0.1,1.)
        CreateVector.Random<float>(i, d)


    let sigmoidArray i =
        i |> array1
          |> Array.map sigmoid
          |> Array.length

    let sigmoidList i =
        i |> list1
          |> List.map sigmoid
          |> List.length

    let sigmoidSeq i =
        i |> seq1
          |> Seq.map sigmoid
          |> Seq.length

    let sigmoidSeries i =
        i |> series1
          |> Series.mapValues sigmoid
          |> Series.countValues

    let sigmoidVecorInitFromArray i =
        i |> vectorFromArray
          |> Vector.map sigmoid
          |> Vector.length

    let sigmoidVecorCreateVector i =
        i |> vectorCreate
          |> Vector.map sigmoid
          |> Vector.length


