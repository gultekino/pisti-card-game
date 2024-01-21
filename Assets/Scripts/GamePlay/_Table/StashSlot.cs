    using System.Collections.Generic;
    using System.Linq;
    using TMPro;
    using UnityEngine;

    public class StashSlot : MonoBehaviour
    {
        private List<Card> cardsInStashSlot = new();

        public List<Card> CardsInStashSlot => cardsInStashSlot;

        public void CarryNewCards(IEnumerable<Card> cards)
        {
            cardsInStashSlot.AddRange(cards);
            foreach (var c in cards)
            {
                c.transform.position = transform.position;
            }
            cards.LastOrDefault().UpdateVisualsSortingOrder(SortingOrder.UnderCard);
            cardsInStashSlot[0].UpdateVisualsSortingOrder(SortingOrder.UpperCard);
        }

        public void Clear()
        {
            cardsInStashSlot.Clear();
        }
    }
