using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance => instance;
    private static PlayerManager instance;

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform[] playerSpawnLocs;
    [SerializeField] private Transform playerContainer;
    private List<GameObject> players = new List<GameObject>();
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
            players.Add(Instantiate(playerPrefab,pos,rot,playerContainer));
        }
    }

    public void GivePlayerPermissionToPlay(int playerIndex)
    {
        
    }
}
