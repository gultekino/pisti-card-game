using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table
{
    private readonly List<Card> cardsOnTable = new();

    public List<Card> CardsOnTable => cardsOnTable;
}
