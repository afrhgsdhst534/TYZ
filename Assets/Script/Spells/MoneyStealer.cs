using System.Collections;
using UnityEngine;
public class MoneyStealer : Spell
{
    BattleChecker bc;
    Rules rules;
    private void Awake()
    {
        bc = GameObject.FindGameObjectWithTag("BattleChecker").GetComponent<BattleChecker>();
        rules = GameObject.FindGameObjectWithTag("Rules").GetComponent<Rules>();
        path = "Money/Medium15";
        bc.onStartAttack += Abillity;
        money = 12;
        description = "Steal 3 coins for an attack of 3 or more cards, cost 12";
    }
    void Abillity()
    {
        if (!active || bc.cc.cards < 3) return;
        trigger = true;
        rules.coins += 3;
        StartCoroutine(Next());
    }
    IEnumerator Next()
    {
        yield return new WaitForSeconds(1);
        trigger = false;
    }
}
