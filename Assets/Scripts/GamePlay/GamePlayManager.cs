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
      DeckManager.Instance.GivePlayersCard(gamePlaySettings.PlayerCount, gamePlaySettings.NumCardsToGive);
      StartCoroutine(GameLoop());
   }

   private IEnumerator GameLoop()
   {
      yield break;
      while (DeckManager.Instance.DoesPackHasCardsForAnotherRound
                (gamePlaySettings.PlayerCount,gamePlaySettings.NumCardsToGive))
      {
         //give cards to the players
         while (PlayerManager.Instance.CanPlayersPlayAnotherRound())
         {
            //make player play
            for (int i = 0; i < gamePlaySettings.PlayerCount; i++)
            {
               PlayerManager.Instance.GivePlayerPermissionToPlay(i);
               while (true)//wait player
               {
                  yield return null;

                  //wait until player plays
               }
            }
         }
      }
      yield return null;
   }
}
