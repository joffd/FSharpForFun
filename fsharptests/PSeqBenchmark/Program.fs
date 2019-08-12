// Learn more about F# at http://fsharp.org

open System
open System.IO
open BenchmarkDotNet.Running
open BenchmarkDotNet.Attributes
open ParallelArray


module ExecBench =
    
    let i = 10000000

    [<MemoryDiagnoser>]
    
    type ExecBench() =
        
        let arr = array1 i
        
        
        [<Benchmark>]
        member __.ArraySimple() =
            sigmoidArraySimple arr

        [<Benchmark>]
        member __.ArrayParralel() =
            sigmoidArrayParallel arr
                   
        
   
[<EntryPoint>]
let main argv =
    Console.WriteLine("Starting!")
    
    let filestr = "F:\\exitconsole\\" + "matrix_vs_tensor_" 
                    + DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".txt"
    let file = new FileStream(filestr, FileMode.OpenOrCreate, FileAccess.Write)
    let w = new StreamWriter(file)
    let console = Console.Out
    
    Console.SetOut(w)
        
    printfn "Array Single Thread VS Parallel map F#!"
       
    BenchmarkRunner.Run<ExecBench.ExecBench>()
    |> printfn "%A"
    
   
    
    Console.WriteLine()
    
    
    
    w.Close()
    
    Console.SetOut(console)
    
    Console.WriteLine("Done!")
    
    Console.ReadLine() |> ignore
    
    0 // return an integer exit code
    