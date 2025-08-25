using UnityEngine;
using UnityEngine.UI;
using YG; // пространство имён Yandex Games SDK
using System.Collections;
public class Test : MonoBehaviour
{
    public Image i;
    public void Start()
    {
        YG2.RewardedAdvShow("",Asd);
        StartCoroutine(N());
    }
    IEnumerator N()
    {
        yield return new WaitForSeconds(61);
        YG2.InterstitialAdvShow();

    }
    void Asd()
    {
        i.color = Color.green;
    }
}
