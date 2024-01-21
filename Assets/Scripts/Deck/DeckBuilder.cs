using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class DeckBuilder : MonoBehaviour
{
    [SerializeField] private Card CardPrefab;
    [SerializeField] private Transform CardContainer;
    [SerializeField] private Suit[] suits;

    public List<Card> BuildDeck()
    {
        var cards = new List<Card>(52);
        foreach (var suit in suits)
        {
            AddCardsForSuit(cards, suit);
        }

        AssignPointsToCards(cards);
        return cards;
    }

    private void AddCardsForSuit(List<Card> cards, Suit suit)
    {
        for (int i = 1; i <= 13; i++)
        {
            var card = (i > 10) ? InstantiateSpecialCard(suit, i) : InstantiateRegularCard(suit, i);
            cards.Add(card);
        }
    }

    private void AssignPointsToCards(List<Card> cards)
    {
        AssignPoints(cards, CardNum.Two, Shape.Club, 2);
        AssignPoints(cards, CardNum.Ten, Shape.Diamond, 3);
        AssignPoints(cards, CardNum.As, null, 1);
        AssignPoints(cards, CardNum.J, null, 1);
    }

    private void AssignPoints(IEnumerable<Card> cards, CardNum number, Shape? shape, int points)
    {
        var filteredCards = cards.Where(card => card.Number == number && (shape == null || card.Shape == shape));
        foreach (var card in filteredCards)
        {
            card.Points = points;
        }
    }

    private Card InstantiateRegularCard(Suit suit, int cardNum)
    {
        return InstantiateCard(suit, cardNum, suit.DefaultSprite, cardNum.ToString());
    }

    private Card InstantiateSpecialCard(Suit suit, int cardNum)
    {
        int spriteIndex = cardNum - 11;
        return InstantiateCard(suit, cardNum, suit.ImageSprites[spriteIndex], "");
    }

    private Card InstantiateCard(Suit suit, int cardNum, Sprite sprite, string text)
    {
        var card = Instantiate(CardPrefab, CardContainer);
        card.InitializeCard(cardNum, suit.Shape, suit.Color, sprite, text);
        return card;
    }
}
