using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="CardSO",menuName = "Serializable Object/CardSO")]
public class Card : ScriptableObject
{
    // 카드 코드
    public int cardCode;
    // 카드 이름
    public string cardname;
    // 카드 효과 설명
    [TextArea]
    public string cardInfo;
    // 카드 종류
    public int cardType;
    // 0 : 물리 공격
    // 1 : 물리 연타
    // 2 : 마법 공격
    // 3 : 마법 연타
    // 4 : 힐
    // 5 ~ : 동료 카드

    // 카드 레벨
    public int cardLevel = 1;
    // 카드 레벨업 경험치
    public int cardEXP = 1;
    // 카드 경험치
    public int curEXP = 0;
    // 카드 코스트
    public int cardCost;
    // 카드 데미지
    public int damage;
    // 카드 회복
    public int heal;
    // 카드 이미지
    public Sprite card_Image;
    // 동료 이미지
    public Sprite partner;

    // 카드 나올 확률
    public float percent;

    public void LevelUP()
    {
        // 카드 현재 경험치가 카드 레벨보다 크면
        if (cardEXP <= curEXP)
        {
            curEXP -= cardEXP;

            // 레벨업 필요 경험치 증가
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
