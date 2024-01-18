using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    private bool isEmpty = true;
    private Card carrying;

    public Card Carrying
    {
        get => carrying;
        set => carrying = value;
    }

    public bool IsEmpty
    {
        get => isEmpty;
        set => isEmpty = value;
    }

    public void CarryNewItem(Card card)
    {
        isEmpty = false;
        carrying = card;
    }

    public void EmptySlot()
    {
        carrying = null;
        isEmpty = true;
    }
}
