using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    private Card carrying;
    public bool IsEmpty { get; private set; } = true;
    public Card Carrying
    {
        get => carrying;
        private set
        {
            carrying = value;
            IsEmpty = carrying == null;
        }
    }

    public void CarryNewItem(Card card)
    {
        Carrying = card;
    }

    public void EmptySlot()
    {
        Carrying = null;
    }
}
