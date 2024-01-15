using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card
{
    private GameObject gameObject;
    private int value;
    private Shape shape;
    private int points;
    private CardColor color;

    public GameObject GameObject => gameObject;

    public int Value => value;

    public Shape Shape => shape;

    public int Points
    {
        get => points;
        set => points = value;
    }

    public CardColor Color => color;

    public Card(GameObject gameObject, int value, Shape shape, int points, CardColor color)
    {
        this.gameObject = gameObject;
        this.value = value;
        this.shape = shape;
        this.points = points;
        this.color = color;
    }
}
