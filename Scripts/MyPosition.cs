using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPosition : MonoBehaviour
{
    // 맵 화살표 로컬 좌표
    // start  :  0,       -14
    // stage1 : -6.5,     -8.5
    // stage2 :  6,       0
    // Spa    : -7.5,     1.5
    // stage3 : -7.5,     8 
    // stage4 :  7,       17

    public bool move;
    bool up = true;
    public Vector3 position;

    private void Start()
    {
        SettingPos();
    }
    void Update()
    {
        if (move)
        {
            MovePoint();
        }
    }

    // 씬로드시 화살표 위치 설정
    public void SettingPos()
    {
        if(GameManager.gameManagerInstance.stage4clear)
        {
            gameObject.transform.localPosition = new Vector3(7f, 17, -1);
        }
        else if (GameManager.gameManagerInstance.stage3clear)
        {
            gameObject.transform.localPosition = new Vector3(-7.5f, 8, -1);
        }
        else if (GameManager.gameManagerInstance.stage2clear)
        {
            gameObject.transform.localPosition = new Vector3(6f, 0, -1);
        }
        else if (GameManager.gameManagerInstance.stage1clear)
        {
            gameObject.transform.localPosition = new Vector3(-6.5f, -8.5f, -1);
        }
        else
        {
            gameObject.transform.localPosition = new Vector3(0, -14, -1);
        }
        move = true;
        position = gameObject.transform.position;
    }

    public void MovePoint()
    {
        if (up)
        {
            gameObject.transform.position += Vector3.up * 0.01f;
            if (transform.position.y >= position.y + 0.3)
                up = false;
        }
        else
        {
            gameObject.transform.position += Vector3.down * 0.01f;
            if (transform.position.y <= position.y)
                up = true;
        }
    }
}
