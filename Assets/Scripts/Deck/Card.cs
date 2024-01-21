using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class Card : MonoBehaviour
{
    public SortingOrder Order { get; private set; } = SortingOrder.UnderCard;
    public CardNum Number { get; private set; }
    public Shape Shape { get; private set; }
    public int Points { get; set; }
    public CardColor Color { get; private set; }
    
    private SpriteRenderer spriteRenderer;
    private TMP_Text tmpText;
    private TextMeshPro tmProText;

    private void Awake()
    {
        InitializeComponents();
    }
    
    private void OnMouseUp()
    {
        DeckManager.Instance.PlayerInteractedWithCard(this,PlayerManager.Instance.GetPlayerIndex());
    }
    private void InitializeComponents()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        tmpText = GetComponentInChildren<TMP_Text>();
        tmProText = GetComponentInChildren<TextMeshPro>();
    }
    public void InitializeCard(int value, Shape shape, CardColor color, Sprite visual, string text)
    {
        AssignCardAttributes(value, shape, color);
        UpdateVisuals(visual, text);
    }
    private void AssignCardAttributes(int value, Shape shape, CardColor color)
    {
        Number = (CardNum)value;
        Shape = shape;
        Color = color;
    }
    
    private void UpdateVisuals(Sprite visual, string text)
    {
        spriteRenderer.sprite = visual;
        tmpText.SetText(text);
    }
    
    public void UpdateVisualSortingOrder(SortingOrder order)
    {
        Order = order;
        UpdateSortingOrder(spriteRenderer, order);
        UpdateSortingOrder(tmProText, order);
    }
    
    private void UpdateSortingOrder(Renderer renderer, SortingOrder order)
    {
        if (renderer != null)
            renderer.sortingOrder = (int)order;
    }
    
    private void UpdateSortingOrder(TextMeshPro tmPro, SortingOrder order)
    {
        if (tmPro != null)
            tmPro.sortingOrder = (int)order;
    }
}