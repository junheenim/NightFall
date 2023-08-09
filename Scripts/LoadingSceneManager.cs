using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class LoadingSceneManager : MonoBehaviour
{
    // 로딩배경화면 이미지
    [SerializeField]
    private Sprite[] loadingBackImage;
    // 로딩 배경이미지
    [SerializeField]
    private Image backImage;

    // 로딩캐릭터 이미지
    [SerializeField]
    private Sprite[] loadingCharicterImage;
    //화면 이미지
    [SerializeField]
    private Image charicterImage;

    // 로딩 바
    [SerializeField]
    private Image loadingBar;

    // 이동 씬이름 저장
    public static string nextScene;
    // 스테이지에 따른 이미지 변경
    public static int stage;

    AsyncOperation op;
    private void Start()
    {
        StartCoroutine("LoadScene");
        // 스테이지에 따른 이미지 변경
        backImage.sprite = loadingBackImage[stage];

        // 랜덤한 숫자를 뽑아 캐릭터 그림으로 변경
        int n = Random.Range(0, loadingCharicterImage.Length);
        charicterImage.sprite = loadingCharicterImage[n];

        // 로딩화면 코루틴 시작
    }

    public static void LoadScene(string sceneName, int stageNum)
    {
        // 이동할 신
        nextScene = sceneName;
        // 로딩 백그라운드 사진
        stage = stageNum;
        SceneManager.LoadScene("LoadingScene");
    }

    IEnumerator LoadScene()
    {
        yield return null;
        // 씬을 불러오는 동안 일시중지 발생 하지 않는 로드 방식(비동기방식 로드)
        // LoadSceneAsync()로 로딩의 진행정도 받아오기
        op = SceneManager.LoadSceneAsync(nextScene);

        // 자동으로 신이동 막기
        op.allowSceneActivation = false;

        //로딩바 Lerp 변수
        float timer = 0f;

        // 작업완료 나타내는 프로퍼티
        while (!op.isDone)
        {
            yield return null;
            timer += Time.deltaTime / 10;
            // 로딩진행 로딩 완료시 0.9에서 끝남
            if (op.progress < 0.9f)
            {
                loadingBar.fillAmount = Mathf.Lerp(loadingBar.fillAmount, op.progress, timer);
                if (loadingBar.fillAmount >= op.progress)
                {
                    timer = 0f;
                }
            }
            else
            {
                // 페이크 로딩 1초후 씬전환
                loadingBar.fillAmount = Mathf.Lerp(loadingBar.fillAmount, 1.0f, timer);
                if (loadingBar.fillAmount >= 0.99f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}
