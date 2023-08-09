using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Player_Class
{
    Null,
    Warrior,
    Wizard,
    Archer,
    Bard,
    Priest
}
public enum Partner_Class
{
    Null,
    Warrior,
    Wizard,
    Archer,
    Bard,
    Priest
}
public class GameManager : MonoBehaviour
{
    static public GameManager gameManagerInstance;

    public Player_Class myJob;
    public Partner_Class partner1;
    public Partner_Class partner2;

    // 플레이어 HP
    public float maxHP;
    public float curHP;

    // 플레이어가 가지고 있는 코스트
    public int playerTotalCost;

    // 스테이지 클리어 확인
    public bool stage1clear;
    public bool stage2clear;
    public bool stage3clear;
    public bool stage4clear;

    // 게임 카드 저장
    public Card[] attackCards;
    public Card[] partnerCards;

    // 객체 생성시 최소 실행(싱글톤 생성)
    private void Awake()
    {
        // 최초 생성시 soundInstance가 null 이라면
        if (gameManagerInstance == null && gameManagerInstance != this)
        {
            gameManagerInstance = this;
            // Scene이 넘어가도 object 파괴되지 않도록함
            DontDestroyOnLoad(gameObject);
        }
        // 아니면 없애기
        else
        {
            Destroy(gameObject);
        }
    }

    // 게임 초기화
    public void Reset_Game()
    {
        myJob = Player_Class.Null;
        partner1 = Partner_Class.Null;
        partner2 = Partner_Class.Null;

        maxHP = 100;
        curHP = maxHP;

        playerTotalCost = 4;

        stage1clear = false;
        stage2clear = false;
        stage3clear = false;
        stage4clear = false;
    }
    public void TakeDamage(int n)
    {
        if (myJob == Player_Class.Bard)
        {
            n--;
        }
        curHP -= n;
        if (curHP <= 0)
            curHP = 0;
    }

    public void Heal(int hp)
    {
        if(myJob==Player_Class.Bard)
        {
            hp += 2;
        }
        curHP += hp;
        if (curHP > maxHP)
        {
            curHP = maxHP;
        }
    }

}
