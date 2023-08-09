using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FadeScreen : MonoBehaviour
{
    public Image fadeImage;
    public void StartFade(int a, int b)
    {
        StartCoroutine(Fade(a, b));
    }

    IEnumerator Fade(float start, float end)
    {
        float cur = 0.0f, per = 0.0f;
        while (per < 1.0f)
        {
            cur += Time.deltaTime;
            per = cur / 1.0f;

            Color color = fadeImage.color;
            color.a = Mathf.Lerp(start, end, per);
            fadeImage.color = color;

            yield return null;
        }
    }
}
