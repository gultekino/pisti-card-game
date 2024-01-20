using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameMenu;
    [SerializeField] private GameEndMenu endMenu;
    void Start()
    {
        GameStateHandler.EOnGameStateChange += OnGameEnd;
    }

    private void OnGameEnd(GameState changedState)
    {
        if (changedState == GameState.GameEnd)
        {
            var points = PlayerManager.Instance.GetPlayerPoints();
            endMenu.GameEndUI(points);
            gameMenu.gameObject.SetActive(true);
        }
    }

    public void ClickedPlayAgain()
    {
        gameMenu.gameObject.SetActive(false);
    }
    
    public void ClickedRestart()
    {
        gameMenu.gameObject.SetActive(false);
    }
}
