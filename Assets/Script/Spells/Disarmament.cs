using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disarment : Spell
{
    BattleChecker bc;
    Enemy enemy;
    private void Awake()
    {
        bc = GameObject.FindGameObjectWithTag("BattleChecker").GetComponent<BattleChecker>();
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();
        path = "HP/Berserker10";
        enemy.onAttackEnd += Abillity;
        money = 15;
        description ="If you survive the damage, you gain 50% of the damage you receive, cost 15";
    }
    void Abillity()
    {
        if (!active) return;
        bc.you.TakeHeal(enemy.attack/2);
    }
}
