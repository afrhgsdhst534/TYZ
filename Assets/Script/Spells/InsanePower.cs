using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class InsanePower : Spell
{
    BattleChecker bc;
    Hand hand;
    void Awake()
    {
        bc = GameObject.FindGameObjectWithTag("BattleChecker").GetComponent<BattleChecker>();
        hand = GameObject.FindGameObjectWithTag("Hand").GetComponent<Hand>();
        path = "Other/Shaman1";
        bc.onStartAttack += Abillity;
        money = 18;
        description = "+25 damage for each card in your hand, cost 18";
    }
    

    void Abillity()
    {
        if (!active) return;
        int n = 25 * hand.cards.Count;
        bc.dopDamage += n;
        trigger = true;
        StartCoroutine(Next());

    }
    IEnumerator Next()
    {
        yield return new WaitForSeconds(1);
        trigger = false;
    }

}