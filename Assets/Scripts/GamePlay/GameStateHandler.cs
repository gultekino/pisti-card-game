using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameStateHandler
{
    private static GameState gameState = GameState.PreGame;
    public static GameState GameState
    {
        get {return gameState;}
        set {
            if (gameState == value) return;
            gameState = value;
            EOnGameStateChange?.Invoke(gameState);
        }
    }
    public delegate void OnGameStateChange(GameState newVal);
    public static event OnGameStateChange EOnGameStateChange;
}
