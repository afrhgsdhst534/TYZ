using UnityEngine;
using TMPro;
using System;
public class EnumDrop : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public  Enemy enemy;
    void Start()
    {
        dropdown.ClearOptions();
        var names = Enum.GetNames(typeof(Enemys));
        dropdown.AddOptions(new System.Collections.Generic.List<string>(names));
        dropdown.onValueChanged.AddListener(OnDropdownChanged);
    }
    void OnDropdownChanged(int index)
    {
        Enemys selectedValue = (Enemys)index;
        enemy.enemys = selectedValue;
        enemy.EnemyChanger();
        Debug.Log("Выбран: " + selectedValue);
    }
}