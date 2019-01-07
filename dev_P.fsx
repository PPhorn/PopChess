let valuesList = [Some k; Some R; Some K]

// let resultPick = List.pick (fun elem ->
//                     match elem with
//                     | (Some, value) -> Some value
//                     | _ -> None) valuesList
// printfn "%A" resultPick


let isWhite x = x = (Some R) || x = (K Some)
match List.tryFind isWhite valuesList with
| Some value -> printfn "The first even value is %d." value
| None -> printfn "There is no even value in the list."
