using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingsSoul : Spell
{
    BattleChecker bc;
    Rules rules;
    private void Awake()
    {
        bc = GameObject.FindGameObjectWithTag("BattleChecker").GetComponent<BattleChecker>();
        rules = GameObject.FindGameObjectWithTag("Rules").GetComponent<Rules>();
        path = "HP/Necromancer13";
        rules.onStartOfBattle += Abillity;
        money = 18;
        description = "For each victory +15 HP, cost 18";
    }
    void Abillity()
    {
        if (!active) return;
        bc.you.TakeHeal(rules.wins*15);
    }
}