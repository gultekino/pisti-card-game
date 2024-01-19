using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayer : Player
{
    private AIBehaviourBase aiBehaviour = new CardCounterAIBehaviour();
    private bool executingMove = false;

    private void Start()
    {
        PlayerManager.Instance.EAPlayerPlayed += UpdateKnowledge;
    }

    private void UpdateKnowledge(Card playedcard, Player player)
    {
        aiBehaviour.UpdateKnowledge(playedcard.Number);
    }

    private void Update()
    {
        if (PermissionToPlay == false || executingMove)
            return;
        StartCoroutine(ExecuteMove());
    }

    private IEnumerator ExecuteMove()
    {
        executingMove = true;
        yield return new WaitForSeconds(0.2f);
        var cardToPlay = aiBehaviour.DecideMove(TableManager.Instance.GetCarNumOnTopCard(), cardsInHand);
        TryPlay(cardToPlay);
        executingMove = false;
    }
}
