open System.IO

let mutable dial = 50
let mutable answer = 0

let input = File.ReadAllLines("input.txt")
for line in input do
    let direction = line.[0]
    let distance = line.[1..] |> int

    for i in 1 .. abs distance do
        match direction with
        | 'L' -> dial <- dial - 1
        | 'R' -> dial <- dial + 1

        if dial = 0 then
            answer <- answer + 1

        if dial = 100 then
            dial <- 0
            answer <- answer + 1
        elif dial = -1 then
            dial <- 99

printfn "Answer: %i" answer