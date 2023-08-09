using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUI : MonoBehaviour
{
    AudioSource audioSource;
    AudioClip click;
    public GameObject ending;
    private void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }
    private void Start()
    {
        if(GameManager.gameManagerInstance.stage4clear)
        {
            StartCoroutine(Ending());
        }
    }
    public GameObject Option;
    public void ReturnMainMenu()
    {
        LoadingSceneManager.LoadScene("MainMenuScene", 0);
    }

    public void OnCLickOption()
    {
        audioSource.clip = click;
        audioSource.Play();
        Option.SetActive(true);
    }

    IEnumerator Ending()
    {
        yield return new WaitForSeconds(1f);
        ending.SetActive(true);
    }
}
