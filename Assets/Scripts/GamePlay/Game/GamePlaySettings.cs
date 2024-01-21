using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class GamePlaySettings
{
    [SerializeField] private int playerCount;
    [SerializeField] private int numCardsToGive;
    [SerializeField] private bool playUntilNoCardsLeft;

    public int PlayerCount => playerCount;

    public int NumCardsToGive => numCardsToGive;
    public bool PlayUntilNoCardsLeft => playUntilNoCardsLeft;

}
