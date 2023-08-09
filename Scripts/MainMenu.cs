using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject collection;
    public GameObject option;
    public GameObject madeBy;

    // ȿ���� ���
    AudioSource sfxOut;
    public AudioClip click;
    public AudioClip gameStart;
    // Fade
    public Image fadeImage;

    private void Awake()
    {
        sfxOut = gameObject.GetComponent<AudioSource>();
    }

    private void Start()
    {
        // ���� ���
        SoundManager.soundInstance.PlayBGMAudio(0);
    }
    public void OnClickGameStart()
    {
        sfxOut.clip = gameStart;
        sfxOut.Play();
        // ���� ���۽� ���� ���� ��ġ �ʱ�ȭ
        GameManager.gameManagerInstance.Reset_Game();

        //���ӽ��� ��ư ������ ���� �� ȭ�� �ε�
        StartCoroutine(SoundManager.soundInstance.SoundFade());
        StartCoroutine(Fade(0, 1));
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

        StopAllCoroutines();
        SoundManager.soundInstance.StopBGM();
        LoadingSceneManager.LoadScene("Character_Setting", 0);
    }


    public void OnClickCollection()
    {
        sfxOut.clip = click;
        sfxOut.Play();
        // ����ǰ ��ư ����ǰ UI Active True
        collection.SetActive(true);
    }

    public void OnClickOption()
    {
        sfxOut.clip = click;
        sfxOut.Play();
        // �ɼ� ��ư �ɼ� UI Active True
        option.SetActive(true);
    }

    public void OnClickMadeBy()
    {
        sfxOut.clip = click;
        sfxOut.Play();
        // �ɼ� ��ư �ɼ� UI Active True
        madeBy.SetActive(true);
    }

    public void OnClickQuit()
    {
        // ����Ƽ �Ͻ� ������ Play false��Ű��
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // ���� ����
        Application.Quit();
#endif
    }
}
