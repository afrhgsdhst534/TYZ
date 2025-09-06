using TMPro;
using UnityEngine;

public class ToolTip : MonoBehaviour
{
    public TextMeshProUGUI text;
    public GameObject panel;
    private void Awake()
    {
        Hide();
    }

    public void Show(string description)
    {
        panel.SetActive(true);
        text.text = description;
    }

    public void Hide()
    {
        panel.SetActive(false);
    }
}
