using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
   [SerializeField] private GamePlaySettings gamePlaySettings;

   private void Start()
   {
      PlayerManager.Instance.CreatePlayers(gamePlaySettings.PlayerCount);
      StartCoroutine(GameLoop());
      PlayerManager.Instance.EAPlayerPlayed += StepInGameLoop;
   }

   private bool step;
   private void StepInGameLoop(Card playedcard, Player player)
   {
      step = true;
   }

   private IEnumerator GameLoop()
   {
      while (DeckManager.Instance.DoesPackHasCardsForAnotherRound
                (gamePlaySettings.PlayerCount,gamePlaySettings.NumCardsToGive))
      {
         DeckManager.Instance.GivePlayersCard(gamePlaySettings.PlayerCount, gamePlaySettings.NumCardsToGive);
         while (PlayerManager.Instance.CanPlayersPlayAnotherRound())
         {
            for (int i = 0; i < gamePlaySettings.PlayerCount; i++)
            {
               PlayerManager.Instance.GivePlayerPermissionToPlay(i);
               while (!step)//wait player
               {
                  yield return null;
               }

               step = false;
            }
         }
      }
      yield return null;
   }
}
