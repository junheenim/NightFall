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

    // 동료를 담을 Queue
    Queue<int> q = new Queue<int>();

    // 체크 표시
    public GameObject[] check;
    public GameObject Select_Job;
    public Button startBut;

    // 동료 설명란
    public Text warriorInfo;
    public Text wizardInfo;
    public Text archerInfo;
    public Text bardInfo;
    public Text priestInfo;

    public FadeScreen fade;

    public Image fadeImage;

    private void Start()
    {
        // 카드 설명
        warriorInfo.text = "강력한 북방의 용병\n\n<color=red>" +
            GameManager.gameManagerInstance.partnerCards[0].damage.ToString() + "</color>물리 피해를 입힙니다.";

        wizardInfo.text = "수상한 비밀의 마도사\n\n<color=blue>" + 
            GameManager.gameManagerInstance.partnerCards[1].damage.ToString() + "</color>마법 피해를 입힙니다.";

        archerInfo.text = "밀림속 엘프숲의 파수꾼\n\n<color=red>" +
            GameManager.gameManagerInstance.partnerCards[2].damage.ToString() +
            "</color>물리 피해를 입히고,\n 코스트 <color=#FF1493>1</color>을 회복합니다.";

        bardInfo.text = "아름다운 선율의 음유시인\n\n<color=blue>2</color>마법 피해를 입히고\n체력을 <color=green>" +
            GameManager.gameManagerInstance.partnerCards[3].heal.ToString() + "</color>회복합니다.";

        priestInfo.text = "든든한 교단의 수호자\n\n<color=red>" +
            GameManager.gameManagerInstance.partnerCards[4].damage.ToString() +
            "</color>물리 피해를 입히고\n 체력을 <color=green>4</color>회복합니다.";

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
            // q에있는 내용과 n이 같으면 return 동료선택 중복방지
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
        // 동료 저장
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
