


using UnityEngine;
using System;

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
    private GameStateEnum currentState;

    Board board = Board.Instance;

    public void NewGame()
    {
        board.Reset();
    }

    public void SetState(GameStateEnum gameState) { this.currentState = gameState; }
    public GameStateEnum GetState(GameStateEnum gameState) { return this.currentState; }
    public void NextState()
    {
        switch (currentState)
        {
            case GameStateEnum.WhitePlayerTurn:
                Debug.Log("White's turn!");
                break;
            case GameStateEnum.BlackPlayerTurn:
                Debug.Log("Black's turn!");
                break;
            case GameStateEnum.Checkmate:
                Debug.Log("Checkmate!");
                break;
        }
    }



}