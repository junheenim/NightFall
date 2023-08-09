using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMarker : MonoBehaviour
{
    // stage1 :    0, 0
    // stage2 : 12.5, 8
    // stage3 :   -1, 16.5
    // stage4 : 13.5, 23.5

    public GameObject sword1;
    public GameObject sword2;
    public GameObject effectpos;
    public GameObject hitEffect;

    public AudioSource audioSource;
    public AudioClip move;
    public AudioClip moveComplet;
    private void Start()
    {
        sword1.transform.eulerAngles = new Vector3(0, 0, 50);
        sword2.transform.eulerAngles = new Vector3(0, 0, 310);
    }
    public void BattleMarkSetPosition()
    {
        if(GameManager.gameManagerInstance.stage4clear)
        {
            return;
        }
        gameObject.SetActive(true);
        if (GameManager.gameManagerInstance.stage3clear)
        {
            gameObject.transform.localPosition = new Vector3(13.5f, 23.5f, -5);
        }
        else if(GameManager.gameManagerInstance.stage2clear)
        {
            gameObject.transform.localPosition = new Vector3(-1f, 16.5f, -5);
        }
        else if(GameManager.gameManagerInstance.stage1clear)
        {
            gameObject.transform.localPosition = new Vector3(12.5f, 8f, -5);
        }
        else
        {
            gameObject.transform.localPosition = new Vector3(0, 0, -5);
        }
    }
    public void ConfirmedBattle(string sceneName,int bgmNum)
    {
        StartCoroutine(sword1Rotation(sceneName, bgmNum));
    }

    IEnumerator sword1Rotation(string sceneName, int bgmNum)
    {
        audioSource.clip = move;
        audioSource.Play();
        while (sword1.transform.rotation.z <= 0.45f)
        {
            // 오른쪽으로 회전
            sword1.transform.eulerAngles += Vector3.back * 4;
            // 왼쪽으로 회전
            sword2.transform.eulerAngles += Vector3.forward * 4;
            yield return new WaitForSeconds(0.01f);
        }
        audioSource.clip = moveComplet;
        audioSource.Play();
        // 이펙트 생성 및 플레이
        Instantiate(hitEffect, effectpos.transform);
        yield return new WaitForSeconds(0.5f);

        gameObject.SetActive(false);
        // 씬 로드(페이드 기능 추가)
        LoadingSceneManager.LoadScene(sceneName, bgmNum);
    }
}
