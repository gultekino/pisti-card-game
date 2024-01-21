using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameMenu;
    [SerializeField] private GameEndMenu endMenu;
    void Start()
    {
        GameStateHandler.OnGameStateChange += HandleGameStateChange;
    }

    private void HandleGameStateChange(GameState changedState)
    {
        if (changedState == GameState.GameEnd)
        {
            List<int> points = PlayerManager.Instance.GetPlayerPoints();
            endMenu.UpdateGameEndUI(points);
            gameMenu.SetActive(true);
        }
    }

    public void OnPlayAgainClicked()
    {
        gameMenu.SetActive(false);
        GameStateHandler.GameState = GameState.Restart;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnDisable()
    {
        GameStateHandler.OnGameStateChange -= HandleGameStateChange;
    }
}
