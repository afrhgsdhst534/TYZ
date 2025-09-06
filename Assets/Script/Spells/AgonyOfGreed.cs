using System.Collections;
using UnityEngine;
public class AgonyOfGreed : Spell
{
    BattleChecker bc;
    Rules rules;
    private void Awake()
    {
        bc = GameObject.FindGameObjectWithTag("BattleChecker").GetComponent<BattleChecker>();
        rules = GameObject.FindGameObjectWithTag("Rules").GetComponent<Rules>();
        path = "Attack/DemonHunter20";
        bc.onStartAttack += Abillity;
        bc.enemy.onAttackEnd += OnElse;
        money = 12;
        description = "+3 coins when attacking, -6 when losing HP, cost 12";
    }
    void OnElse()
    {
        if (!active) return;
        rules.coins -= 6;
    }
    void Abillity()
    {
        if (!active) return;
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
