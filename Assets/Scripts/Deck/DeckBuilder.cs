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
        List<Card> cards = new List<Card>(52);
        for (int i = 0; i < suits.Length; i++)
            AddCardsToList(cards,suits[i]);

        GivePointsToTheCards(cards);
        return cards;
    }
    
    private void AddCardsToList(List<Card> cards,Suit suit)
    {
        for (int i = 1; i <= 13; i++)
        {
            if (i > 10)
                cards.Add(InstantiateImageCard(suit, i));
            else
                cards.Add(InstantiateRegularCard(suit, i));
        }
    }

    private void GivePointsToTheCards(List<Card> cards)
    {
        var clubTwo = cards.Where(card => (card.Value == 2 && card.Shape == Shape.Club));
        foreach (var c in clubTwo)
            c.Points = 2;
        
        var diaTen = cards.Where(card => (card.Value == 10 && card.Shape == Shape.Diamond));
        foreach (var c in diaTen)
            c.Points = 3;
        
        var a = cards.Where(card => (card.Value == 1));
        foreach (var c in a)
            c.Points = 1;

        var j = cards.Where(card => (card.Value == 11));
        foreach (var c in j)
            c.Points = 1;
    }
    
    private Card InstantiateRegularCard(Suit suit, int cardNum)
    {
        Card instantiatedObj = Instantiate(CardPrefab,CardContainer);
        instantiatedObj.CardInit(cardNum,suit.Shape,suit.Color,suit.DefaultSprite,cardNum.ToString());
        return instantiatedObj;
    }

    Card InstantiateImageCard(Suit suit, int cardNum)
    {
        int spriteIndexInArray = cardNum - 11;
        Card instantiatedObj = Instantiate(CardPrefab, CardContainer);
        instantiatedObj.CardInit(cardNum,suit.Shape,suit.Color,suit.ImageSprites[spriteIndexInArray],"");
        return instantiatedObj;
    }
}
