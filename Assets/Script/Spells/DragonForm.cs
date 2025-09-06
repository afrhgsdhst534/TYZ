using System.Collections;
using UnityEngine;
public class DragonForm : Spell
{
    CardChecker cc; BattleChecker bc;
    private void Awake()
    {
        cc = GameObject.FindGameObjectWithTag("CardChecker").GetComponent<CardChecker>();
        bc = GameObject.FindGameObjectWithTag("BattleChecker").GetComponent<BattleChecker>();
        path = "Attack/Dragonknight18";
        bc.onStartAttack += Abillity;
        a = true;
        money = 18;
        description = "1 time per round, strengthens a combination of 4 cards or more, gives + 150 damage, cost 18";
    }
    bool a;
    void Abillity()
    {
        if (!a || !active|| cc.cards < 4) return;
        trigger = true;
        a = false;
        bc.dopDamage += 150;
        StartCoroutine(Next());
    }
    IEnumerator Next()
    {
        yield return new WaitForSeconds(1);
        trigger = false ;
    }
}
