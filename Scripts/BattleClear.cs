using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleClear : MonoBehaviour
{
    public GameObject Battle1Clear;
    public GameObject Battle2Clear;
    public GameObject usedSpa;
    public GameObject Battle3Clear;
    public GameObject Battle4Clear;

    private void Start()
    {
        Battle1Clear.SetActive(false);
        Battle2Clear.SetActive(false);
        usedSpa.SetActive(false);
        Battle3Clear.SetActive(false);
        Battle4Clear.SetActive(false);
    }
    // 배틀후 엑스자 표시
    public void CrossSign()
    {
        if(GameManager.gameManagerInstance.stage4clear)
        {
            Battle1Clear.SetActive(true);
            Battle2Clear.SetActive(true);
            usedSpa.SetActive(true);
            Battle3Clear.SetActive(true);
            Battle4Clear.SetActive(true);
        }
        else if (GameManager.gameManagerInstance.stage3clear)
        {
            Battle1Clear.SetActive(true);
            Battle2Clear.SetActive(true);
            usedSpa.SetActive(true);
            Battle3Clear.SetActive(true);
        }
        else if (GameManager.gameManagerInstance.stage2clear)
        {
            Battle1Clear.SetActive(true);
            Battle2Clear.SetActive(true);
        }
        else if (GameManager.gameManagerInstance.stage1clear)
        {
            Battle1Clear.SetActive(true);
        }
    }
}
