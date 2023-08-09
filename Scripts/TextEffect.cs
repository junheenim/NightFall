using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextEffect : MonoBehaviour
{
    public float delay = 0.1f; // delay
    public string fullText; // full text
    private string currentText = ""; // now text

    void Start()
    {
        StartCoroutine(ShowText());
    }

    IEnumerator ShowText()
    {
        for (int i = 0; i < fullText.Length; i++)
        {
            currentText += fullText[i]; // one letter at a time
            this.GetComponent<Text>().text = currentText; // text update
            yield return new WaitForSeconds(delay); // delay after
        }
    }
}