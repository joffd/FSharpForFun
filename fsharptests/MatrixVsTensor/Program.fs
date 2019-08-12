// Learn more about F# at http://fsharp.org

open System
open System.IO
open BenchmarkDotNet.Running
open BenchmarkDotNet.Attributes
open MatrixVsTensor
open Tensor
open MathNet.Numerics.LinearAlgebra


module ExecBench =
    [<MemoryDiagnoser>]
    type ExecBench() =
        [<Benchmark>]
        member __.Matrix() =
            m1sig()
                   
        [<Benchmark>]
        member __.Tensor() =
            t1sig()

        [<Benchmark>]
        member __.TensorParallel() =
            t1sigmulti()
   

[<EntryPoint>]
let main _ =
    
    Console.WriteLine("Starting!")

    let filestr = "F:\\exitconsole\\" + "matrix_vs_tensor_" 
                    + DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".txt"
    let file = new FileStream(filestr, FileMode.OpenOrCreate, FileAccess.Write)
    let w = new StreamWriter(file)
    let console = Console.Out

    Console.SetOut(w)
    
    printfn "Testing speed Matrix vs Tensors im F#!"
   
    BenchmarkRunner.Run<ExecBench.ExecBench>()
    |> printfn "%A"

    Console.WriteLine()


    t1sig() |> HostTensor.toArray |> Array.iteri (fun i s -> if (i < 10) then Console.WriteLine(s)) 
    m1sig() |> Vector.toArray |> Array.iteri (fun i s -> if (i < 10) then Console.WriteLine(s)) 


    w.Close()

    Console.SetOut(console)

    Console.WriteLine("Done!")

    Console.ReadLine() |> ignore

    0 // return an integer exit code
