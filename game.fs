module Game
open Chess
open Pieces
type Game(p1: Player, p2: Player, b: Board) =
    let turn = p1
    let inputAsMove (input: string) : Position list = 
        [((int input.[0]) - 97, (int input.[1]) - 49); ((int input.[3]) - 97, (int input.[4]) - 49)]
    member this.run(p: Player) =
        p.nextMove b
        // let inputAsMove (input: string) : Position list = 
        //     [((int input.[0]) - 97, (int input.[1]) - 49); ((int input.[3]) - 97, (int input.[4]) - 49)]
        // let moveSource (lst: Position List) : Position = 
        //     lst.[0]
        // let moveTarget (lst: Position List) : Position = 
        //     lst.[1]
// Pieces are kept in an array for easy testing

        // if(this.PlayerBlack.piecesOnBoard.Length > 0 && this.PlayerWhite.piecesOnBoard.Length > 0) then
        //     match p with
        //     White -> 
        //         let move = this.PlayerWhite.nextMove
        //         this.board.move (moveSource move) (moveTarget move)
        //         this.run(Black)
        //     | Black -> 
        //         let move = this.PlayerBlack.nextMove
        //         this.board.move (moveSource move) (moveTarget move)
        //         this.run(White)
        // else
        //     System.Enviroment.Exit(0)
