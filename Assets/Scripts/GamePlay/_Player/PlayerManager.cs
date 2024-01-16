using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance => instance;
    private static PlayerManager instance;

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform[] playerSpawnLocs;
    [SerializeField] private Transform playerContainer;
    private List<Player> players = new List<Player>();
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }

        instance = this;
    }


    public void CreatePlayers(int playerCount)
    {
        for (int i = 0; i < playerCount; i++)
        {
            var pos = playerSpawnLocs[players.Count].position;
            var rot = playerSpawnLocs[players.Count].rotation;
            var go = Instantiate(playerPrefab, pos, rot, playerContainer);
            var playerC = go.GetComponent<Player>();
            playerC.EPlayerPlayed += OnPlayerMadeMove;
            players.Add(playerC);
            
        }
    }

    public void OnPlayerMadeMove(Card asd)
    {
        Debug.Log("PlayerPlayed");
    }

    public void GivePlayerPermissionToPlay(int playerIndex)
    {
        
    }

    public bool CanPlayersPlayAnotherRound()
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (!players[i].CanPlayARound())
                return false;
        }
        return true;
    }

    private void OnDestroy()
    {
        for (int i = 0; i < players.Count; i++)
        {
            players[i].EPlayerPlayed -= OnPlayerMadeMove;
        }
    }

    public void GivePlayersCards(List<Card> cardsToPlay)
    {
        var howManyCardsShouldEachPlayerHave = cardsToPlay.Count / players.Count;
        for (int i = 0; i < players.Count; i++)
        {
            var list =cardsToPlay.Skip(i*howManyCardsShouldEachPlayerHave).Take((i+1)*howManyCardsShouldEachPlayerHave);
            players[i].TakeCards(list);
        }
    }
}
