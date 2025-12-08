open System.IO
open System

let input = File.ReadAllLines("Input.txt")

let mutable freshRanges : (int64 * int64) list = []
let mutable ingredients : int64 list = []

let mutable answer = 0
//let freshIngredients : int64 list = []


for line in input do
    if line.Contains('-') then
        let parts = line.Trim().Split('-')
        let startId = int64 parts[0]
        let endId = int64 parts[1]
        freshRanges <- (startId, endId) :: freshRanges
    else if not (String.IsNullOrWhiteSpace(line)) then
        let id = int64 line
        ingredients <- id :: ingredients
    else
        ()


let IdsInFreshRanges () =
    let isInFreshRange (id: int64) =
        freshRanges |> List.exists (fun (start, end') -> id >= start && id <= end')

    for id in ingredients do
        if isInFreshRange id then
            answer <- answer + 1
            printfn "Ingredient %d is fresh" id
        else 
            printfn "Ingredient %d is NOT fresh" id

    printfn "Answer: %i" answer

let MergeRanges (ranges: (int64 * int64) list) : (int64 * int64) list =
    let sortedRanges = ranges |> List.sortBy fst
    let rec merge acc remaining =
        match remaining with
        | [] -> List.rev acc
        | (start, end') :: rest ->
            match acc with
            | [] -> merge [(start, end')] rest
            | (lastStart, lastEnd) :: accRest ->
                if start <= lastEnd + 1L then
                    let newEnd = max lastEnd end'
                    merge ((lastStart, newEnd) :: accRest) rest
                else
                    merge ((start, end') :: acc) rest
    merge [] sortedRanges

let AmountOfFreshIngredients () =
    let mergedRanges = MergeRanges freshRanges
    let count = mergedRanges |> List.sumBy (fun (start, end') -> end' - start + 1L)
    printfn "Total amount of fresh ingredients: %d" count


//IdsInFreshRanges()
AmountOfFreshIngredients()