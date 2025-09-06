using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class ZeroScene : MonoBehaviour
{
    public TextMeshProUGUI money;
    public TextMeshProUGUI damage;
    public TextMeshProUGUI health;
    public TextMeshProUGUI points;
    int m, d, h, p;
    void Start()
    {
        m = PlayerPrefs.GetInt("Money", 0);
        d = PlayerPrefs.GetInt("Damage", 0);
        h = PlayerPrefs.GetInt("Health", 0);
        p = PlayerPrefs.GetInt("Points", 0);
        UpdateUI();
    }
    void UpdateUI()
    {
        money.text = "Money: "+ m.ToString();
        damage.text ="Damage: "+ d.ToString();
        health.text ="Health: "+ h.ToString();
        points.text ="Points: "+ p.ToString();
    }
    public void NextScene(int str)
    {
        PlayerPrefs.SetInt("VFX",str);
        SaveData();
        SceneManager.LoadScene(1);
    }
    void SaveData()
    {
        PlayerPrefs.SetInt("Money", m);
        PlayerPrefs.SetInt("Damage", d);
        PlayerPrefs.SetInt("Health", h);
        PlayerPrefs.SetInt("Points", p);
        PlayerPrefs.Save();
        UpdateUI();
    }
    public void Damage()
    {
        if (p <= 0) return;
        p--;
        d += 5;
        SaveData();
    }
    public void Health()
    {
        if (p <= 0) return;
        p--;
        h += 10;
        SaveData();
    }
    public void Money()
    {
        if (p <= 0) return;
        p--;
        m += 3;
        SaveData();
    }
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBGL
        Application.ExternalEval("window.close();");
#else
        Application.Quit();
#endif
    }
}
