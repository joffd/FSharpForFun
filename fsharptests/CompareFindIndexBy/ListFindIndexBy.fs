module ListFindIndexBy

    let rec private findIndexByRec indexCur indexMax maxValue f l = 
        match l with
        | [] -> indexMax                                                            
        | head::tail -> match head with
                        | head when (f head maxValue) ->  findIndexByRec (indexCur + 1) indexCur head f tail
                        | _ -> findIndexByRec  (indexCur + 1) indexMax maxValue f tail

    let findIndexByTailRec (f: 'a -> 'a -> bool) (l: 'a list) =
       match l with
       | [] -> 0
       | head::_ -> findIndexByRec 0 0 head f l

    let findMaxByIndexed (l: 'a list) =
        l
        |> List.indexed
        |> List.maxBy snd
        |> fst

    let findMaxByMapi (l: 'a list) =
        l
        |> List.mapi (fun i v -> i, v)
        |> List.maxBy snd
        |> fst

    let findMaxImperativeFor (l: 'a list) =
        match List.isEmpty l with
        | true -> 0
        | _    -> 
                let mutable i = 0 
                let mutable m = l.[0]
                for j in [|0..l.Length - 1|] do
                    if l.[j] > m then
                        m <- l.[j]
                        i <- j
                i

    let findMaxImperativeForeach (l: 'a list) =
        match List.isEmpty l with
        | true -> 0
        | _    -> 
                    let mutable i = 0
                    let mutable mi = 0
                    let mutable m = l.[0]
                    for v in l do
                        if v > m then
                            m <- v
                            mi <- i
                        i <- i + 1
                    mi


    let findMaxImperativeIteri (l: 'a list) =        
        match List.isEmpty l with
        | true -> 0
        | _    -> 
                    let mutable i = 0
                    let mutable m = l.[0]
                    l
                    |> List.iteri (fun j v -> 
                                    if v > m then 
                                             i <- j
                                             m <- v    ) |> ignore
                    i

     
       
       
    let list1 i= 
        let rnd = System.Random()
        List.init i (fun _ -> rnd.NextDouble())



