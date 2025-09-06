using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HailOfBlades : Spell
{
    CardChecker cc; BattleChecker bc;
    Enemy enemy;
    private void Awake()
    {
        cc = GameObject.FindGameObjectWithTag("CardChecker").GetComponent<CardChecker>();
        bc = GameObject.FindGameObjectWithTag("BattleChecker").GetComponent<BattleChecker>();
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();
        path = "Attack/Enchanter2";
        bc.onStartAttack += Abillity;
        a = true;
        money = 19;
        description = "doubles the damage of your first attack as well as the damage of your opponent, cost 19";
    }
    bool a;
    void Abillity()
    {
        if (!a || !active) return;
        a = false; trigger = true;

        bc.dopDamage += cc.damage;
        enemy.attack += cc.damage;
        StartCoroutine(Next());

    }
    IEnumerator Next()
    {
        yield return new WaitForSeconds(1);
        trigger = false;
    }

}
