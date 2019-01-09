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
  king (Black) :> chessPiece;
  rook (Black) :> chessPiece|]
// Place pieces on board
board.[4,4] <- Some pieces.[0]
board.[1,1] <- Some pieces.[1]
board.[5,5] <- Some pieces.[2]
board.[7,7] <- Some pieces.[2]

let (theonlygame : Game) = Game(player1, player2, board)
theonlygame.run(player1)