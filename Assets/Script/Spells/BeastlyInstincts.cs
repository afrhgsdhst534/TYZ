using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BeastlyInstincts : Spell
{
    BattleChecker bc;
    void Awake()
    {
        bc = GameObject.FindGameObjectWithTag("BattleChecker").GetComponent<BattleChecker>();
        path = "Attack/BeastMaster19";
        bc.onStartAttack += Abillity;
        money = 15;
        description = "Each subsequent attack deals 10 more damage until the end of the round, cost 15";
        bc.enemy.onKill += OnED;
    }
    int i;
    void OnED()
    {
        i = 0;
    }
    void Abillity()
    {
        if (!active) return;
        i++;
        int n = 10 * i;
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