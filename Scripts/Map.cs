using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    // ������ġ position
    Vector3 curPos;
    // ��ǥ��ġ position
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
        // ���� ǥ��
        cloud.CloudeActiveFalse();
        // X ǥ��
        cross.CrossSign();

        SoundManager.soundInstance.PlayBGMAudio(2);
        AllStageActiveFalse();
        // ī�޶� ��ġ �̵�
        CameraSetting();
    }
    void CameraSetting()
    {
        // ī�޶� �̵�
        if(GameManager.gameManagerInstance.stage4clear)
        {
            Camera.main.transform.position = new Vector3(0, 8f, -10f);
            return;
        }
        else if(GameManager.gameManagerInstance.stage3clear)
        {
            // ī�޶� ��ġ ����
            Camera.main.transform.position = new Vector3(0, 1.5f, -10f);
            // ī�޶� �̵� �ڷ�ƾ
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
            // ��ǥ��ġ ����
            mapPos = new Vector3(0, 8.5f, -10);
            // ī�޶� ������ġ ����
            curPos = Camera.main.transform.position;
            StartCoroutine("CameraMove");
        }
        
    }
    // �������� Ŭ������ ī�޶� ��ġ ����
    IEnumerator StageClearCameraMove(float y,GameObject stage)
    {
        yield return new WaitForSeconds(2f);
        // y�� ī�޶� ��ġ ����
        while (Camera.main.transform.position.y <= y)
        {
            Camera.main.transform.position += Vector3.up * 0.2f;
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.5f);
        // ���� ���� ���̵�
        cloud.CloudRemove();
        yield return new WaitForSeconds(1f);
        // �� ��ũ ����
        marker.BattleMarkSetPosition();
        // �������� ��Ƽ�� �ʱ�ȭ
        AllStageActiveFalse();
        
        // Ŭ�� �̺�Ʈ ����
        StartCoroutine(CoillerActive(stage));
    }
    IEnumerator CoillerActive(GameObject stage)
    {
        yield return new WaitForSeconds(0.5f);
        stage.SetActive(true);
    }

    // 2�ܰ� Ŭ���� �� ��õ ���� �ڷ�ƾ
    IEnumerator StageClearCameraMove(float y, GameObject stage1,GameObject stage2)
    {
        yield return new WaitForSeconds(2f);
        // y�� ī�޶� ��ġ ����
        while (Camera.main.transform.position.y <= y)
        {
            Camera.main.transform.position += Vector3.up * 0.2f;
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.5f);
        // ���� ���� ���̵�
        cloud.CloudRemove();
        yield return new WaitForSeconds(1f);
        // �� ��ũ ����
        marker.BattleMarkSetPosition();
        // �������� ��Ƽ�� �ʱ�ȭ
        AllStageActiveFalse();

        // Ŭ�� �̺�Ʈ ����
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
        // �� Ŭ�� �̺�Ʈ �ʱ�ȭ
        stage1.SetActive(false);
        stage2.SetActive(false);
        stage3.SetActive(false);
        spa.SetActive(false);
        stage4.SetActive(false);
    }
    IEnumerator CameraMove()
    {
        // ó�� �� ����ø�
        yield return new WaitForSeconds(1f);
        // ī�޶� ��ǥ��ġ���� õõ�� �̵�
        while (mapPos.y >= Camera.main.transform.position.y)
        {
            Camera.main.transform.position += Vector3.up * 0.2f;
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(1.5f);
        // ���� Ȯ�� �� 1���� ������ġ �̵�
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