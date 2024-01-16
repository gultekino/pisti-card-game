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
   }
}
