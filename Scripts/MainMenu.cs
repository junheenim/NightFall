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

    // 효과음 출력
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
        // 사운드 재생
        SoundManager.soundInstance.PlayBGMAudio(0);
    }
    public void OnClickGameStart()
    {
        sfxOut.clip = gameStart;
        sfxOut.Play();
        // 게임 시작시 게임 진행 수치 초기화
        GameManager.gameManagerInstance.Reset_Game();

        //게임시작 버튼 누를시 게임 씬 화면 로드
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
        // 수집품 버튼 수집품 UI Active True
        collection.SetActive(true);
    }

    public void OnClickOption()
    {
        sfxOut.clip = click;
        sfxOut.Play();
        // 옵션 버튼 옵션 UI Active True
        option.SetActive(true);
    }

    public void OnClickMadeBy()
    {
        sfxOut.clip = click;
        sfxOut.Play();
        // 옵션 버튼 옵션 UI Active True
        madeBy.SetActive(true);
    }

    public void OnClickQuit()
    {
        // 유니티 일시 에디터 Play false시키기
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // 게임 종료
        Application.Quit();
#endif
    }
}
