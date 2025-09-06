using System.Collections;
using UnityEngine;
public class ArchideaBloom : Spell
{
    BattleChecker bc;
    private void Awake()
    {
        bc = GameObject.FindGameObjectWithTag("BattleChecker").GetComponent<BattleChecker>();
        path = "HP/Druid2";
        bc.onStartAttack += Abillity;
        money = 12;
        description = "Each attack of ≥3 cards gives +15 HP, cost 12";
    }
    void Abillity()
    {
        if (!active||bc.cc.cards<3) return;
        trigger = true;
        bc.you.TakeHeal(15);
        StartCoroutine(Next());
    }
    IEnumerator Next()
    {
        yield return new WaitForSeconds(1);
        trigger = false;
    }
}
