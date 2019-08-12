module ParallelArray

    open System
    open MathNet.Numerics.LinearAlgebra
    open MathNet.Numerics.Distributions
    

    let sigmoid x = 1. / (1. + exp(-x))
    let rnd = System.Random()

    let print s i = printfn "%s: %i" s i

    let array1 i = 
        let rnd = System.Random()
        Array.init i (fun _ -> rnd.NextDouble())
 
    let vectorFromArray i = 
        let rnd = System.Random()
        CreateVector.DenseOfArray (Array.init i (fun _ -> rnd.NextDouble())) //vector (Seq.init i (fun _ -> rnd.NextDouble()))
    let vectorCreate i =           
        let d = new ContinuousUniform(0.1,1.)
        CreateVector.Random<float>(i, d)


    let sigmoidArraySimple a =
        a |> Array.map sigmoid

    let sigmoidArrayParallel a =
        a |> Array.Parallel.map sigmoid



