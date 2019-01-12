fsharpc -a chess.fs -a pieces.fs
fsharpc -r chess.dll -r pieces.dll chessApp.fsx
mono chessApp.exe
