



public enum GameStateEnum
{
    WhitePlayerTurn,
    BlackPlayerTurn,
    Checkmate
}


public class GameState
{
    Player whitePlayer = new Player();
    Player blackPlayer = new Player();

    Board board = Board.Instance;



}