using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ToolTip : MonoBehaviour
{
    // 카드 이름
    [SerializeField]
    Text cardName;

    // 카드 레벨
    [SerializeField]
    Text cardLevel;

    //카드 설명
    [SerializeField]
    Text cardDetail;

    //카드 능력
    [SerializeField]
    Text cardAbility;

    private void Start()
    {
        gameObject.SetActive(false);        
    }
    private void Update()
    {
        transform.position = Input.mousePosition;
    }
    // 툴팁 보기
    public void ShowToolTip(Card card)
    {
        gameObject.SetActive(true);

        cardLevel.text = "lv : " + card.cardLevel.ToString();
        cardName.text = card.cardname;
        cardDetail.text = card.cardInfo;

        // 카드 종류에 따른 다른 설명
        if (card.cardType == 0)
        {
            cardAbility.text = "물리 공격 : " + card.damage;
        }
        else if (card.cardType == 1)
        {
            cardAbility.text = "물리 공격 : " + card.damage + " X 2";
        }
        else if(card.cardType == 2)
        {
            cardAbility.text = "마법 공격 : " + card.damage;
        }
        else if(card.cardType == 3)
        {
            cardAbility.text = "마법 공격 : " + card.damage + " X 2";
        }
        else if(card.cardType == 4)
        {
            cardAbility.text = "회복 : " + card.heal;
        }

        // 동료 카드
        else if(card.cardType == 5)
        {
            cardAbility.text = "물리 공격 : " + card.damage.ToString();
        }
        else if (card.cardType == 6)
        {
            cardAbility.text = "마법 공격 : " + card.damage.ToString();
        }
        else if (card.cardType == 7)
        {
            cardAbility.text = "물리 공격 : " + card.damage.ToString() + "\n 코스트 회복 : 1 ";
        }
        else if (card.cardType == 8)
        {
            cardAbility.text = "마법 공격 : " + card.damage.ToString() + "\n체력 회복 : " + card.heal.ToString();
        }
        else if (card.cardType == 9)
        {
            cardAbility.text = "물리 공격 : " + card.damage.ToString() + "\n체력 회복 : " + card.heal.ToString();
        }
    }

    // 툴팁 숨기기
    public void HideToolTip()
    {
        gameObject.SetActive(false);
    }
}
