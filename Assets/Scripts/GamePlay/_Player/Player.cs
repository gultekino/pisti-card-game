using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]

public class Player : MonoBehaviour
{
    public static Player playerWonTheLast;
    protected List<Card> cardsInHand = new List<Card>();
    protected List<Slot> cardHoldingSlots;
    protected StashSlot stashSlot;
    protected Points playerPoints = new();
    public delegate void PlayerPlayed(Card playedCard, Player player);
    public event PlayerPlayed EPlayerPlayed;

    private void Awake()
    {
        cardHoldingSlots = GetComponentsInChildren<Slot>().ToList();
        stashSlot = GetComponentInChildren<StashSlot>();
    }
    
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
        TakeCardsToEmptySlots(cardsToPlay);
    }

    public void TryPlayCard(Card playedCard)
    {
        if (cardsInHand.Contains(playedCard) && PermissionToPlay)
        {
            EPlayerPlayed?.Invoke(playedCard, this);
            EmptyPlayedCardSlot(playedCard);
            cardsInHand.Remove(playedCard);
        }
    }

    private void EmptyPlayedCardSlot(Card playedCard)
    {
        cardHoldingSlots.First(s => s.Carrying == playedCard).EmptySlot();
    }

    private void TakeCardToEmptySlot(Card card)
    {
        var slot = cardHoldingSlots.First(s => s.IsEmpty);
        slot.CarryNewItem(card);
        card.transform.position = slot.transform.position;
    }

    private void TakeCardsToEmptySlots(IEnumerable<Card> cardsToPlay)
    {
        for (int i = 0; i < cardsToPlay.Count(); i++)
        {
            TakeCardToEmptySlot(cardsToPlay.ElementAt(i));
        }
    }

    public void MadeAPisti()
    {
        Debug.Log("Pişti");
        playerPoints.MadePisti();
    }

    public void CollectCards(IEnumerable<Card> cardsInTheCenter)
    {
        playerWonTheLast = this;
        stashSlot.CarryNewCards(cardsInTheCenter);
    }

    public void CalculatePoints()
    {
        playerPoints.CalculateTotalPoints(stashSlot.CardsInStashSlot);
    }

    public int GetPoints()
    {
        return playerPoints.GamePoints;
    }

    public void TakePoints(int point)
    {
        playerPoints.AddDirectPoints(point);
    }

    public int GetCardCount()
    {
        return stashSlot.CardsInStashSlot.Count;
    }

    public void ResetScore()
    {
        playerPoints.ResetPoints();
    }
}