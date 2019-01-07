fsharpc -a chess.fs
fsharpc -r chess.dll -a pieces.fs
fsharpc -r chess.dll -r pieces.dll chessApp.fsx
mono chessApp.exe