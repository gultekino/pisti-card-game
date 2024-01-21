using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameStateHandler
{
    private static GameState currentGameState = GameState.PreGame;

    public static GameState GameState
    {
        get => currentGameState;
        set
        {
            if (currentGameState == value) return;

            currentGameState = value;
            OnGameStateChange?.Invoke(currentGameState);
        }
    }

    public delegate void GameStateChangeDelegate(GameState newState);
    public static event GameStateChangeDelegate OnGameStateChange;
}

