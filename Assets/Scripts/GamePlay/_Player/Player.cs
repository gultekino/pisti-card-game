using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[System.Serializable]

public class Player : MonoBehaviour
{
    private List<Card> cardsInHand = new List<Card>();
    private List<Card> cardsInStash =new List<Card>();
    
    public delegate void PlayerPlayed(Card playedCard);
    public event PlayerPlayed EPlayerPlayed;
    
    public bool CanPlayARound()
    {
        return cardsInHand.Count != 0;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            EPlayerPlayed?.Invoke(new Card(null, 3, Shape.Club, 3, CardColor.Black));
        }
    }

    public void TakeCards(IEnumerable<Card> cardsToPlay)
    {
        cardsInHand.AddRange(cardsToPlay);
    }
}
