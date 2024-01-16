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
    
    public delegate void PlayerPlayed(Card playedCard,Player player);
    public event PlayerPlayed EPlayerPlayed;
    public bool PermissionToPlay { get; set; }

    public bool CanPlayARound()
    {
        return cardsInHand.Count != 0;
    }
    
    public void PlayCard(Card card)
    {
        
    }

    
    public void TakeCards(IEnumerable<Card> cardsToPlay)
    {
        cardsInHand.AddRange(cardsToPlay);
    }

    public void TryPlay(Card playedCard)
    {
        if (cardsInHand.Contains(playedCard) && PermissionToPlay)
        {
            EPlayerPlayed?.Invoke(playedCard,this);
            cardsInHand.Remove(playedCard);
        }
    }
}
