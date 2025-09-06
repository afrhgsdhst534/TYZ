using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiercingGaze : Spell
{
    BattleChecker bc;
    private void Awake()
    {
        bc = GameObject.FindGameObjectWithTag("BattleChecker").GetComponent<BattleChecker>();
        path = "HP/Druid9";
        bc.onEnemyStartAttack += Abillity;
        money = 15;
        a = true;
        description = "1 time per round, you get HP = damage done by the next attack, cost 15";
    }
    bool a;
    void Abillity(int i)
    {
        if (!active || !a) return;
        a = false;
        bc.you.TakeHeal(bc.dopDamage + bc.cc.damage);
        trigger = true;
        StartCoroutine(Next());
    }
    IEnumerator Next()
    {
        yield return new WaitForSeconds(1);
        trigger = false;
    }
}