using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonsSpirit : Spell
{
    Rules rules;
    DealerMudak dm;
    private void Awake()
    {
        rules = GameObject.FindGameObjectWithTag("Rules").GetComponent<Rules>();
        dm = GameObject.FindGameObjectWithTag("DealerMudak").GetComponent<DealerMudak>();
        path = "Other/Dragonknight4";
        rules.onStartOfBattle += Abillity;
        money = 16;
        description = "+2 cards to your hand, cost 16";
    }
    void Abillity()
    {
        if (!active) return;
        for (int i = 0; i < 2; i++)
        {
            dm.DealCard();
        }
    }
}