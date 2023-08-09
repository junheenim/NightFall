using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ending : MonoBehaviour
{
    public GameObject[] text;
    public Image fade;
    void Start()
    {
        StartCoroutine(EndingText());
    }

    IEnumerator EndingText()
    {
        for (int i = 0; i < text.Length; i++)
        {
            text[i].SetActive(true);
            yield return new WaitForSeconds(2f);
        }

        StartCoroutine(Fade(0, 1));
    }
    IEnumerator Fade(int start, int end)
    {
        float cur = 0.0f, per = 0.0f;
        while (per < 1.0f)
        {
            cur += Time.deltaTime;
            per = cur / 1.0f;

            Color color = fade.color;
            color.a = Mathf.Lerp(start, end, per);
            fade.color = color;

            yield return null;
        }

        LoadingSceneManager.LoadScene("MainMenuScene", 0);
    }
}
