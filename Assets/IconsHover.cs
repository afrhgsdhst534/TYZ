using UnityEngine;
using UnityEngine.EventSystems;

public class IconsHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private int index;
    public SpellManager sm;

    public void Init(SpellManager manager, int idx)
    {
        sm = manager;
        index = idx;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (index < sm.curSpells.Count)
        {
            Spell spell = sm.curSpells[index];
            sm.tooltip.Show(spell.description);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        sm.tooltip.Hide();
    }
}
