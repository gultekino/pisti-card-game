using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class GameEndMenu
{
    [SerializeField] private TMP_Text[] playerTexts;
    private const string scoreText = "Player {0} score: {1}";
    public void UpdateGameEndUI(List<int> points)
    {
        for (int i = 0; i < playerTexts.Length; i++)
            playerTexts[i].SetText(scoreText,i+1,points[i]);
    }
}
