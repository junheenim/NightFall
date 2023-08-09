using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class LoadingSceneManager : MonoBehaviour
{
    // �ε����ȭ�� �̹���
    [SerializeField]
    private Sprite[] loadingBackImage;
    // �ε� ����̹���
    [SerializeField]
    private Image backImage;

    // �ε�ĳ���� �̹���
    [SerializeField]
    private Sprite[] loadingCharicterImage;
    //ȭ�� �̹���
    [SerializeField]
    private Image charicterImage;

    // �ε� ��
    [SerializeField]
    private Image loadingBar;

    // �̵� ���̸� ����
    public static string nextScene;
    // ���������� ���� �̹��� ����
    public static int stage;

    AsyncOperation op;
    private void Start()
    {
        StartCoroutine("LoadScene");
        // ���������� ���� �̹��� ����
        backImage.sprite = loadingBackImage[stage];

        // ������ ���ڸ� �̾� ĳ���� �׸����� ����
        int n = Random.Range(0, loadingCharicterImage.Length);
        charicterImage.sprite = loadingCharicterImage[n];

        // �ε�ȭ�� �ڷ�ƾ ����
    }

    public static void LoadScene(string sceneName, int stageNum)
    {
        // �̵��� ��
        nextScene = sceneName;
        // �ε� ��׶��� ����
        stage = stageNum;
        SceneManager.LoadScene("LoadingScene");
    }

    IEnumerator LoadScene()
    {
        yield return null;
        // ���� �ҷ����� ���� �Ͻ����� �߻� ���� �ʴ� �ε� ���(�񵿱��� �ε�)
        // LoadSceneAsync()�� �ε��� �������� �޾ƿ���
        op = SceneManager.LoadSceneAsync(nextScene);

        // �ڵ����� ���̵� ����
        op.allowSceneActivation = false;

        //�ε��� Lerp ����
        float timer = 0f;

        // �۾��Ϸ� ��Ÿ���� ������Ƽ
        while (!op.isDone)
        {
            yield return null;
            timer += Time.deltaTime / 10;
            // �ε����� �ε� �Ϸ�� 0.9���� ����
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
                // ����ũ �ε� 1���� ����ȯ
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
