using System.Collections.Generic;
using UnityEngine;
public class DealerMudak : MonoBehaviour
{
    public Sprite cardImage;
    private Hand hand;
    public CardPool pool;
    private void Start()
    {
        hand = GameObject.FindGameObjectWithTag("Hand").GetComponent<Hand>();
    }
    [ContextMenu("AddCard")]
    public void DealCard()
    {
        if (hand.cards.Count >= 10) return;
        if (pool.cards.Count <= 0) return;
        hand.Add(pool.cards[0]);
        pool.cards.Remove(pool.cards[0]);
    }
}