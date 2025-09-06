using System.Collections;
using UnityEngine;
public class Aura : Spell
{
    BattleChecker bc;
    Rules rules;
    private void Awake()
    {
        bc = GameObject.FindGameObjectWithTag("BattleChecker").GetComponent<BattleChecker>();
        rules = GameObject.FindGameObjectWithTag("Rules").GetComponent<Rules>();
        path = "Money/Geomancer5";
        bc.onStartAttack += Abillity;
        money = 18;
        description = "increase damage by money,cost 18";
    }

    void Abillity()
    {
        if (!active) return;
        trigger = true;
        bc.dopDamage = rules.coins;
        StartCoroutine(Next());
    }
    IEnumerator Next()
    {
        yield return new WaitForSeconds(1);
        trigger = false;
    }
}
