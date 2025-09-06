using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class ShopButton : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    public SpellManager sm;
    public Spell spell;
    SpriteRenderer icon;
    public bool active;
    public Rules rules;
    public TextMeshProUGUI ugui;
    void Awake()
    {
        icon = GetComponent<SpriteRenderer>();
        active = true;
    }
    public void SetSpell(Spell s)
    {
        spell = s;
        active = true;
        icon.sprite = Resources.Load<Sprite>(s.path);
        icon.color = new(1, 1, 1);
    }
    public void Use()
    {
        if (!active||spell.money>rules.coins) return;
        if (spell != null)
        {
            rules.coins -= spell.money;
            sm.AddSpell(spell);
        }
        icon.color = new Color(0.5f, 0.5f, 0.5f, 100);
        active = false;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Use();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ugui.gameObject.SetActive(true);
        ugui.text = spell.description;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ugui.gameObject.SetActive(false);
    }
}