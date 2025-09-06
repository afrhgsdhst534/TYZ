using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistOfRage : Spell
{
    CardChecker cc; BattleChecker bc;

    private void Awake()
    {
        cc = GameObject.FindGameObjectWithTag("CardChecker").GetComponent<CardChecker>();
        bc = GameObject.FindGameObjectWithTag("BattleChecker").GetComponent<BattleChecker>();

        path = "Attack/Brawler1";
        bc.onStartAttack += Abillity;
        money = 14;
        description = "+50 damage if you use no more than 3 cards in an attack, cost 14";
    }
    IEnumerator Next()
    {
        yield return new WaitForSeconds(1);
        trigger = false;
    }

    void Abillity()
    {
        if (cc.cards > 3 || !active) return;
        bc.dopDamage += 50;
        trigger = true;
        StartCoroutine(Next());

    }
}
