using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reward : MonoBehaviour
{
    public GameObject closeBox;
    public GameObject openBox;

    public GameObject RewardCard;

    public GameObject fadeImage;

    int partnerORAttak;
    int index;
    public void OnCLickBox()
    {
        closeBox.SetActive(false);
        openBox.SetActive(true);
        StartCoroutine("UP");
    }

    public void GetCard()
    {
        partnerORAttak = Random.Range(0, 2);
        if (partnerORAttak == 0)
        {
            index = Random.Range(0, GameManager.gameManagerInstance.attackCards.Length);
            GameManager.gameManagerInstance.attackCards[index].curEXP++;
        }
        else
        {
            index = Random.Range(0, GameManager.gameManagerInstance.partnerCards.Length);
            GameManager.gameManagerInstance.partnerCards[index].curEXP++;
        }
        StartCoroutine("RotateCard");
    }

    // 카드 위로
    IEnumerator UP()
    {
        while (RewardCard.transform.position.y <= 550f)
        {
            RewardCard.transform.position += Vector3.up * 5;
            yield return new WaitForSeconds(0.01f);
        }
    }

    // 보상 카드 클릭시 회전후 보상 보여주기
    IEnumerator RotateCard()
    {
        while (RewardCard.transform.eulerAngles.y <= 90)
        {
            RewardCard.transform.eulerAngles += Vector3.up * 6;
            yield return new WaitForSeconds(0.001f);
        }
        if(partnerORAttak == 0)
            RewardCard.GetComponent<Image>().sprite = GameManager.gameManagerInstance.attackCards[index].card_Image;
        else
        RewardCard.GetComponent<Image>().sprite = GameManager.gameManagerInstance.partnerCards[index].card_Image;
        RewardCard.transform.eulerAngles = new Vector3(0, -90, 0);
        while (RewardCard.transform.eulerAngles.y >= 0.1f)
        {
            RewardCard.transform.eulerAngles += Vector3.up * 6;
            yield return new WaitForSeconds(0.001f);
        }
        RewardCard.transform.eulerAngles = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(2f);
        StartCoroutine(MoveMap(0, 1));
    }

    // Map 이동 페이드
    IEnumerator MoveMap(float start, float end)
    {
        Image fade = fadeImage.GetComponent<Image>();
        fadeImage.SetActive(true);
        float cur = 0.0f, per = 0.0f;
        while (per < 1.0f)
        {
            cur += Time.deltaTime;
            per = cur / 1.0f;

            Color color = fade.color;
            color.a = Mathf.Lerp(start, end, per);
            fade.color = color;

            yield return null;
        }

        yield return new WaitForSeconds(2f);
        SoundManager.soundInstance.SoundFade();
        LoadingSceneManager.LoadScene("InGameMap", 0);
    }
}
