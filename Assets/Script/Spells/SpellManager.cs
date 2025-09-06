using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SpellManager : MonoBehaviour
{
    public List<Image> icons;
    public List<Spell> curSpells = new List<Spell>();
    public List<Spell> allSpells = new List<Spell>();
    public ToolTip tooltip;

    public void AddSpell(Spell spell)
    {
        if (curSpells.Count >= icons.Count) return;
        curSpells.Add(spell);
        int index = curSpells.Count - 1;
        icons[index].gameObject.SetActive(true);
        icons[index].sprite = Resources.Load<Sprite>(spell.path);
        spell.active = true;
    }
    public void RemoveSpell(Spell spell)
    {
        int index = curSpells.IndexOf(spell);
        if (index == -1) return; // такого спелла нет
        spell.active = false;
        curSpells.RemoveAt(index);
        for (int i = index; i < curSpells.Count; i++)
        {
            icons[i].sprite = Resources.Load<Sprite>(curSpells[i].path);
        }
        icons[curSpells.Count].gameObject.SetActive(false);
    }
    public void AddRandomSpell()
    {
        if (curSpells.Count >= icons.Count) return; // нет места

        List<Spell> available = new List<Spell>();
        foreach (var s in allSpells)
        {
            if (!curSpells.Contains(s)) available.Add(s);
        }

        if (available.Count == 0) return; // все уже добавлены

        int rand = Random.Range(0, available.Count);
        AddSpell(available[rand]);
    }

}