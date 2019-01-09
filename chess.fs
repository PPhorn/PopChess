module Chess (*//§\label{chessHeader}§*)
open System.Text.RegularExpressions
open System
type Color = White | Black
type Position = int * int(*//§\label{chessTypeEnd}§*)
/// An abstract chess piece §\label{chessPieceBegin}§
[<AbstractClass>] //man kan ikke umiddelbart lave objecter af den type, fordi de
//mangler deres implementation. Så alle brikker skal komme med en implementation
//af relative moves.
type chessPiece(color : Color) =
  let mutable _position : Position option = None
  abstract member nameOfType : string // "king", "rook", ...
  member this.color = color // White, Black
  member this.position // E.g., (0,0), (3,4), etc.
    with get() = _position
    and set(pos) = _position <- pos
  override this.ToString () = // E.g. "K" for white king
    match color with
      White -> (string this.nameOfType.[0]).ToUpper ()
      | Black -> (string this.nameOfType.[0]).ToLower ()
  /// A list of runs, which is a list of relative movements, e.g.,
  /// [[(1,0); (2,0);...]; [(-1,0); (-2,0)]...]. Runs must be
  /// ordered such that the first in a list is closest to the piece
  /// at hand.
  abstract member candiateRelativeMoves : Position list list
  /// Available moves and neighbours ([(1,0); (2,0);...], [p1; p2])
  member this.availableMoves (board : Board) : (Position list * chessPiece list) =
    //possibleMoves gets list of vacant squares from the vacant function in
    //scope of getVacantNNeighbours
    //possibleKills gets list of squares with opponent pieces from the opponent
    //function in scope of getVacantNNeighbours

    let ChessPieces (b: Board) : chessPiece list =
      let mutable newList = List.empty<chessPiece option>
      for i = 0 to 7 do
        for j = 0 to 7 do
          let piece : chessPiece option = b.[i,j]
          match piece with
            Some piece -> newList <- board.[i,j] :: newList
            | None -> ()
      let pieceList = (List.map Option.get (newList)) //fjerne option typen fra listen
      //Filters the list so as to only hold opponent pieces
      let oppList =
        List.filter (fun (x : chessPiece) -> x.color <> this.color) pieceList
      //Gets opponents coordinates
      //let lst = List.map (fun (x : chessPiece) -> x.position.Value) oppList
      //lst
      oppList

    let invalidToSafe (b: Board) (riskZonelist: Position list) =
      let mutable possibleMoves, possibleKills = b.getVacantNNeighbours this
      for elem in possibleKills do
        possibleMoves <- elem.position.Value :: possibleMoves
      let mutable safeList = []
      for elem in possibleMoves do
        if (List.exists(fun x -> x = elem) riskZonelist) = false then
          safeList <- elem :: safeList
      safeList

    let riskZone (b: Board) : Position list =
      if(this.nameOfType.ToLower() = "king") then
        let mutable rookList =
          (ChessPieces b)
          |> List.filter(fun (x : chessPiece) -> x.nameOfType.ToLower() = "rook")
          |> List.map(fun (x : chessPiece) -> x.position.Value)
        let mutable oppking =
          (ChessPieces b)
          |> List.filter(fun (x : chessPiece) -> x.nameOfType.ToLower() = "king")
        let mutable possibleMoves, _ = b.getVacantNNeighbours this
        //for elem in possibleKills do
          //possibleMoves <- elem.position.Value :: possibleMoves
        let mutable invalidMoves = []
        //Checking for rook collision
        for i = 0 to possibleMoves.Length - 1 do
          for j = 0 to rookList.Length - 1 do
            if (fst possibleMoves.[i] = fst rookList.[j]) || (snd possibleMoves.[i] = snd rookList.[j]) then
              invalidMoves <- possibleMoves.[i] :: invalidMoves
        //invalidMoves for king collision
        let oppkingMoves = fst (b.getVacantNNeighbours oppking.[0])
        for i = 0 to possibleMoves.Length - 1 do
          for j = 0 to oppkingMoves.Length - 1 do
            if (possibleMoves.[i] = oppkingMoves.[j]) then
              invalidMoves <- possibleMoves.[i] :: invalidMoves
        //possibleMoves <- List.filter(fun x -> x = invalidMoves) possibleMoves
        invalidToSafe b invalidMoves
      else
        fst (b.getVacantNNeighbours this)

    (riskZone board, snd (board.getVacantNNeighbours this))

/// A board §\label{chessBoardBegin}§
     //let possibleMoves, possibleKills = board.getVacantNNeighbours this

and Board () =
  let _array = Collections.Array2D.create<chessPiece option> 8 8 None
  /// Wrap a position as option type
  let validPositionWrap (pos : Position) : Position option =
    let (rank, file) = pos // square coordinate
    if rank < 0 || rank > 7 || file < 0 || file > 7
    then None
    else Some (rank, file)
  /// Convert relative coordinates to absolute and remove out
  /// of board coordinates.
  let relativeToAbsolute (pos : Position) (lst : Position list) : Position list =
    let addPair (a : int, b : int) (c : int, d : int) : Position =
      (a+c,b+d)
    // Add origin and delta positions
    List.map (addPair pos) lst
    // Choose absolute positions that are on the board
    |> List.choose validPositionWrap
  /// Board is indexed using .[,] notation
  member this.Item
    with get(a : int, b : int) = _array.[a, b]
    and set(a : int, b : int) (p : chessPiece option) =
      if p.IsSome then p.Value.position <- Some (a,b)  (*//§\label{chessItemSet}§*)
      _array.[a, b] <- p
  /// Produce string of board for, e.g., the printfn function.
  override this.ToString() =
    let rec boardStr (i : int) (j : int) : string =
      match (i,j) with
        (8,0) -> ""
        | _ ->
          let stripOption (p : chessPiece option) : string =
            match p with
              None -> ""
              | Some p -> p.ToString()
          // print top to bottom row
          let pieceStr = stripOption _array.[7-i,j]
          //let pieceStr = sprintf "(%d, %d)" i j
          let lineSep = " " + String.replicate (8*4-1) "-"
          match (i,j) with
          (0,0) ->
            let str = sprintf "%s\n| %1s " lineSep pieceStr
            str + boardStr 0 1
          | (i,7) ->
            let str = sprintf "| %1s |\n%s\n" pieceStr lineSep
            str + boardStr (i+1) 0
          | (i,j) ->
            let str = sprintf "| %1s " pieceStr
            str + boardStr i (j+1)
    boardStr 0 0
  /// Move piece by specifying source and target coordinates
  member this.move (source : Position) (target : Position) : unit =
    this.[fst target, snd target] <- this.[fst source, snd source]
    this.[fst source, snd source] <- None
  /// Find the tuple of empty squares and first neighbour if any.
  //tager to funktioner og overskriver det der står på hhv. target og source
  member this.getVacantNOccupied (run : Position list) : (Position list * (chessPiece option)) =
    try
      // Find index of first non-vacant square of a run
      let idx = List.findIndex (fun (i, j) -> this.[i,j].IsSome) run
      let (i,j) = run.[idx]
      let piece = this.[i, j] // The first non-vacant neighbour
      if idx = 0
      then ([], piece)
      else (run.[..(idx-1)], piece)
    with
      _ -> (run, None) // outside the board
  /// find the list of all empty squares and list of neighbours
  member this.getVacantNNeighbours (piece : chessPiece) : (Position list * chessPiece list)  =
    match piece.position with
      None ->
        ([],[])
      | Some p ->
        let convertNWrap =
          (relativeToAbsolute p) >> this.getVacantNOccupied // ">>" Funktions komposition
          // Haer vi to funktioner g(f(x)), dvs. først udregner vi værdien af f på x og derefter g på det
          // med piping ville vi skrive x|>f|>g. Her laver vi en funktion, som vi endnu ikke evaluerer. Og resultatet af funktionskomposition er en funktion. Ligesom h = f >> g eller h = g(f(x)), hvis vi ville vende det om kunne vi skrive g<<f. Vi ender med en funktion, der venter på et input.
        let vacantPieceLists = List.map convertNWrap piece.candiateRelativeMoves
        // Extract and merge lists of vacant squares
        // den tager kalder convertNrap og får ud to lister. Hhv. listen af runs og listen af brikker den kan tage
        let vacant = List.collect fst vacantPieceLists
        // Extract and merge lists of first obstruction pieces and filter out own pieces
        let opponent =
          vacantPieceLists
          |> List.choose snd //pipen piper en liste ind i list.choose, det er det
          //samme som  f(x) --> x|> f. list.choose tager to argumenter, men er her kun givet
          //1 nemlig snd. Derfor piper vi en liste ind på den.
        (vacant, opponent)(*//§\label{chessBoardEnd}§*)

[<AbstractClass>]
type Player (color: Color) =
  member this.playerColor = color
  //abstract member nextMove : Board : Color -> string
  abstract member nameOfType : string
  abstract member piecesOnBoard : Board -> chessPiece list

type Human (color: Color) =
  inherit Player(color)
  override this.nameOfType = "Player" + string(color)

  override this.piecesOnBoard (b: Board) : chessPiece list =
    let myChessPieces (b: Board) : chessPiece list =
      let mutable myList = List.empty<chessPiece option>
      for i = 0 to 7 do
        for j = 0 to 7 do
          let piece : chessPiece option = b.[i,j]
          match piece with
            Some piece -> myList <- b.[i,j] :: myList
            | None -> ()
      let pieceList = (List.map Option.get myList) //fjerne option typen fra listen
      //Filters the list so as to only hold opponent pieces
      let oppList =
        List.filter (fun (x : chessPiece) -> x.color = color) pieceList
      oppList
    //printfn "WOW: %A"
    myChessPieces b


  member this.nextMove (b: Board) (p: Human) : string =
    printfn "Make a move Player %A" p.playerColor
    let mutable input = System.Console.ReadLine() //Takes console input from user

    let checkInput =
      let pattern = @"[a-h][1-8]\s[a-h][1-8]" //Pattern of chess move e.g. a5 a4
      Regex.IsMatch(input, pattern) || (input = "quit")

    let checkMoveInput =
      let liste1 = this.piecesOnBoard b
      if checkInput && input = "quit" then
        input
      elif checkInput && input <> "quit" then
        let chosenPiece : chessPiece =
          let coordOfchosenPiece = ((int input.[0]) - 97, (int input.[1]) - 49)
          b.[fst coordOfchosenPiece, snd coordOfchosenPiece].Value
        let movesOfchosenPiece = fst (chosenPiece.availableMoves b)
        let inputAsMove : Position = ((int input.[3]) - 97, (int input.[4]) - 49) //converts the console string input from letter+number to the corresponding coordinate on the board. e.g from a4 to (0,4)
        if (List.exists (fun x -> x = chosenPiece) liste1) && (List.exists(fun x -> x = inputAsMove) movesOfchosenPiece) then //Checks if the user input matches any valid moves for the possibleM ove list
          printfn "Great Move"
          input //Returns the console input
        else
          printfn "Not a valid chess piece, try again"
          this.nextMove b p
      else
        printfn "Invalid move, Try again"
        this.nextMove b p

    checkMoveInput

type Game(p1: Human, p2: Human, b: Board) =
    let turn = p1
    let inputAsMove (input: string) : Position list = 
        [((int input.[0]) - 97, (int input.[1]) - 49); ((int input.[3]) - 97, (int input.[4]) - 49)]
    let moveSource (lst: Position List) : Position = 
        lst.[0]
    let moveTarget (lst: Position List) : Position = 
        lst.[1]
    member this.run(p: Human) =
      Console.Clear()
      printfn "%A" b
      if not (p1.piecesOnBoard b).IsEmpty || not (p2.piecesOnBoard b).IsEmpty then
        match p.playerColor with
        White -> 
            let move = inputAsMove (p1.nextMove b p)
            b.move (moveSource move) (moveTarget move)
            this.run(p2)
        | Black -> 
            let move = inputAsMove (p2.nextMove b p)
            b.move (moveSource move) (moveTarget move)
            this.run(p1)
      else
        Environment.Exit(0)

// type Game () =
//   let PlayerBlack = Human(Black)
//   let PlayerWhite = Human(White)
//
//   member this.run (b: Board) (piece : chessPiece) : string =

      // this.Piece.Position = toSquare
