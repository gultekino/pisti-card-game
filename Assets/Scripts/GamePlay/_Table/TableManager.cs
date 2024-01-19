using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TableManager : MonoBehaviour
{
    [SerializeField] private Transform[] playerLocationsOnTable;
    [SerializeField] private Transform Center;
    [SerializeField] private Transform DeckLoc;
    private List<Card> cardsInTheCenter = new();
    public static TableManager Instance => instance;
    private static TableManager instance;
    
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }

        instance = this;
    }

    private void Start()
    {
        PlayerManager.Instance.EAPlayerPlayed += HandleCardOnTable;
    }

    public Transform GetPlayerLoc(int index)
    {
        return playerLocationsOnTable[index];
    }

    public Transform GetDeckLoc()
    {
        return DeckLoc;
    }

    private void HandleCardOnTable(Card playedcard, Player player)
    {
        HandleGameRules(playedcard);
        cardsInTheCenter.Add(playedcard);
        playedcard.transform.position = Center.position;
        //if it makes a match make player take cards into stash
    }

    private bool HandleGameRules(Card playedcard)
    {
        SameNumberTakeRule snTR = new SameNumberTakeRule();
        JTakesAll jTakesAll = new JTakesAll();
        if (snTR.ApplyGameRule(cardsInTheCenter.LastOrDefault(), playedcard))
        {
            bool isPistiPosible = cardsInTheCenter.Count == 1;
            if (isPistiPosible)
                Debug.Log("Pisti");
            Debug.Log("Same Num Rule");
            return true;
        }

        if (jTakesAll.ApplyGameRule(cardsInTheCenter.LastOrDefault(),playedcard))
        {
            Debug.Log("JTakesAlll");            
            return true;
        }

        return false;
    }

    public CardNum GetCarNumOnTopCard()
    {
        return cardsInTheCenter.LastOrDefault()!.Number;
    }

    private void OnDisable()
    {
        PlayerManager.Instance.EAPlayerPlayed -= HandleCardOnTable;
    }
}
