// Learn more about F# at http://fsharp.org

open System
open System.IO
open BenchmarkDotNet.Running
open BenchmarkDotNet.Attributes
//open ListFindIndexBy

module ExecBenchList =
    let i = 100000
    let l = ListFindIndexBy.list1 i
    
    
    [<MemoryDiagnoser>]
    type ExecBench() =
        [<Benchmark>]
        member __.ListFindMaxByRec() =
            ListFindIndexBy.findIndexByTailRec (>) l
                   
        [<Benchmark>]
        member __.ListFindMaxByIndexed() =
            ListFindIndexBy.findMaxByIndexed l

        [<Benchmark>]
        member __.ListFindMaxByMapi() =
            ListFindIndexBy.findMaxByMapi l

        [<Benchmark>]
        member __.ListFindMaxImperativeFor() =
            ListFindIndexBy.findMaxImperativeFor l

        [<Benchmark>]
        member __.ListFindMaxImperativeForeach() =
            ListFindIndexBy.findMaxImperativeForeach l

        [<Benchmark>]
        member __.ListFindMaxImperativeIteri() =
            ListFindIndexBy.findMaxImperativeIteri l
    




module ExecBenchArray =
    let i = 100000
    let l = ArrayFindIndexBy.array1 i
    let f = (>)
    let g l = l |> Array.maxBy snd
                |> fst

    
    [<MemoryDiagnoser>]
    type ExecBench() =
        //[<Benchmark>]
        //member __.ArrayFindMaxByRec() =
        //    ArrayFindIndexBy.findIndexByTailRec (>) l
                   
        [<Benchmark>]
        member __.ArrayFindMaxByIndexed() =
            ArrayFindIndexBy.findMaxByIndexed g l

        [<Benchmark>]
        member __.ArrayFindMaxByMapi() =
            ArrayFindIndexBy.findMaxByMapi g l

        [<Benchmark>]
        member __.ArrayFindMaxImperativeFor() =
            ArrayFindIndexBy.findMaxImperativeFor f l

        [<Benchmark>]
        member __.ArrayFindMaxImperativeForeach() =
            ArrayFindIndexBy.findMaxImperativeForeach f l

        [<Benchmark>]
        member __.ArrayFindMaxImperativeIteri() =
            ArrayFindIndexBy.findMaxImperativeIteri f l

        [<Benchmark>]
        member __.ArrayFindMaxByMax() =
            ArrayFindIndexBy.findMaxByMax l

        [<Benchmark>]
        member __.ArrayFindMaxByList() =
            ArrayFindIndexBy.findMaxByList l

   

[<EntryPoint>]
let main _ =
    
    let f = (>)
    let g l = l |> Array.maxBy snd
                |> fst

    Console.WriteLine("Starting!")

    let filestr = "F:\\exitconsole\\" + "findMax_" 
                    + DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".txt"
    let file = new FileStream(filestr, FileMode.OpenOrCreate, FileAccess.Write)
    let w = new StreamWriter(file)
    let console = Console.Out

    Console.SetOut(w)
    
    printfn "Testing speed findMax"
   
    //BenchmarkRunner.Run<ExecBenchList.ExecBench>()
    //|> printfn "%A"

    BenchmarkRunner.Run<ExecBenchArray.ExecBench>()
    |> printfn "%A"


    Console.WriteLine()
    Console.WriteLine("CHECK LIST")

    let l = ExecBenchList.l

    Console.WriteLine(ListFindIndexBy.findIndexByTailRec (>) l)
    Console.WriteLine(ListFindIndexBy.findMaxByIndexed l)
    Console.WriteLine(ListFindIndexBy.findMaxByMapi l)
    Console.WriteLine(ListFindIndexBy.findMaxImperativeFor l)
    Console.WriteLine(ListFindIndexBy.findMaxImperativeForeach l)
    Console.WriteLine(ListFindIndexBy.findMaxImperativeIteri l)

    Console.WriteLine()
    Console.WriteLine("CHECK ARRAY")

    let a = ExecBenchArray.l
    
    Console.WriteLine(ArrayFindIndexBy.findIndexByTailRec (>) a)
    Console.WriteLine(ArrayFindIndexBy.findMaxByIndexed g a)
    Console.WriteLine(ArrayFindIndexBy.findMaxByMapi g a)
    Console.WriteLine(ArrayFindIndexBy.findMaxImperativeFor f a)
    Console.WriteLine(ArrayFindIndexBy.findMaxImperativeForeach f a)
    Console.WriteLine(ArrayFindIndexBy.findMaxImperativeIteri f a)

    w.Close()

    Console.SetOut(console)

    Console.WriteLine("Done!")

    Console.ReadLine() |> ignore

    0 // return an integer exit code

