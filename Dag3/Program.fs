open System.IO
open System

let input = File.ReadAllLines("input.txt")
let mutable answer = 0L

let rec findMaxSequence (s: string) : char =
    if s.Length = 1 then
        s.[0]
    else
        let first = s.[0]
        let restMax = findMaxSequence (s.[1..])
        if first > restMax then first else restMax

for line in input do
    let targetLength = 12
    let mutable result = ""
    let mutable remaining = line
    
    for position in 0 .. targetLength - 1 do
        let charsNeeded = targetLength - position - 1
        let searchLength = remaining.Length - charsNeeded
        
        if searchLength > 0 then
            let maxChar = 
                remaining.[0..searchLength - 1]
                |> Seq.max
            
            printfn "Position %d: Found max '%c' in first %d chars (need %d chars remaining)" position maxChar searchLength charsNeeded
            
            result <- result + string maxChar
            
            let index = remaining.IndexOf(maxChar)
            remaining <- remaining.[index + 1..]
    
    printfn "Result: %s" result
    printfn ""
    
    answer <- answer + (result |> int64)

printfn "Answer: %i" answer