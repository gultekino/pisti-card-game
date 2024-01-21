using UnityEngine;

[System.Serializable]
public class TableLocationHandler : IPlayerLocationHandler, ITableLocationHandler
{
    [SerializeField] private Transform[] playerLocationsOnTable;
    [SerializeField] private Transform deckLocation;
    [SerializeField] private Transform centerLocation;

    public Transform GetPlayerLocation(int index) => playerLocationsOnTable[index];
    public Transform GetDeckLocation() => deckLocation;
    public Transform GetCenterLocation() => centerLocation;
}