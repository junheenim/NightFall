using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Character_Setting : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip click;

    public AudioSource checkAudio;
    public AudioClip clipCheck;

    public GameObject[] check;
    public FadeScreen fade;
    // 동료선택 UI
    public GameObject partner;

    public Button partnerBut;
    private void Start()
    {
        StartCoroutine("StartPage");
    }
    IEnumerator StartPage()
    {
        fade.StartFade(1, 0);
        // 순서를 위해 동료선택 UI 없애기
        partner.SetActive(false);

        // 사운드 재생
        SoundManager.soundInstance.PlayBGMAudio(1);

        // 시작시 모든 체크표시 없애기
        for (int i = 0; i < 5; i++)
        {
            check[i].SetActive(false);
        }
        yield return null;
    }
    public void SelectJop(int n)
    {
        checkAudio.clip = clipCheck;
        checkAudio.Play();

        partnerBut.interactable = true;
        // 게임매니저의 플레이어 직업 선택
        GameManager.gameManagerInstance.myJob = (Player_Class)n;
        // 다른 체크 표시 false
        for (int i = 0; i < 5; i++)
        {
            check[i].SetActive(false);
        }
        // 코루틴 반복 중단
        StopAllCoroutines();
        // 코루틴 시작
        StartCoroutine(CheckEffect(check[n - 1]));
    }

    // 동료선택 UI 이동
    public void OnClickPartner()
    {
        audioSource.clip = click;
        audioSource.Play();
        StartCoroutine("GOPrtnerUI");
    }

    IEnumerator GOPrtnerUI()
    {
        fade.StartFade(0, 1);
        partner.SetActive(true);
        gameObject.SetActive(false);
        fade.StartFade(1, 0);
        yield return null;
    }
    // 돌아가기
    public void OnClickReturn()
    {
        audioSource.clip = click;
        audioSource.Play();

        fade.StartFade(0, 1);
        SoundManager.soundInstance.StopBGM();
        LoadingSceneManager.LoadScene("MainMenuScene", 0);
        StartCoroutine(SoundManager.soundInstance.SoundFade());
    }

    // 체크 이펙트 발생
    IEnumerator CheckEffect(GameObject c)
    {
        // 선택한 체크표시 true
        c.SetActive(true);
        float start = 0;

        while (start < 1.0f)
        {
            start += 0.05f;
            // 0 ~ 1까지 start 0.01초 후 0.05 만큼 증가
            c.GetComponent<Image>().fillAmount = Mathf.Lerp(0, 1f, start);
            yield return new WaitForSeconds(0.01f);
        }
    }
}

