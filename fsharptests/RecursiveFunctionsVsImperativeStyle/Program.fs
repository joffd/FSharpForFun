// Learn more about F# at http://fsharp.org


open System
open System.IO
open BenchmarkDotNet.Running
open BenchmarkDotNet.Attributes
open Calendar


module ExecBench =
    
    let d = DateTime(1990,1,1)
    let m = 7300 //730000
    let n = 2000 // 200000

    let listcal = createNRandomCalendar d m n 5
    
    
    
    [<MemoryDiagnoser>]
    type ExecBench() =
        [<Benchmark>]
        member __.Imperative() =
            buildWorkingDaysListImperative d m listcal
                   
        [<Benchmark>]
        member __.Recursive() =
            buildWorkingDaysListRec d m listcal

        [<Benchmark>]
        member __.Choose() =
            buildWorkingDaysListChoose d m listcal

[<EntryPoint>]
let main argv =
    Console.WriteLine("Starting!")
    
    let filestr = "F:\\exitconsole\\" + "imperative_vs_recursive_" 
                    + DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".txt"
    let file = new FileStream(filestr, FileMode.OpenOrCreate, FileAccess.Write)
    let w = new StreamWriter(file)
    let console = Console.Out
    
    Console.SetOut(w)
        
    printfn "Testing speed Imperative vs Recursive-Tail im F#!"
       
    BenchmarkRunner.Run<ExecBench.ExecBench>()
    |> printfn "%A"
    
    Console.WriteLine()
    
    Console.WriteLine("CHeck if list are equals!")
    
    let d = DateTime(1990,1,1)
    let m = 7300 //730000
    let n = 2000 // 200000

    let listcal = createNRandomCalendar d m n 5

    let resImp = buildWorkingDaysListImperative d m listcal
    
    let resRec = buildWorkingDaysListRec d m listcal

    let resChoose = buildWorkingDaysListChoose d m listcal

    
    let eq1 = (resImp = resRec)
    let eq2 = (resRec = resChoose)
    Console.WriteLine("Imperative vs Rec. Same result? " + eq1.ToString())
    Console.WriteLine("Choose vs Rec. Same result? " + eq2.ToString())
    
    w.Close()
    
    Console.SetOut(console)
    
    Console.WriteLine("Done!")
    
    Console.ReadLine() |> ignore
    
    0 // return an integer exit code
    