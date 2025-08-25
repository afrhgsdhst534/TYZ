using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
[RequireComponent(typeof(SpriteRenderer))]
public class Card : MonoBehaviour,IPointerClickHandler
{
    public Suit suit;
    public Rank rank;
    public SpriteRenderer sr;
    public bool isUp;
    private bool canDo;
    private bool canDo2;
    private Vector3 startPos;
    private Hand hand; EventSystem cur;
    [HideInInspector]
  public  CardAnimator ca;
    private void Start()
    {
        ca = GetComponent<CardAnimator>();
        canDo2 = true;
        sr = GetComponent<SpriteRenderer>();
        hand = GameObject.FindGameObjectWithTag("Hand").GetComponent<Hand>();
        cur = hand.cur;
        hand.onAnimEnd += OnAnimEnd;
    }
    void OnAnimEnd()
    {
        canDo = true;
        isUp = false;
        hand.cardsUp.Clear();
    }
    void ToggleCard()
    {
        StopAllCoroutines();
        Vector3 target = isUp ? startPos + Vector3.up * -1 : startPos + Vector3.up * 1;
        StartCoroutine(MoveTo(target, 0.25f));
        isUp = !isUp;
    }
    private void OnDestroy()
    {
        if (hand != null)
        {
            hand.cards.Remove(this);
            hand.cardsUp.Remove(this);
        }
    }

    IEnumerator MoveTo(Vector3 target, float time)
    {
        canDo2 = false;
        Vector3 from = transform.position;
        float t = 0;
        while (t < 1f)
        {
            t += Time.deltaTime / time;
            transform.position = Vector3.Lerp(from, target, Mathf.SmoothStep(0, 1, t));
            yield return null;
        }
        canDo2 = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!canDo || !canDo2 || !cur.enabled) return;
        startPos = transform.position;

        if (!isUp && hand.cardsUp.Count < 5)
        {
            hand.cardsUp.Add(this);
            ToggleCard();
        }
        else if (hand.cardsUp.Contains(this))
        {
            hand.cardsUp.Remove(this);
            ToggleCard();

        }
    }
}
public enum Suit
{
    Hearts,    // ♥
    Diamonds,  // ♦
    Clubs,     // ♣
    Spades     // ♠
}
public enum Rank
{
    Two = 2, Three, Four, Five, Six,
    Seven, Eight, Nine, Ten,
    Jack, Queen, King, Ace
}