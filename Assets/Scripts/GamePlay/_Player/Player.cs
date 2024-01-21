using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]

public abstract class Player : MonoBehaviour
{
    public static Player playerWonTheLast;
    protected List<Slot> cardHoldingSlots;
    protected StashSlot stashSlot;
    protected Points playerPoints = new();
    protected List<Card> cardsInHand = new();
    private float timeToTakeToSlot = 0.4f;
    public bool PermissionToPlay { get; set; }

    public event Action<Card, Player> OnPlayerPlayed;
    
    protected virtual void Awake()
    {
        cardHoldingSlots = GetComponentsInChildren<Slot>().ToList();
        stashSlot = GetComponentInChildren<StashSlot>();
    }
    

    public bool CanPlayARound()
    {
        return cardsInHand.Count != 0;
    }

    public virtual void TakeCards(IEnumerable<Card> cardsToPlay)
    {
        cardsInHand.AddRange(cardsToPlay);
        AssignCardsToSlots(cardsToPlay);
    }

    public void TryPlayCard(Card playedCard)
    {
        if (cardsInHand.Contains(playedCard) && PermissionToPlay)
        {
            OnPlayerPlayed?.Invoke(playedCard, this);
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
        card.transform.DOMove(slot.transform.position,timeToTakeToSlot);
    }

    protected void AssignCardsToSlots(IEnumerable<Card> cardsToPlay)
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