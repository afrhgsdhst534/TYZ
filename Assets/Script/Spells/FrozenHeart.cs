using System.Collections;
using UnityEngine;
public class FrozenHeart : Spell
{
    BattleChecker bc;
    private void Awake()
    {
        bc = GameObject.FindGameObjectWithTag("BattleChecker").GetComponent<BattleChecker>();
        path = "HP/Cryomancer4";
        bc.onStartAttack += Abillity;
        a = true;
        money = 18;
        description = "1 time per round, freezes the opponent for 1 turn, cost 18";
    }
    bool a;
    void Abillity()
    {
        if (!a || !active) return;
        trigger = true;
        bc.enemy.freeze=true;
        a = false;
        bc.dopDamage += 150;
        StartCoroutine(Next());
    }
    IEnumerator Next()
    {
        yield return new WaitForSeconds(1);
        trigger = false;
    }
}
