using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState
{
    Start,
    PlayerTurn,
    EnemyTurn,
    Won,
    Lost
}

public class Stage1Battle : MonoBehaviour
{
    // 배경
    public Image rewaldbackground;
    public SpriteRenderer background;
    public Sprite forest;
    public Sprite mountain;
    public Sprite dungeon;
    public Sprite castle;

    // 프리펩 적용;
    public GameObject player;

    public GameObject wolf;
    public GameObject bear;
    public GameObject rich;
    public GameObject devil;

    // 대화창
    public TalkManager talkManager;

    // 공격 사용 가져오기
    public Unit enemy;
    public Player playerInfo;
    public UseCard useCard;

    // 프리펩 위치 세팅
    public Transform playerPosition;
    public Transform enemyPosition;

    // 버튼 엑티브
    public Button[] buttons;

    // 게임 오버 이미지 페이드
    public GameObject gameOver;

    // 턴
    public BattleState state;

    // 현재 스테이지
    public int stage;

    private void Start()
    {
        state = BattleState.Start;
        SetupBattle();
        SoundManager.soundInstance.PlayBGMAudio(stage + 2);
    }

    void SetupBattle()
    {   
        if(GameManager.gameManagerInstance.stage4clear)
        {
            return;
        }
        if (GameManager.gameManagerInstance.stage3clear)
        {
            background.sprite = castle;
            rewaldbackground.sprite = castle;
            stage = 4;
        }
        else if (GameManager.gameManagerInstance.stage2clear)
        {
            background.sprite = dungeon;
            rewaldbackground.sprite = dungeon;
            stage = 3;
        }
        else if (GameManager.gameManagerInstance.stage1clear)
        {
            background.sprite = mountain;
            rewaldbackground.sprite = mountain;
            stage = 2;
        }
        else
        {
            background.sprite = forest;
            rewaldbackground.sprite = forest;
            stage = 1;
        }

        // 플레이어 프리펩 셍성
        GameObject playerOBJ =  Instantiate(player, playerPosition);
        playerInfo = playerOBJ.GetComponent<Player>();

        GameObject enemyOBJ;
        switch (stage)
        {
            case 1:
                // 적 프리펩 생성
                enemyOBJ = Instantiate(wolf, enemyPosition);
                // 적 정보
                enemy = enemyOBJ.GetComponentInChildren<Wolf>();
                break;
            case 2:
                // 적 프리펩 생성
                enemyOBJ = Instantiate(bear, enemyPosition);
                // 적 정보
                enemy = enemyOBJ.GetComponentInChildren<Bear>();
                break;
            case 3:
                // 적 프리펩 생성
                enemyOBJ = Instantiate(rich, enemyPosition);
                // 적 정보
                enemy = enemyOBJ.GetComponentInChildren<Rich>();
                break;
            case 4:
                // 적 프리펩 생성
                enemyOBJ = Instantiate(devil, enemyPosition);
                // 적 정보
                enemy = enemyOBJ.GetComponentInChildren<Devil>();
                break;
        }

        // 대화창 실행
        talkManager.StartTalk(stage);
        state = BattleState.PlayerTurn;
    }

    public void  PlayerTurn()
    {
        if (GameManager.gameManagerInstance.curHP <= 0)
        {
            StartCoroutine(GameOver(0, 1));
            return;
        }
        useCard.CardDrow();
    }

    public void EnemyTurn()
    {
        enemy.isAttack = true;
        if (!enemy.Attack(stage))
        {
            state = BattleState.PlayerTurn;
        }
        else
        {
            StartCoroutine(GameOver(0, 1));
        }
    }

    // 게임 오버시
    IEnumerator GameOver(float start, float end)
    {
        Image fade = gameOver.GetComponent<Image>();
        // GameOver 이미지 페이드
        gameOver.SetActive(true);
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

        // 2초후 메인메뉴 이동
        yield return new WaitForSeconds(2f);
        SoundManager.soundInstance.StopBGM();
        LoadingSceneManager.LoadScene("MainMenuScene", 0);
    }

    
}
