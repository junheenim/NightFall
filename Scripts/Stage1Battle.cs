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
    // ���
    public Image rewaldbackground;
    public SpriteRenderer background;
    public Sprite forest;
    public Sprite mountain;
    public Sprite dungeon;
    public Sprite castle;

    // ������ ����;
    public GameObject player;

    public GameObject wolf;
    public GameObject bear;
    public GameObject rich;
    public GameObject devil;

    // ��ȭâ
    public TalkManager talkManager;

    // ���� ��� ��������
    public Unit enemy;
    public Player playerInfo;
    public UseCard useCard;

    // ������ ��ġ ����
    public Transform playerPosition;
    public Transform enemyPosition;

    // ��ư ��Ƽ��
    public Button[] buttons;

    // ���� ���� �̹��� ���̵�
    public GameObject gameOver;

    // ��
    public BattleState state;

    // ���� ��������
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

        // �÷��̾� ������ �ļ�
        GameObject playerOBJ =  Instantiate(player, playerPosition);
        playerInfo = playerOBJ.GetComponent<Player>();

        GameObject enemyOBJ;
        switch (stage)
        {
            case 1:
                // �� ������ ����
                enemyOBJ = Instantiate(wolf, enemyPosition);
                // �� ����
                enemy = enemyOBJ.GetComponentInChildren<Wolf>();
                break;
            case 2:
                // �� ������ ����
                enemyOBJ = Instantiate(bear, enemyPosition);
                // �� ����
                enemy = enemyOBJ.GetComponentInChildren<Bear>();
                break;
            case 3:
                // �� ������ ����
                enemyOBJ = Instantiate(rich, enemyPosition);
                // �� ����
                enemy = enemyOBJ.GetComponentInChildren<Rich>();
                break;
            case 4:
                // �� ������ ����
                enemyOBJ = Instantiate(devil, enemyPosition);
                // �� ����
                enemy = enemyOBJ.GetComponentInChildren<Devil>();
                break;
        }

        // ��ȭâ ����
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

    // ���� ������
    IEnumerator GameOver(float start, float end)
    {
        Image fade = gameOver.GetComponent<Image>();
        // GameOver �̹��� ���̵�
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

        // 2���� ���θ޴� �̵�
        yield return new WaitForSeconds(2f);
        SoundManager.soundInstance.StopBGM();
        LoadingSceneManager.LoadScene("MainMenuScene", 0);
    }

    
}
