using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class DeckBuilder : MonoBehaviour
{
    [SerializeField] private GameObject CardPrefab;
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
                cards.Add(new Card(InstantiateImageCard(suit, i), i, suit.Shape, 0, suit.Color));
            else
                cards.Add(new Card(InstantiateRegularCard(suit, i), i, suit.Shape, 0, suit.Color));
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
    
    private GameObject InstantiateRegularCard(Suit suit, int cardNum)
    {
        GameObject instantiatedObj = Instantiate(CardPrefab,CardContainer);
        var spriteRenderer = instantiatedObj.GetComponent<SpriteRenderer>();
        var text = instantiatedObj.GetComponentInChildren<TMP_Text>();
        spriteRenderer.sprite = suit.DefaultSprite;
        
        text.SetText(cardNum.ToString());
        return instantiatedObj;
    }

    GameObject InstantiateImageCard(Suit suit, int cardNum)
    {
        int spriteIndexInArray = cardNum - 11;
        GameObject instantiatedObj = Instantiate(CardPrefab, CardContainer);
        var spriteRenderer = instantiatedObj.GetComponent<SpriteRenderer>();
        var text = instantiatedObj.GetComponentInChildren<TMP_Text>();

        spriteRenderer.sprite = suit.ImageSprites[spriteIndexInArray];
        text.SetText("");
        return instantiatedObj;
    }
}
