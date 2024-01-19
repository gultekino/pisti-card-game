using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]

public class Player : MonoBehaviour
{
    protected List<Card> cardsInHand = new List<Card>();
    protected List<Card> cardsInStash = new List<Card>();
    protected List<Slot> cardHoldingSlots;

    private void Awake()
    {
        cardHoldingSlots = GetComponentsInChildren<Slot>().ToList();
    }

    public delegate void PlayerPlayed(Card playedCard, Player player);

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
        TakeCardsToEmptySlots(cardsToPlay);
    }

    public void TryPlay(Card playedCard)
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
}