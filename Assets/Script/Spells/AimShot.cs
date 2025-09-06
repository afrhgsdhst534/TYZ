using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimShot : Spell
{
    CardChecker cc; BattleChecker bc;
    private void Awake()
    {
        cc = GameObject.FindGameObjectWithTag("CardChecker").GetComponent<CardChecker>();
        bc = GameObject.FindGameObjectWithTag("BattleChecker").GetComponent<BattleChecker>();
        path = "Attack/Hunter1";
        bc.onStartAttack += Abillity;
        money = 14;
        description = "+15 damage for each card in the attack, cost 14";
    }
    void Abillity()
    {
        if (!active) return;
        bc.dopDamage += 15*cc.cards;
        trigger = true;

        StartCoroutine(Next());

    }
    IEnumerator Next()
    {
        yield return new WaitForSeconds(1);
        trigger = false;
    }

}
