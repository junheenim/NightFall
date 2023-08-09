using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="CardSO",menuName = "Serializable Object/CardSO")]
public class Card : ScriptableObject
{
    // ī�� �ڵ�
    public int cardCode;
    // ī�� �̸�
    public string cardname;
    // ī�� ȿ�� ����
    [TextArea]
    public string cardInfo;
    // ī�� ����
    public int cardType;
    // 0 : ���� ����
    // 1 : ���� ��Ÿ
    // 2 : ���� ����
    // 3 : ���� ��Ÿ
    // 4 : ��
    // 5 ~ : ���� ī��

    // ī�� ����
    public int cardLevel = 1;
    // ī�� ������ ����ġ
    public int cardEXP = 1;
    // ī�� ����ġ
    public int curEXP = 0;
    // ī�� �ڽ�Ʈ
    public int cardCost;
    // ī�� ������
    public int damage;
    // ī�� ȸ��
    public int heal;
    // ī�� �̹���
    public Sprite card_Image;
    // ���� �̹���
    public Sprite partner;

    // ī�� ���� Ȯ��
    public float percent;

    public void LevelUP()
    {
        // ī�� ���� ����ġ�� ī�� �������� ũ��
        if (cardEXP <= curEXP)
        {
            curEXP -= cardEXP;

            // ������ �ʿ� ����ġ ����
            cardEXP++;
            cardLevel++;

            if (cardType == 4 || cardType == 8)
            {
                heal++;
            }
            else
            {
                damage++;
            }
        }
    }

}
