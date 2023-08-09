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
    // ���ἱ�� UI
    public GameObject partner;

    public Button partnerBut;
    private void Start()
    {
        StartCoroutine("StartPage");
    }
    IEnumerator StartPage()
    {
        fade.StartFade(1, 0);
        // ������ ���� ���ἱ�� UI ���ֱ�
        partner.SetActive(false);

        // ���� ���
        SoundManager.soundInstance.PlayBGMAudio(1);

        // ���۽� ��� üũǥ�� ���ֱ�
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
        // ���ӸŴ����� �÷��̾� ���� ����
        GameManager.gameManagerInstance.myJob = (Player_Class)n;
        // �ٸ� üũ ǥ�� false
        for (int i = 0; i < 5; i++)
        {
            check[i].SetActive(false);
        }
        // �ڷ�ƾ �ݺ� �ߴ�
        StopAllCoroutines();
        // �ڷ�ƾ ����
        StartCoroutine(CheckEffect(check[n - 1]));
    }

    // ���ἱ�� UI �̵�
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
    // ���ư���
    public void OnClickReturn()
    {
        audioSource.clip = click;
        audioSource.Play();

        fade.StartFade(0, 1);
        SoundManager.soundInstance.StopBGM();
        LoadingSceneManager.LoadScene("MainMenuScene", 0);
        StartCoroutine(SoundManager.soundInstance.SoundFade());
    }

    // üũ ����Ʈ �߻�
    IEnumerator CheckEffect(GameObject c)
    {
        // ������ üũǥ�� true
        c.SetActive(true);
        float start = 0;

        while (start < 1.0f)
        {
            start += 0.05f;
            // 0 ~ 1���� start 0.01�� �� 0.05 ��ŭ ����
            c.GetComponent<Image>().fillAmount = Mathf.Lerp(0, 1f, start);
            yield return new WaitForSeconds(0.01f);
        }
    }
}

