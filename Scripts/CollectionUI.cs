using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionUI : MonoBehaviour
{

    public AudioSource audioSource;
    public AudioClip click;
    private void Start()
    {
        gameObject.SetActive(false);
    }
    public void OnCLickReturn()
    {
        audioSource.clip = click;
        audioSource.Play();
        gameObject.SetActive(false);
    }
}
