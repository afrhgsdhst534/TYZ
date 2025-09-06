using System.Collections;
using UnityEngine;

public class CardAnimator : MonoBehaviour
{
    SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public IEnumerator BlackoutCoroutine(float duration)
    {
        Color startColor = sr.color;
        Color endColor = Color.black;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            sr.color = Color.Lerp(startColor, endColor, t);
            yield return null;
        }

        t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            sr.color = new Color(0, 0, 0, 1 - t);
            yield return null;
        }
    }

    public IEnumerator GreenCoroutine(float duration)
    {
        Color startColor = sr.color;
        Color endColor = Color.green;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            sr.color = Color.Lerp(startColor, endColor, t);
            yield return null;
        }

        t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            sr.color = new Color(0, 255, 0, 1 - t); // зелёный → исчезновение
            yield return null;
        }
    }
}
