open Chess
open Pieces
open Game
/// Print various information about a piece
let printPiece (board : Board) (p : chessPiece) : unit =
  printfn "%A: %A %A" p p.position (p.availableMoves board)

let board = Board () // Create a board
let player1 = Human White
let player2 = Human Black
let pieces = [|
  king (White) :> chessPiece;
  rook (White) :> chessPiece;
  king (Black) :> chessPiece |]
// Place pieces on the board
// let newPiece = king (White) :> chessPiece
// board.[7,7] <- Some newPiece
board.[4,4] <- Some pieces.[0]
board.[1,1] <- Some pieces.[1]
board.[5,5] <- Some pieces.[2]
board.[7,7] <- Some pieces.[2]

let newplayer = Human(White)

printfn "%A" (newplayer.nextMove board)

// Make moves
board.move (1,1) (3,3) // Moves a piece from (1,1) to (3,1)
printfn "%A" board
Array.iter (printPiece board) pieces
