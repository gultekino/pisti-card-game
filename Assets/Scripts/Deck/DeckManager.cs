using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public static DeckManager Instance => instance;
    private static DeckManager instance;
    
    private DeckBuilder deckBuilder;
    private List<Card> cards;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }

        instance = this;
    }

    void Start()
    {
        deckBuilder = GetComponent<DeckBuilder>();
        deckBuilder.BuildDeck();
    }

    void Update()
    {
        
    }
}
