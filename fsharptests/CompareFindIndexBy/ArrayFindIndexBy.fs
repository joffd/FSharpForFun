module ArrayFindIndexBy
 
    let findIndexByTailRec (f: 'a -> 'a -> bool) (l: 'a array) =
       let len = l.Length
       let rec findIndexByRec indexCur indexMax maxValue f (l: 'a array) = 
           //printfn "index Cur %i" indexCur
           //printfn "index Max %i" indexMax
           //printfn "max Value %s" (maxValue.ToString())
           match l with
           | [||] -> indexMax                                                            
           | [|var|] -> if var > maxValue then len - 1 else indexMax
           | _    -> match (l.[0]) with
                     | head when (f l.[0] maxValue) ->  findIndexByRec (indexCur + 1) (indexCur - 1) head f l.[1 ..]
                     | _ -> findIndexByRec  (indexCur + 1) indexMax maxValue f l.[1 ..]
       
       match l with
       | [||] -> 0
       | _ -> findIndexByRec 1 0 (Array.head l) f l

    let findMaxByIndexed (g: (int * 'a) array -> int) (l: 'a array) =
        match Array.isEmpty l with
        | true -> None
        | _    ->
                    l
                    |> Array.indexed
                    |> g
                    |> Some

    let findMaxByMapi (g: (int * 'a) array -> int) (l: 'a array) =
        match Array.isEmpty l with
        | true -> None
        | _    ->
                    l
                    |> Array.mapi (fun i v -> i, v)
                    |> g
                    |> Some

    let findMaxImperativeFor (f: 'a -> 'a -> bool) (l: 'a array) =       
        match Array.isEmpty l with
        | true -> None
        | _    -> 
                let mutable i = 0 
                let mutable m = l.[0]
                for j in [|0..l.Length - 1|] do
                    if f l.[j] m then
                        m <- l.[j]
                        i <- j
                Some i

    let findMaxImperativeForeach (f: 'a -> 'a -> bool) (l: 'a array) =
        match Array.isEmpty l with
        | true -> None
        | _    ->
                    let mutable i = 0
                    let mutable mi = 0
                    for v in l do
                        if f v l.[mi] then                            
                            mi <- i
                        i <- i + 1
                    Some mi
                    

    let findMaxImperativeIteri (f: 'a -> 'a -> bool) (l: 'a array) =
        match Array.isEmpty l with
        | true -> None
        | _    ->
                    let mutable i = 0
                    let mutable m = l.[0]
                    l
                    |> Array.iteri (fun j v -> 
                                    if f v m then 
                                             i <- j
                                             m <- v    ) |> ignore
                    Some i


    let findMaxByMax (l: 'a array) =
        match Array.isEmpty l with
        | true -> None
        | _    -> 
                    let m = Array.max l
                    Array.findIndex (fun v -> v = m) l
                    |> Some

    let findMaxByList (l: 'a array) =
        match Array.isEmpty l with
        | true -> None
        | _    -> 
                    let m = l |> Array.toList
                    m |> ListFindIndexBy.findIndexByTailRec (>)
                      |> Some

      
    let array1 i= 
        let rnd = System.Random()
        Array.init i (fun _ -> rnd.NextDouble())


    