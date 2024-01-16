using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Card : MonoBehaviour
{
    private int value;
    private Shape shape;
    private SpriteRenderer spriteRenderer;
    private TMP_Text tmpText;
    
    public int Value => value;

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
        this.value = value;
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