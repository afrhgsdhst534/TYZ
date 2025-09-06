using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardChecker : MonoBehaviour
{
    public Hand hand;
    public List<Card> comboCards;
    public List<Card> otherCards;

    public void CheckCombination()
    {
        comboCards = new List<Card>();
        otherCards = new List<Card>();

        List<Card> selected = new List<Card>(hand.cardsUp);
        if (selected.Count == 0)
        {
            Debug.Log("Нет выбранных карт!");
            return;
        }
        selected = selected.OrderBy(c => (int)c.rank).ToList();
        var rankGroups = selected.GroupBy(c => c.rank).OrderByDescending(g => g.Count());
        var suitGroups = selected.GroupBy(c => c.suit).OrderByDescending(g => g.Count());
        if (IsRoyalFlush(selected)) { PrintResult("Royal Flush", 5000,5); return; }
        if (IsStraightFlush(selected)) { PrintResult("Straight Flush", 400,5); return; }
        if (IsFourOfKind(rankGroups)) { PrintResult("Four of a Kind", 300,4); return; }
        if (IsFullHouse(rankGroups)) { PrintResult("Full House", 220,5); return; }
        if (IsFlush(suitGroups)) { PrintResult("Flush", 110,5); return; }
        if (IsStraight(selected)) { PrintResult("Straight", 60,5); return; }
        if (IsThreeOfKind(rankGroups)) { PrintResult("Three of a Kind", 40,3); return; }
        if (IsTwoPair(rankGroups)) { PrintResult("Two Pair", 30,4); return; }
        if (IsOnePair(rankGroups)) { PrintResult("One Pair", 20,2); return; }
        Card high = selected.Last();
        comboCards.Add(high);
        otherCards = selected.Except(comboCards).ToList();
        int dmg = (int)high.rank <= 9 ? (int)high.rank : 10;
        PrintResult("High Card", dmg,1);
    }
    [HideInInspector]
    public int damage;
    public System.Action onResult;
    [HideInInspector]
    public int cards;
    private void PrintResult(string comboName, int damage,int cards)
    {
        Debug.Log("Комбинация: " + comboName + " | Damage: " + damage);
        this.cards = cards;
        this.damage = damage;
        string combo = string.Join(", ", comboCards.Select(c => $"{c.rank} {c.suit}"));
        string other = string.Join(", ", otherCards.Select(c => $"{c.rank} {c.suit}"));
        onResult?.Invoke();
        Debug.Log("Карты комбинации: " + combo);
        Debug.Log("Остальные карты: " + other);
    }
    private bool IsRoyalFlush(List<Card> cards)
    {
        if (IsStraightFlush(cards))
        {
            if (cards.Max(c => c.rank) == Rank.Ace)
            {
                comboCards = new List<Card>(cards);
                otherCards.Clear();
                return true;
            }
        }
        return false;
    }
    private bool IsStraightFlush(List<Card> cards)
    {
        if (IsFlush(cards.GroupBy(c => c.suit)) && IsStraight(cards))
        {
            comboCards = new List<Card>(cards);
            otherCards.Clear();
            return true;
        }
        return false;
    }

    private bool IsFourOfKind(IOrderedEnumerable<IGrouping<Rank, Card>> rankGroups)
    {
        var four = rankGroups.FirstOrDefault(g => g.Count() == 4);
        if (four != null)
        {
            comboCards = four.ToList();
            otherCards = rankGroups.SelectMany(g => g).Except(comboCards).ToList();
            return true;
        }
        return false;
    }

    private bool IsFullHouse(IOrderedEnumerable<IGrouping<Rank, Card>> rankGroups)
    {
        var three = rankGroups.FirstOrDefault(g => g.Count() == 3);
        var two = rankGroups.FirstOrDefault(g => g.Count() == 2);
        if (three != null && two != null)
        {
            comboCards = three.Concat(two).ToList();
            otherCards = rankGroups.SelectMany(g => g).Except(comboCards).ToList();
            return true;
        }
        return false;
    }

    private bool IsFlush(IEnumerable<IGrouping<Suit, Card>> suitGroups)
    {
        var flush = suitGroups.FirstOrDefault(g => g.Count() >= 5);
        if (flush != null)
        {
            comboCards = flush.Take(5).ToList();
            otherCards = suitGroups.SelectMany(g => g).Except(comboCards).ToList();
            return true;
        }
        return false;
    }

    private bool IsStraight(List<Card> cards)
    {
        var distinctRanks = cards.Select(c => (int)c.rank).Distinct().OrderBy(x => x).ToList();

        for (int i = 0; i <= distinctRanks.Count - 5; i++)
        {
            if (distinctRanks[i + 4] - distinctRanks[i] == 4)
            {
                comboCards = cards.Where(c => (int)c.rank >= distinctRanks[i] && (int)c.rank <= distinctRanks[i + 4]).ToList();
                otherCards = cards.Except(comboCards).ToList();
                return true;
            }
        }
        return false;
    }

    private bool IsThreeOfKind(IOrderedEnumerable<IGrouping<Rank, Card>> rankGroups)
    {
        var three = rankGroups.FirstOrDefault(g => g.Count() == 3);
        if (three != null)
        {
            comboCards = three.ToList();
            otherCards = rankGroups.SelectMany(g => g).Except(comboCards).ToList();
            return true;
        }
        return false;
    }

    private bool IsTwoPair(IOrderedEnumerable<IGrouping<Rank, Card>> rankGroups)
    {
        var pairs = rankGroups.Where(g => g.Count() == 2).Take(2).ToList();
        if (pairs.Count == 2)
        {
            comboCards = pairs.SelectMany(g => g).ToList();
            otherCards = rankGroups.SelectMany(g => g).Except(comboCards).ToList();
            return true;
        }
        return false;
    }

    private bool IsOnePair(IOrderedEnumerable<IGrouping<Rank, Card>> rankGroups)
    {
        var pair = rankGroups.FirstOrDefault(g => g.Count() == 2);
        if (pair != null)
        {
            comboCards = pair.ToList();
            otherCards = rankGroups.SelectMany(g => g).Except(comboCards).ToList();
            return true;
        }
        return false;
    }
}
