    using System.Collections.Generic;
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
        }
    }
