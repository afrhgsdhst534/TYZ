using System.Collections.Generic;
using UnityEngine;
public class DealerMudak : MonoBehaviour
{
    public Sprite cardImage; // ������ �� UI Image ��� ������� �����
    private HashSet<string> usedCards = new HashSet<string>(); // ��� �������� �����
    private System.Random rng = new System.Random();
    private Hand hand;
    private void Start()
    {
        hand = GameObject.FindGameObjectWithTag("Hand").GetComponent<Hand>();
        for (int i = 0; i < 10; i++)
        {
            DealCard();
        }
    }
    [ContextMenu("AddCard")]
    public void DealCard()
    {
        if (hand.cards.Count >= 10) return;
        if (usedCards.Count == 52)
        {
            // ��� ����� ����� � �������
            usedCards.Clear();
        }
        CardData newCard = GetRandomCard();
        if (newCard == null)
        {
            Debug.LogWarning("������ �����������!");
            return;
        }
        string resourceName = $"card-{newCard.suit.ToString()}-{newCard.rank.ToString()}";
        Sprite sprite = Resources.Load<Sprite>(resourceName);
        if (sprite == null)
        {
            Debug.LogError($"�� ������ ������: {resourceName}");
        }
        else
        {
            cardImage = sprite;
        }
        hand.Add(newCard.suit, newCard.rank,cardImage);
        Debug.Log($"������ �����: {newCard.rank} of {newCard.suit}");
    }
    private CardData GetRandomCard()
    {
        if (usedCards.Count == 52) return null;
        CardData chosen;
        string key;
        do
        {
            Suit suit = (Suit)rng.Next(0, 4);
            Rank rank = (Rank)rng.Next(2, 15); // ���� � ���� 13 ������
            chosen = new CardData(suit, rank);
            key = $"{suit}-{rank}";
        } while (usedCards.Contains(key)||hand.cards.Exists(c => c.suit == chosen.suit && c.rank == chosen.rank));
        usedCards.Add(key);
        return chosen;
    }
    private class CardData
    {
        public Suit suit;
        public Rank rank;
        public CardData(Suit suit, Rank rank)
        {
            this.suit = suit;
            this.rank = rank;
        }
    }
}