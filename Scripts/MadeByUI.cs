using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MadeByUI : MonoBehaviour
{
    public AudioSource audio;
    public AudioClip click;
    private void Start()
    {
        gameObject.SetActive(false);
    }
    public void OnCLickReturn()
    {
        audio.clip = click;
        audio.Play();
        gameObject.SetActive(false);
    }
}
