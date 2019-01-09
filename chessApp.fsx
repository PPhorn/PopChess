open Chess
open Pieces
/// Print various information about a piece
let printPiece (board : Board) (p : chessPiece) : unit =
  printfn "%A: %A %A" p p.position (p.availableMoves board)

// Create a game
let board = Chess.Board () // Create a board
// Pieces are kept in an array for easy testing
let pieces = [|
  king (White) :> chessPiece;
  rook (White) :> chessPiece;
  king (Black) :> chessPiece |]
// Place pieces on the board
board.[4,4] <- Some pieces.[0]
board.[1,1] <- Some pieces.[1]
board.[0,2] <- Some pieces.[2]
printfn "%A" board
Array.iter (printPiece board) pieces

let newplayer = Human(Black)
newplayer.nextMove board

// Make moves
board.move (1,1) (3,3) // Moves a piece from (1,1) to (3,1)
printfn "%A" board
Array.iter (printPiece board) pieces
