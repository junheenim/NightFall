using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    // 현재위치 position
    Vector3 curPos;
    // 목표위치 position
    Vector3 mapPos;

    public GameObject stage1;
    public GameObject stage2;
    public GameObject spa;
    public GameObject stage3;
    public GameObject stage4;

    public CloudFade cloud;
    public BattleClear cross;
    public BattleMarker marker;
    void Start()
    {
        // 구름 표시
        cloud.CloudeActiveFalse();
        // X 표시
        cross.CrossSign();

        SoundManager.soundInstance.PlayBGMAudio(2);
        AllStageActiveFalse();
        // 카메라 위치 이동
        CameraSetting();
    }
    void CameraSetting()
    {
        // 카메라 이동
        if(GameManager.gameManagerInstance.stage4clear)
        {
            Camera.main.transform.position = new Vector3(0, 8f, -10f);
            return;
        }
        else if(GameManager.gameManagerInstance.stage3clear)
        {
            // 카메라 위치 고정
            Camera.main.transform.position = new Vector3(0, 1.5f, -10f);
            // 카메라 이동 코루틴
            StartCoroutine(StageClearCameraMove(8, stage4));
        }
        else if(GameManager.gameManagerInstance.stage2clear)
        {
            Camera.main.transform.position = new Vector3(0, -4f, -10f);
            StartCoroutine(StageClearCameraMove(1.5f, stage3, spa));
        }
        else if (GameManager.gameManagerInstance.stage1clear)
        {
            Camera.main.transform.position = new Vector3(0, -8.5f, -10f);
            StartCoroutine(StageClearCameraMove(-4f, stage2));
        }
        else
        {
            Camera.main.transform.position = new Vector3(0, -8.5f, -10f);
            // 목표위치 세팅
            mapPos = new Vector3(0, 8.5f, -10);
            // 카메라 현재위치 세팅
            curPos = Camera.main.transform.position;
            StartCoroutine("CameraMove");
        }
        
    }
    // 스테이지 클리어후 카메라 위치 변경
    IEnumerator StageClearCameraMove(float y,GameObject stage)
    {
        yield return new WaitForSeconds(2f);
        // y축 카메라 위치 변경
        while (Camera.main.transform.position.y <= y)
        {
            Camera.main.transform.position += Vector3.up * 0.2f;
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.5f);
        // 구름 제거 페이드
        cloud.CloudRemove();
        yield return new WaitForSeconds(1f);
        // 검 마크 생성
        marker.BattleMarkSetPosition();
        // 스테이지 엑티브 초기화
        AllStageActiveFalse();
        
        // 클릭 이벤트 생성
        StartCoroutine(CoillerActive(stage));
    }
    IEnumerator CoillerActive(GameObject stage)
    {
        yield return new WaitForSeconds(0.5f);
        stage.SetActive(true);
    }

    // 2단계 클리어 후 온천 여는 코루틴
    IEnumerator StageClearCameraMove(float y, GameObject stage1,GameObject stage2)
    {
        yield return new WaitForSeconds(2f);
        // y축 카메라 위치 변경
        while (Camera.main.transform.position.y <= y)
        {
            Camera.main.transform.position += Vector3.up * 0.2f;
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.5f);
        // 구름 제거 페이드
        cloud.CloudRemove();
        yield return new WaitForSeconds(1f);
        // 검 마크 생성
        marker.BattleMarkSetPosition();
        // 스테이지 엑티브 초기화
        AllStageActiveFalse();

        // 클릭 이벤트 생성
        StartCoroutine(CollderActive2(stage1, stage2));
    }

    IEnumerator CollderActive2(GameObject stage1, GameObject stage2)
    {
        yield return new WaitForSeconds(0.5f);
        stage1.SetActive(true);
        stage2.SetActive(true);
    }
    public void AllStageActiveFalse()
    {
        // 맵 클릭 이벤트 초기화
        stage1.SetActive(false);
        stage2.SetActive(false);
        stage3.SetActive(false);
        spa.SetActive(false);
        stage4.SetActive(false);
    }
    IEnumerator CameraMove()
    {
        // 처음 맵 입장시만
        yield return new WaitForSeconds(1f);
        // 카메라 목표위치까지 천천히 이동
        while (mapPos.y >= Camera.main.transform.position.y)
        {
            Camera.main.transform.position += Vector3.up * 0.2f;
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(1.5f);
        // 도착 확인 후 1초후 원래위치 이동
        while (curPos.y <= Camera.main.transform.position.y)
        {
            Camera.main.transform.position -= Vector3.up * 0.3f;
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.5f);
        marker.BattleMarkSetPosition();
        stage1.SetActive(true);
    }
}