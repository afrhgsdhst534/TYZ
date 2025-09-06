using System.Collections;
using UnityEngine;
public class GoldenSnake : Spell
{
    BattleChecker bc;
    Rules rules;
    private void Awake()
    {
        bc = GameObject.FindGameObjectWithTag("BattleChecker").GetComponent<BattleChecker>();
        rules = GameObject.FindGameObjectWithTag("Rules").GetComponent<Rules>();
        path = "Money/DemonHunter8";
        bc.onStartAttack += Abillity;
        money = 12;
        description = "+3 coins if the attack is less than 3 cards, cost 12";
    }
    void Abillity()
    {
        if (!active || bc.cc.cards >3) return;
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
