using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingsSword : Spell
{
    BattleChecker bc;
    Rules rules;
    private void Awake()
    {
        bc = GameObject.FindGameObjectWithTag("BattleChecker").GetComponent<BattleChecker>();
        rules = GameObject.FindGameObjectWithTag("Rules").GetComponent<Rules>();
        path = "Money/Paladin10";
        rules.onStartOfBattle += Abillity;
        money = 12;
        description = "For each victory + 3 coins, cost 12";
    }
    void Abillity()
    {
        if (!active) return;
        rules.coins +=rules.wins*3;
    }
}