using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Hand : MonoBehaviour
{
    public List<Card> cards;
    public List<Card> cardsUp;
    public Card card;
    [Header("Настройки позиции")]
    public float cardSpacing;        // Расстояние между картами по X
    public float yPos;                 // Позиция по Y
    [Header("Анимация")]
    public float animationSpeed;
    [Header("Спавн карты")]
    public float spawnPosX;          // Откуда карты прилетают
    public System.Action onAnimEnd;
  public  EventSystem cur;
    private void Start()
    {
        cur = EventSystem.current;
    }
    public void Add(Suit suit, Rank rank, Sprite sprite)
    {
        Vector3 startPos = new Vector3(transform.position.x + spawnPosX, transform.position.y, transform.position.z);
        Card card = Instantiate(this.card, startPos, Quaternion.identity, transform);
        card.suit = suit;
        card.sr.sprite = sprite;
        card.rank = rank;
        AddCard(card);
    }
    public void AddCard(Card card)
    {
        card.transform.SetParent(transform, false);
        card.transform.localRotation = Quaternion.identity;
        cards.Add(card);
        UpdateHandPositions();
    }
    private IEnumerator MoveCard(Transform card, Vector3 targetPos)
    {
        cur.enabled = false;

        Vector3 startPos = card.localPosition;
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * animationSpeed;
            card.localPosition = Vector3.Lerp(startPos, targetPos, t);
            yield return null;
        }

        onAnimEnd?.Invoke();
        cur.enabled = true;

    }
    public void UpdateHandPositions()
    {
        int count = cards.Count;
        if (count == 0) return;
        for (int i = 0; i < count; i++)
        {
            float middle = (count - 1) / 2f;
            float x = (i - middle) * cardSpacing;
            Vector3 targetPos = new Vector3(x, yPos, 0);
            StartCoroutine(MoveCard(cards[i].transform, targetPos));
        }
    }
    public void RemoveCard(Card card)
    {
        cards.Remove(card);
        Destroy(card.gameObject);
        UpdateHandPositions();
    }
}
