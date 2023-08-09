using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StageSetting : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip move;

    bool click;
    bool moveArrow;
    public GameObject PositionArrow;
    MyPosition mypos;
    public GameObject marker;

    public GameObject stage1;
    public GameObject stage2;
    public GameObject spa;
    public GameObject stage3;
    public GameObject stage4;

    public GameObject spaUsed;
    public Image backImage;
    public Text spaText;
    public GameObject spaCrossMark;

    public PlayerHP plyerHP;
    // LocalPosition 목표 좌표 등록
    Vector3 movePos;

    Vector3 stage1Pos =  new Vector3(-6.5f, -8.5f, -1);
    Vector3 stage2Pos = new Vector3(6f, 0f, -1);
    Vector3 spaPos = new Vector3(-7.5f, 1.5f, -1);
    Vector3 stage3Pos = new Vector3(-7.5f, 8f, -1);
    Vector3 stage4Pos = new Vector3(7f, 17f, -1);

    RaycastHit2D hit;

    int moveStage;
    private void Start()
    {
        click = true;
        moveArrow = false;
        // 게임시작 동시에 클릭 방지용
        SetStage();
    }
    
    private void Update()
    {
        // 마우스 클릭 정보 받기
        if (Input.GetMouseButtonDown(0) && click)
        {
            // UI가 켜져있을때는 클릭 방지
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                InputClick();
            }

        }
        
        if (moveArrow)
        {
            // 위아래 움직이는거 막기
            mypos.move = false;
            // 화살표 위치 이동
            PositionArrow.transform.localPosition = Vector3.Lerp(PositionArrow.transform.localPosition, movePos, Time.deltaTime * 2);
            // 화살표 위치 와 목표 지점의 거리가 0.1 이하면
            if (Vector3.Distance(PositionArrow.transform.localPosition, movePos) <= 0.1f)
            {
                moveArrow = false;
                // 다시 위아래로 움직이게 하기
                mypos.move = true;
                // 현재위치 저장
                mypos.position = PositionArrow.transform.position;
                // 검 회전
                if (hit.collider.tag != "Spa")
                {
                    marker.GetComponent<BattleMarker>().ConfirmedBattle("Stage1Battle", moveStage);
                }
                else
                {
                    // 마우스 클릭 다시 원상태로 돌리기
                    click = true;
                    // 온천 씬 이용시 끝
                    StartCoroutine(Fade(0, 1));
                    GameManager.gameManagerInstance.curHP = GameManager.gameManagerInstance.maxHP;
                    GameManager.gameManagerInstance.playerTotalCost++;
                    plyerHP.SetHP();
                    spa.SetActive(false);
                    spaCrossMark.SetActive(true);
                }
            }
        }
    }
    void SetStage()
    {
        if (GameManager.gameManagerInstance.stage4clear)
        {
            stage1.SetActive(false);
            stage2.SetActive(false);
            spa.SetActive(false);
            stage3.SetActive(false);
            stage4.SetActive(false);
        }
        else if (GameManager.gameManagerInstance.stage3clear)
        {
            stage1.SetActive(false);
            stage2.SetActive(false);
            spa.SetActive(false);
            stage3.SetActive(false);
            stage4.SetActive(true);
        }
        else if (GameManager.gameManagerInstance.stage2clear)
        {
            stage1.SetActive(false);
            stage2.SetActive(false);
            spa.SetActive(true);
            stage3.SetActive(true);
            stage4.SetActive(false);
        }
        else if (GameManager.gameManagerInstance.stage1clear)
        {
            stage1.SetActive(false);
            stage2.SetActive(true);
            spa.SetActive(false);
            stage3.SetActive(false);
            stage4.SetActive(false);
        }
        else
        {
            stage1.SetActive(true);
            stage2.SetActive(false);
            spa.SetActive(false);
            stage3.SetActive(false);
            stage4.SetActive(false);
        }
    }
    public void InputClick()
    {
        // 마우스 클릭 좌표
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // 해당 좌표 오브젝트 확인 hit에 담기
        hit = Physics2D.Raycast(pos, Vector2.zero, 0f);

        // hit이 null이 아니라면
        if (hit.collider != null)
        {
            mypos = PositionArrow.GetComponent<MyPosition>();
            // 클릭한 콜라이더 태그에 따라서 화살표  위치 조정
            if (hit.collider.tag=="Stage1")
            {
                movePos = stage1Pos;
                moveStage = 1;
            }
            else if (hit.collider.tag == "Stage2")
            {
                movePos = stage2Pos;
                moveStage = 2;
            }
            else if (hit.collider.tag == "Spa")
            {
                movePos = spaPos;
                moveStage = 3;
            }
            else if (hit.collider.tag == "Stage3")
            {
                movePos = stage3Pos;
                moveStage = 3;
            }
            else if (hit.collider.tag == "Stage4")
            {
                movePos = stage4Pos;
                moveStage = 4;
            }

            audioSource.clip = move;
            audioSource.Play();

            moveArrow = true;
            click = false;
        }
    }

    IEnumerator Fade(float start, float end)
    {
        spaUsed.SetActive(true);
        float cur = 0.0f, per = 0.0f;
        while (per < 1.0f)
        {
            cur += Time.deltaTime;
            per = cur / 1.0f;

            Color color1 = backImage.color;
            Color color2 = spaText.color;
            color1.a = Mathf.Lerp(start, end, per);
            color2.a = Mathf.Lerp(start, end, per);
            backImage.color = color1;
            spaText.color = color2;
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        cur = 0.0f;
        per = 0.0f;
        while (per < 1.0f)
        {
            cur += Time.deltaTime;
            per = cur / 1.0f;

            Color color1 = backImage.color;
            Color color2 = spaText.color;
            color1.a = Mathf.Lerp(end, start, per);
            color2.a = Mathf.Lerp(end, start, per);
            backImage.color = color1;
            spaText.color = color2;
            yield return null;
        }
        spaUsed.SetActive(false);
    }
}
