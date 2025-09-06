using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingsArrow : Spell
{
    Rules rules;
    BattleChecker bc;
    private void Awake()
    {
        rules = GameObject.FindGameObjectWithTag("Rules").GetComponent<Rules>();
        bc = GameObject.FindGameObjectWithTag("BattleChecker").GetComponent<BattleChecker>();
        path = "Attack/Ranger7";
        bc.onStartAttack += Abillity;
        money = 12;
        description = "For each victory, it gives +5 damage, cost 12";
    }
    void Abillity()
    {
        if (!active) return;
        bc.dopDamage += rules.wins*5;
        trigger = true;
        StartCoroutine(Next());
    }
    IEnumerator Next()
    {
        yield return new WaitForSeconds(1);
        trigger = false;
    }
}
