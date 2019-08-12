// Learn more about F# at http://fsharp.org

open System
open System.IO
open BenchmarkDotNet.Running
open BenchmarkDotNet.Attributes
open Collections


module ExecBench =
    
    let i = 1000000

    [<MemoryDiagnoser>]
    
    type ExecBench() =
        [<Benchmark>]
        member __.Array() =
            sigmoidArray i
                   
        [<Benchmark>]
        member __.List() =
           sigmoidList i

        [<Benchmark>]
        member __.Seq() =
            sigmoidSeq i

        [<Benchmark>]
        member __.Series() =
            sigmoidSeries i

        [<Benchmark>]
        member __.VecorInitFromArray() =
            sigmoidVecorInitFromArray i

        [<Benchmark>]
        member __.VectorCreateVector() =
            sigmoidVecorCreateVector i
   

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

    let i = ExecBench.i
    Console.WriteLine("--- CHECK ---")
    sigmoidArray i |> (print "Array")
    sigmoidList i |> (print "List")
    sigmoidSeq i |> (print  "Seq")
    sigmoidSeries i |> (print "List")
    sigmoidVecorInitFromArray i |> (print "VectorInitFromArray")
    sigmoidVecorCreateVector i |> (print "VecorCreateVector")


    Console.WriteLine()



    w.Close()

    Console.SetOut(console)

    Console.WriteLine("Done!")

    Console.ReadLine() |> ignore

    0 // return an integer exit code
