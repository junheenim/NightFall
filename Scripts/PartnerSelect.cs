using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartnerSelect : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip click;
    public AudioClip start;

    public AudioSource checkAudio;
    public AudioClip clipCheck;

    // ���Ḧ ���� Queue
    Queue<int> q = new Queue<int>();

    // üũ ǥ��
    public GameObject[] check;
    public GameObject Select_Job;
    public Button startBut;

    // ���� �����
    public Text warriorInfo;
    public Text wizardInfo;
    public Text archerInfo;
    public Text bardInfo;
    public Text priestInfo;

    public FadeScreen fade;

    public Image fadeImage;

    private void Start()
    {
        // ī�� ����
        warriorInfo.text = "������ �Ϲ��� �뺴\n\n<color=red>" +
            GameManager.gameManagerInstance.partnerCards[0].damage.ToString() + "</color>���� ���ظ� �����ϴ�.";

        wizardInfo.text = "������ ����� ������\n\n<color=blue>" + 
            GameManager.gameManagerInstance.partnerCards[1].damage.ToString() + "</color>���� ���ظ� �����ϴ�.";

        archerInfo.text = "�и��� �������� �ļ���\n\n<color=red>" +
            GameManager.gameManagerInstance.partnerCards[2].damage.ToString() +
            "</color>���� ���ظ� ������,\n �ڽ�Ʈ <color=#FF1493>1</color>�� ȸ���մϴ�.";

        bardInfo.text = "�Ƹ��ٿ� ������ ��������\n\n<color=blue>2</color>���� ���ظ� ������\nü���� <color=green>" +
            GameManager.gameManagerInstance.partnerCards[3].heal.ToString() + "</color>ȸ���մϴ�.";

        priestInfo.text = "����� ������ ��ȣ��\n\n<color=red>" +
            GameManager.gameManagerInstance.partnerCards[4].damage.ToString() +
            "</color>���� ���ظ� ������\n ü���� <color=green>4</color>ȸ���մϴ�.";

        StartCoroutine("StartPage");
    }

    IEnumerator StartPage()
    {
        fade.StartFade(1, 0);
        for (int i = 0; i < 5; i++)
        {
            check[i].SetActive(false);
        }
        yield return null;
    }

    public void SelectPatner(int n)
    {
        foreach(int i in q)
        {
            // q���ִ� ����� n�� ������ return ���ἱ�� �ߺ�����
            if (i == n)
                return;
        }

        audioSource.clip = clipCheck;
        audioSource.Play();
        check[n - 1].SetActive(true);
        StartCoroutine(CheckEffect(check[n - 1]));
        q.Enqueue(n);
        if (q.Count >= 2)
        {
            startBut.interactable = true;
        }
        if (q.Count > 2)
        {
            check[q.Dequeue() - 1].SetActive(false);
        }
    }
    public void GameStart()
    {
        if (q.Count == 0)
            return;

        audioSource.clip = start;
        audioSource.Play();
        // ���� ����
        GameManager.gameManagerInstance.partner1 = (Partner_Class)q.Dequeue();
        GameManager.gameManagerInstance.partner2 = (Partner_Class)q.Dequeue();

        StartCoroutine(SoundManager.soundInstance.SoundFade());
        StartCoroutine(Fade(0, 1));
    }
    
    public void OnCLickReturn()
    {
        StartCoroutine("GOReturn");
    }

    IEnumerator GOReturn()
    {
        audioSource.clip = click;
        audioSource.Play();

        fade.StartFade(0, 1);
        Select_Job.SetActive(true);
        gameObject.SetActive(false);
        fade.StartFade(1, 0);
        yield return null;
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
        LoadingSceneManager.LoadScene("InGameMap", 0);
    }
}
