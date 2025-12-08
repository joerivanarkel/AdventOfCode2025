open System.IO

let hasPattern (s: string) : bool =
    let len = s.Length
    let rec loop size =
        if size > len / 2 then
            false
        elif len % size <> 0 then
            loop (size + 1)
        else
            let p = s.Substring(0, size)
            let mutable i = size
            let mutable ok = true
            while ok && i < len do
                if s.Substring(i, size) <> p then
                    ok <- false
                i <- i + size
            if ok then true else loop (size + 1)
    loop 1

let hasPatternTwice (s: string) : bool =
    let len = s.Length
    let rec loop size =
        if size * 2 > len then
            false
        elif size * 2 <> len then
            loop (size + 1)
        else
            let p = s.Substring(0, size)
            let rest = s.Substring(size, size)
            if p = rest then true else loop (size + 1)
    loop 1





let input = File.ReadAllLines("input.txt")[0]

let ids = 
    input.Split(',') 
    |> Array.map (fun r -> 
        let parts = r.Trim().Split('-')
        (int64 parts[0], int64 parts[1]))
    |> Array.collect (fun (start, end') -> [| start .. end' |])
    |> Set.ofArray
    
let invalidIds = 
    ids
    |> Set.filter (fun id -> 
        let idString = id.ToString()
        if idString |> Seq.distinct |> Seq.length <> idString.Length then
            hasPattern idString
        else
            false

    )



for number in invalidIds |> Set.toList do
    printfn "Invalid ID: %i" number

printfn ""
printfn "Sum of invalid IDs: %i" (invalidIds |> Set.toList |> List.sum)