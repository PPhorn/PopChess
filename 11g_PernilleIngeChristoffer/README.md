Simple Chess
by Christoffer, Inge and Pernille

////////////////////////
Til Cami

Vores README fil er formateret som .md, da det er standardformatet for README’s på GIT. Når du opretter et nyt Repo i GIT spørger den endda, om der skal oprettes en README right-away, hvilken oprettes som .md pr. automatik.

////////////////////////
Getting started

To start the game follow these steps:
1. Open a terminal
2. Locate the folder 11g_PernilleIngeChristoffer
3. When inside the folder run the following in the terminal/command: bash compileSimpleChess.sh

If you are using windows or the above instructions does not work please follow these steps:
1. Open a cmd
2. Locate the folder  11g_PernilleIngeChristoffer
3. When inside the folder compile the application by running the following in the terminal/command prompt:
fsharpc -a chess.fs -a pieces.fs
fsharpc -r chess.dll -r pieces.dll chessApp.fsx
mono chessApp.exe

///////////////////////
Playing the game

After compiling the application in the terminal/command prompt the user will have a cleared console displaying only the chess game-board in its initial game state.
By default the game is initialized as a two-player game based on two human players. Unfortunately the current version of the game is not yet developed to handle Human vs. Computer nor Computer vs. Computer.

To play the game, follow the on-screen instructions indicating which player is up. To spice the game up, the user playing as Player Black has red as their piece color, the user playing as Player White has green as their piece color.

To make a move, a player must first type the current position of the piece, which he/she wishes to move, followed by the target position. E.g. “b2 b8” moves the white (green) Rook from its initial position to the far right square of row b (column 8).

The game ends when the king of either one of the players gets killed.

To start a new game re-compile the application.

///////////////////////
In order to compile the game you must have fsharp installed.