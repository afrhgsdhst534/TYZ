using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleChecker : MonoBehaviour
{
    CardChecker cc;

    private void Start()
    {
        cc = GameObject.FindGameObjectWithTag("CardChecker").GetComponent<CardChecker>();
        cc.onResult += AnimateAttack;
    }

    public void AnimateAttack()
    {
        StartCoroutine(AnimatePhase(cc.otherCards, true));
    }

    IEnumerator AnimatePhase(List<Card> cards, bool nextPhase)
    {
        // Запускаем анимацию на всех картах сразу
        foreach (var card in cards)
        {
            card.ca.StartCoroutine(card.ca.BlackoutCoroutine(0.5f));
        }

        yield return new WaitForSeconds(0.6f);

        // Уничтожаем все карты сразу
        foreach (var card in cards)
        {
            Destroy(card.gameObject);
        }
        cards.Clear();

        // Если это была фаза other → запускаем combo
        if (nextPhase)
            StartCoroutine(AnimatePhase(cc.comboCards, false));
    }

}
