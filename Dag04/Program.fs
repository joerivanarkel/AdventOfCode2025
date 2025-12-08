open System.IO
open System

let mutable input = 
    File.ReadAllLines("input.txt")
    |> Array.map (fun line -> line.ToCharArray())

let mutable answer = 0
let mutable amountFound = 0

while amountFound > 0 || answer = 0 do
    let mutable newInput = input
    amountFound <- 0

    for rowIndex in 0 .. input.Length - 1 do
        for columIndex in 0 .. input.[rowIndex].Length - 1 do

            if input.[rowIndex].[columIndex] = '@' then 
                let mutable surroundingCount = 0
            
                for surroundingRow in -1 .. 1 do
                    for surroundingColum in -1 .. 1 do
                        if not (surroundingRow = 0 && surroundingColum = 0) then
                            let newRow = rowIndex + surroundingRow
                            let newCol = columIndex + surroundingColum
                        
                            if newRow >= 0 && newRow < input.Length && 
                               newCol >= 0 && newCol < input.[newRow].Length then
                                if input.[newRow].[newCol] = '@' then
                                    surroundingCount <- surroundingCount + 1
            
                if surroundingCount < 4 then
                    printfn "Found @ at (%d, %d) with %d surrounding @'s" rowIndex columIndex surroundingCount
                    amountFound <- amountFound + 1
                    newInput.[rowIndex].[columIndex] <- 'x'

    answer <- answer + amountFound
    input <- newInput




printfn "Answer: %d" answer