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
        GameStateHandler.GameState = GameState.Restart;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void ClickedResetScores()
    {
        PlayerManager.Instance.ResetPlayerScores();
        ClickedPlayAgain();
        gameMenu.gameObject.SetActive(false);
    }
}
