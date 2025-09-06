using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class You : MonoBehaviour
{
    public TextMeshProUGUI attack;
    public TextMeshProUGUI hpUGUI;
    public Slider sl;
    public int hp;
    public CardsSkirt vfx;
    public Rules rules;
    private void Start()
    {
        hp += PlayerPrefs.GetInt("Health");
        sl.maxValue = hp;
        sl.value = hp;
        hpUGUI.text = hp.ToString();
    }
    public void TakeHeal(int heal)
    {
        hp += heal;
        if (sl.maxValue < hp) sl.maxValue = hp;
        hpUGUI.text = hp.ToString();
        sl.value = hp;   
    }
    public void TakeDamage(int damage)
    {
        hp -= damage;
        sl.value = hp;
        hpUGUI.text = hp.ToString();
        if (hp <= 0)
        {
            sl.value = 0;
            rules.OnYourDeath();
        }
    }
}
