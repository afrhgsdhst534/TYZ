using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CardPool : MonoBehaviour
{
    public GameObject card;
    public HashSet<string> usedCards = new HashSet<string>(); // уже вышедшие карты
    private System.Random rng = new System.Random();
    public DealerMudak dm;
    public Sprite cardImage; // —сылка на UI Image дл€ визуала карты
    private Hand hand;
    public Enemy enemy;
    public List<Card> cards; 
    void Start()
    {
        hand = GameObject.FindGameObjectWithTag("Hand").GetComponent<Hand>();
        Next();
        enemy.onKill += RiseFromTheAshes;
    }

    public void RiseFromTheAshes()
    {

        // вернуть кладбище
        for (int i = 0; i < hand.graveyard.Count; i++)
        {
            hand.graveyard[i].gameObject.SetActive(true);
            hand.graveyard[i].sr.color = new Color(1f, 1f, 1f, 1f); // лучше нормализованные значени€, не 255
            hand.graveyard[i].transform.SetParent(transform);
            hand.graveyard[i].transform.localPosition = Vector3.zero;
            cards.Add(hand.graveyard[i]);
        }
        hand.graveyard.Clear();

        // вернуть карты из руки
        for (int i = 0; i < hand.cards.Count; i++)
        {
            hand.cards[i].transform.SetParent(transform);
            hand.cards[i].transform.localPosition = Vector3.zero;
            cards.Add(hand.cards[i]);
        }
        hand.cards.Clear();
        hand.cardsUp.Clear();

        // сброс использованных карт
        usedCards.Clear();

        ShuffleCards();
    }
    public void ShuffleCards()
    {
        for (int i = cards.Count - 1; i > 0; i--)
        {
            int j = rng.Next(i + 1);
            var temp = cards[i];
            cards[i] = cards[j];
            cards[j] = temp;
        }

        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].sr.sortingOrder = cards.Count - i;
        }
    }

    void Next()
    {
        int j = 53;
        for (int i = 0; i < 52; i++)
        {
            j--;
            CardData newCard = GetRandomCard();
            string resourceName = $"card-{newCard.suit.ToString()}-{newCard.rank.ToString()}";
            Sprite sprite = Resources.Load<Sprite>(resourceName);
            if (sprite == null)
            {
            }
            else
            {
                cardImage = sprite;
            }
            var h = Instantiate(card.gameObject, transform.position, Quaternion.identity, transform);
            Card c = h.GetComponent<Card>();
            c.sr.sortingOrder = j;
            c.suit = newCard.suit;
            c.rank = newCard.rank;
            c.sr.sprite = cardImage;
            cards.Add(c);
        }
    }
    private CardData GetRandomCard()
    {
        if (usedCards.Count == 52) return null;
        CardData chosen;
        string key;
        do
        {
            Suit suit = (Suit)rng.Next(0, 4);
            Rank rank = (Rank)rng.Next(2, 15);
            chosen = new CardData(suit, rank);
            key = $"{suit}-{rank}";
        } while (usedCards.Contains(key) || hand.cards.Exists(c => c.suit == chosen.suit && c.rank == chosen.rank));
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