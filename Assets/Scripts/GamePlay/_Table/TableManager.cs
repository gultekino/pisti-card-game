using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TableManager : MonoBehaviour
{
    [SerializeField] private Transform[] playerLocationsOnTable;
    [SerializeField] private Transform Center;
    private void Start()
    {
        PlayerManager.Instance.EAPlayerPlayed += MoveCardToTheCenter;
        PlayerManager.Instance.EPlayerTookCards += MoveCardsToPlayersLoc;
    }

    private void MoveCardsToPlayersLoc(Card[] playedcard, int playerindex)
    {
        for (int i = 0; i < playedcard.Count(); i++)
        {
            playedcard[i].transform.position = playerLocationsOnTable[playerindex].position+Vector3.right*i*1.25f;
        }
    }


    private void MoveCardToTheCenter(Card playedcard)
    {
        playedcard.transform.position = Center.position;
    }

    private void OnDisable()
    {
        PlayerManager.Instance.EAPlayerPlayed -= MoveCardToTheCenter;

    }
}
