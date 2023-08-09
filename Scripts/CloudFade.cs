using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudFade : MonoBehaviour
{
    public GameObject Mountain_Cloud;
    public GameObject Dungeon_Cloud;
    public GameObject Spa_Cloud;
    public GameObject Castle_Cloud;

    public void CloudeActiveFalse()
    {
        if (GameManager.gameManagerInstance.stage4clear)
        {
            Castle_Cloud.SetActive(false);
            Mountain_Cloud.SetActive(false);
            Dungeon_Cloud.SetActive(false);
            Spa_Cloud.SetActive(false);
        }
        if (GameManager.gameManagerInstance.stage3clear)
        {
            Mountain_Cloud.SetActive(false);
            Dungeon_Cloud.SetActive(false);
            Spa_Cloud.SetActive(false);
        }
        else if (GameManager.gameManagerInstance.stage2clear)
        {
            Mountain_Cloud.SetActive(false);
        }
    }
    public void CloudRemove()
    {   
        if (GameManager.gameManagerInstance.stage3clear)
        {
            Mountain_Cloud.SetActive(false);
            Dungeon_Cloud.SetActive(false);
            Spa_Cloud.SetActive(false);
            StartCoroutine(Clude(Castle_Cloud, 1, 0));
        }
        else if (GameManager.gameManagerInstance.stage2clear)
        {
            Mountain_Cloud.SetActive(false);
            StartCoroutine(Clude(Dungeon_Cloud, 1, 0));
            StartCoroutine(Clude(Spa_Cloud, 1, 0));
        }
        else if (GameManager.gameManagerInstance.stage1clear)
        {
            StartCoroutine(Clude(Mountain_Cloud, 1, 0));
        }
    }

    IEnumerator Clude(GameObject gameobject, int start, int end)
    {
        SpriteRenderer sp = gameobject.GetComponent<SpriteRenderer>();
        float cur = 0.0f, per = 0.0f;
        while (per < 1.0f)
        {
            cur += Time.deltaTime;
            per = cur / 1.0f;

            Color color = sp.color;
            color.a = Mathf.Lerp(start, end, per);
            sp.color = color;

            yield return null;
        }
        gameobject.SetActive(false);
    }
}
