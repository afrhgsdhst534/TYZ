using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectroShiel : Spell
{
    BattleChecker bc;
    Enemy enemy;
    private void Awake()
    {
        bc = GameObject.FindGameObjectWithTag("BattleChecker").GetComponent<BattleChecker>();
        path = "HP/Electromancer16";
        bc.enemy.onAttackEnd += Abillity;
        money = 20;
        description = "Reflects 25% of the damage received to the enemy, cost 20";
    }
    void Abillity()
    {
        if (!active) return;
        bc.you.TakeHeal(enemy.attack / 4);
        bc.enemy.TakeDamage(enemy.attack / 4);
    }
}
