﻿

module Calendar

    open System

    // Calendar Type
    type public Calendar = { weekendDays:System.DayOfWeek Set; holidays:System.DateTime Set }

    let randomInt m =
        let R = System.Random()
        fun n -> [|for _ in 1..n -> R.Next(m)|] |> Array.distinct


    let createNRandomCalendar (dStart: DateTime) (endDay: int) (nbDraw: int) (nbCal: int) : Calendar list =

        let checkWeekday (d: DateTime) =
            match d.DayOfWeek with
            | DayOfWeek.Saturday | DayOfWeek.Sunday -> None
            | _ -> Some d
        [1..nbCal]
        |> List.map (fun _ ->
                    randomInt endDay nbDraw
                    |> Array.map (fun x -> dStart.AddDays((float) x))
                    |> Array.choose checkWeekday
                    |> Set.ofArray
                    |> (fun x -> { weekendDays = [DayOfWeek.Saturday ; DayOfWeek.Sunday] |> Set.ofList
                                   holidays = x }))



    let public isBusinessDay  calendar (date:System.DateTime) = 
        not (calendar.weekendDays.Contains date.DayOfWeek || calendar.holidays.Contains date)


    let buildWorkingDaysListImperative (startD : DateTime) (maxD: int) (seqCalendar: Calendar list) =
        let mutable res = []
        let mutable tmp = startD
        let mutable i = 0

        for i in 0..1..maxD do
            tmp <- startD.AddDays((float) i)
            let mutable hol = true
            for cal in seqCalendar do
                if (isBusinessDay cal tmp) then
                    hol <- false
            if hol = false then
                res <- res @ [tmp]

        res


    let buildWorkingDaysListRec (startD : DateTime) (maxD: int) (seqCalendar: Calendar list) =

        let (++) (b1: bool) (b2: bool) =
            match (b1,b2) with
            | (true, true) -> true
            | _            -> false

        let checkSeqHoldays d =
            seqCalendar
            |> List.map (fun x -> not (isBusinessDay x d))
            |> List.fold (++) true  

        let listdays =
            [0..maxD]
            |> List.rev
            |> List.map (fun i -> startD.AddDays((float) i))

        //Console.WriteLine("nb")
        //Console.WriteLine(listdays.Length.ToString())


        let rec builList acc (l: DateTime list) =
            //Console.WriteLine(l.Length.ToString())
            match l with
            | [] -> acc
            | x::r -> match (checkSeqHoldays x) with
                      | false -> builList ([x]@acc) r
                      | true  -> builList acc r

        builList [] listdays

            

                
                


        
            






    // TEST
    let d = DateTime(1001,1,1)
    let m = 73000 //730000
    let n = 20000 // 200000

    let listcal = createNRandomCalendar d m n 3

    let resImp = buildWorkingDaysListImperative d m listcal

    let resRec = buildWorkingDaysListRec d m listcal
    resRec.Length

    resImp = resRec

    
    [m..0]
    |> List.map (fun i -> d.AddDays((float) i))
                        