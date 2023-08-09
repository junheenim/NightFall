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

    // �÷��̾� HP
    public float maxHP;
    public float curHP;

    // �÷��̾ ������ �ִ� �ڽ�Ʈ
    public int playerTotalCost;

    // �������� Ŭ���� Ȯ��
    public bool stage1clear;
    public bool stage2clear;
    public bool stage3clear;
    public bool stage4clear;

    // ���� ī�� ����
    public Card[] attackCards;
    public Card[] partnerCards;

    // ��ü ������ �ּ� ����(�̱��� ����)
    private void Awake()
    {
        // ���� ������ soundInstance�� null �̶��
        if (gameManagerInstance == null && gameManagerInstance != this)
        {
            gameManagerInstance = this;
            // Scene�� �Ѿ�� object �ı����� �ʵ�����
            DontDestroyOnLoad(gameObject);
        }
        // �ƴϸ� ���ֱ�
        else
        {
            Destroy(gameObject);
        }
    }

    // ���� �ʱ�ȭ
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
