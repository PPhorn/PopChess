let valuesList = [Some k; Some K; Some R]
//
// let resultPick = List.pick (fun elem ->
//                     match elem with
//                     | (Some "R") -> Some value
//                     | _ -> None) valuesList
// printfn "%A" resultPick


// let isWhite x = x = (Some R) || x = (K Some)
// match List.tryFind isWhite valuesList with
// | Some value -> printfn "The first even value is %d." value
// | None -> printfn "There is no even value in the list."


// let listWords = [ "and"; "Rome"; "Bob"; "apple"; "zebra" ]
// let isCapitalized (string1:string) = System.Char.IsUpper string1.[0]
// let results = List.choose (fun elem ->
//     match elem with
//     | elem when isCapitalized elem -> Some(elem + "'s")
//     | _ -> None) listWords
// printfn "%A" results



// let lift =
//     let lst = [Some "k"; Some "K"; Some "R"]
//     if (List.contains (None "k") lst) then None
//     else Some (List.map Option.get lst)
    // List.map (fun (x,y) ->
    //   (fst (Option.get a.position) + x, snd (Option.get a.position) + y)) nc
//(l: string option list)
let lst = [Some 1; Some 2; Some 3]
printfn "%A" (List.contains (Some k) valuesList)
printfn "%A" (List.map Option.get valuesList)

let pieceCharList = (List.map Option.get valuesList)
let isCapitalized = System.Char.IsUpper pieceCharList
// let results = List.choose (fun elem ->
//     match elem with
//     | elem when isCapitalized elem -> Some(elem + "'s")
//     | _ -> None) listWords
// printfn "%A" results


//
// member this. (p: chessPiece) : =
//   match this.color with
//   Black ->
//            match whiteChessPieces with
//            R this._position p._position
//            | K
//   | White -> blackChessPiece



let safeMoves =
  let mutable pisP = ChessPieces board //liste med output fra ChessPieces funktionen
  let mutable safeMovesList = List.empty<Position list>
  let possibleMoves, possibleKills = board.getVacantNNeighbours this
  possibleMoves
  let compareMossibleMovesNoppCoord =
    List.compareWith (fun elm1 elm2 -> if elm1 = elm2 then elm1 = None) pisP possibleMoves
  pisP
  //for i in possibleMoves do
  //   match this.position with
  //     |i when fst = 1 -> printfn "NOW: %A" i
