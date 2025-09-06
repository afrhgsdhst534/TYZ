using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingsBook : Spell
{
    Rules rules;
    DealerMudak dm;
    private void Awake()
    {
        rules = GameObject.FindGameObjectWithTag("Rules").GetComponent<Rules>();
        dm = GameObject.FindGameObjectWithTag("DealerMudak").GetComponent<DealerMudak>();
        path = "Other/Enchanter18";
        rules.onStartOfBattle += Abillity;
        money = 18;
        description = "For every 4 wins +1 to hand size, cost 18";
    }
    void Abillity()
    {
        if (!active) return;
        int j = rules.wins / 4;
        if (j == 0) return;
        for (int i = 0; i < j; i++)
        {
            dm.DealCard();
        }
    }
}