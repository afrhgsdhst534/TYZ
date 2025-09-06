using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
public class RollSpells : MonoBehaviour, IPointerDownHandler
{
    public List<ShopButton> b; public SpellManager sm;
    System.Random rnd = new();
    void Start()
    {
        Use();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Use();
    }
    public void Use()
    {
        var avail = sm.allSpells.Except(sm.curSpells).ToList();
        var chosen = avail.OrderBy(x => rnd.Next()).Take(b.Count).ToList();
        for (int i = 0; i < b.Count; i++)
        {
            if (i < chosen.Count)
            {
                b[i].SetSpell(chosen[i]);
            }
            else
                b[i].SetSpell(null);
        }
    }
}