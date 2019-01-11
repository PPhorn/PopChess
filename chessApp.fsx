open Chess
open Pieces
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
  rook (Black) :> chessPiece;|]
// Place pieces on the board
let placeRooks =
board.[0,0] <- Some pieces.[0]
board.[1,1] <- Some pieces.[1]
board.[7,7] <- Some pieces.[2]
board.[6,6] <- Some pieces.[3]

let newplayer = Human(White)

//printfn "%A" (newplayer.nextMove board)
let justgame = Game(player1, player2, board)
justgame.run(player1)
// Make moves
// board.move (1,1) (3,3) // Moves a piece from (1,1) to (3,1)
// printfn "%A" board
// Array.iter (printPiece board) pieces
