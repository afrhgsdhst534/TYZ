using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandOfFire : Spell
{
    CardChecker cc; BattleChecker bc;
    private void Awake()
    {
        cc = GameObject.FindGameObjectWithTag("CardChecker").GetComponent<CardChecker>();
        bc = GameObject.FindGameObjectWithTag("BattleChecker").GetComponent<BattleChecker>();
        path = "Attack/Pyromancer2";
        bc.onStartAttack += Abillity;
        money = 18;
        description = "When using a strike of less than 3 cards, permanently increases the damage of this ability by 5, 18";
    }
    int i;
    void Abillity()
    {
        i++;
        if (!active||cc.cards>2) return;

        bc.dopDamage += 5*i; trigger = true;

        StartCoroutine(Next());

    }
    IEnumerator Next()
    {
        yield return new WaitForSeconds(1);
        trigger = false;
    }

}
