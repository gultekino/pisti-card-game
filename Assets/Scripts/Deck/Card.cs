using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Card : MonoBehaviour
{
    private CardNum number;
    private Shape shape;
    private SpriteRenderer spriteRenderer;
    private TMP_Text tmpText;
    
    public CardNum Number => number;

    public Shape Shape => shape;

    public int Points { get; set; }

    public CardColor Color { get; private set; }
    
    

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        tmpText = GetComponentInChildren<TMP_Text>();
    }

    public void CardInit(int value, Shape shape, CardColor color, Sprite visual, string text)
    {
        this.number = (CardNum) value;
        this.shape = shape;
        this.Points = Points;
        this.Color = color;
        
        spriteRenderer.sprite = visual;
        tmpText.SetText(text);
    }

    private int a = 0;
    private void OnMouseUp()
    {
        DeckManager.Instance.PlayerInteractedWithCard(this,a%2);
        a++;
    }
}