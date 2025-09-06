using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class Activator : MonoBehaviour
{
    public GameObject obj;
    public TextMeshProUGUI ugui;
    public Rules rules;
    public void Activate()
    {
        obj.SetActive(!obj.activeInHierarchy);
        ugui.text = "money = " + rules.coins;
    }
    public void Zero()
    {
        SceneManager.LoadScene(0);
    }
}
