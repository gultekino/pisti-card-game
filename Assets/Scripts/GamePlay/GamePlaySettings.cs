using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class GamePlaySettings
{
    [SerializeField] private int playerCount;
    [SerializeField] private int numCardsToGive;

    public int PlayerCount => playerCount;

    public int NumCardsToGive => numCardsToGive;
}
