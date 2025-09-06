using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class BattleChecker : MonoBehaviour
{
    public CardChecker cc;
   public You you;
    public Enemy enemy;
   public SpellManager sm;
    public Rules rules;
    private void Start()
    {
        cc.onResult += AnimateAttack;
        saveDamage = PlayerPrefs.GetInt("Damage");
    }

    public void AnimateAttack()
    {
        StartCoroutine(AnimatePhase(cc.otherCards, true));
    }

    IEnumerator AnimatePhase(List<Card> cards, bool nextPhase)
    {
        foreach (var card in cards)
        {
            card.ca.StartCoroutine(card.ca.BlackoutCoroutine(0.5f));
        }
        yield return new WaitForSeconds(0.6f);
        foreach (var card in cards)
        {
            card.gameObject.SetActive(false);
        }
        cards.Clear();
        if (nextPhase)
            StartCoroutine(AnimatePhase1(cc.comboCards, false));
    }
    IEnumerator AnimatePhase1(List<Card> cards, bool nextPhase)
    {
        foreach (var card in cards)
        {
            card.ca.StartCoroutine(card.ca.GreenCoroutine(0.5f));
        }
        yield return new WaitForSeconds(0.6f);
        foreach (var card in cards)
        {
            card.gameObject.SetActive(false);
        }
        cards.Clear();
        StartCoroutine(AnimateDamage());
        if (nextPhase)
            StartCoroutine(AnimatePhase(cc.comboCards, false));
    }
    public Action onStartAttack;
    public Action<int> onEnemyStartAttack;
    public Action onAttackEnd;
    public int dopDamage;
    public GameObject attackObj;
    public DealerMudak dm;
    public Hand hand;
    public int saveDamage;
    public int vfxDamage;
    IEnumerator AnimateDamage()
    {
        yield return new WaitForSeconds(0.5f);
        dopDamage = 0;
        you.attack.text = (cc.damage + saveDamage).ToString();
        onStartAttack?.Invoke();
        for (int i = 0; i < Mathf.Min(sm.icons.Count, sm.curSpells.Count); i++)
        {
            if (sm.curSpells[i].active)
            {
                sm.icons[i].color = new Color(0, 1, 0);
            }
        }
        yield return new WaitForSeconds(0.5f);
        you.attack.text = you.attack.text +" + "+ dopDamage.ToString();
        you.GetComponent<Animator>().SetTrigger("atk");
        yield return new WaitForSeconds(0.5f);
        you.vfx.Play(cc.damage + saveDamage);
        yield return new WaitForSeconds(0.5f);
        onEnemyStartAttack?.Invoke(cc.damage+ saveDamage + dopDamage+ vfxDamage);
        yield return new WaitForSeconds(1f);
        vfxDamage = 0;
        if (rules.deathScreen.activeInHierarchy)yield break;
        enemy.Attack();
        for (int i = 0; i < Mathf.Min(sm.icons.Count, sm.curSpells.Count); i++)
        {
            sm.icons[i].color = Color.white;
        }
        yield return new WaitForSeconds(1f);
        attackObj.SetActive(true);
        you.attack.text = "";
        enemy.freeze = false;
        onAttackEnd?.Invoke();
        if (enemy.isKilled) yield break;
        if (hand.cards.Count > 6)
        {
            dm.DealCard();
            dm.DealCard();
            dm.DealCard();
        }
        else
        {
            dm.DealCard();
            dm.DealCard();
            dm.DealCard();
            dm.DealCard();
            dm.DealCard();
        }
    }
}
