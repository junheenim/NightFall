using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BattleCardToolTip : MonoBehaviour
{
    [SerializeField]
    Text cardName;
    [SerializeField]
    Text cardInfo;
    [SerializeField]
    Text jopAbility;
    [SerializeField]
    Text totalDamage;

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
        int damage = card.damage;
        int heal = card.heal;

        gameObject.SetActive(true);
        cardName.text = card.cardname + " lv" + card.cardLevel.ToString();

        // 직업 능력 설명
        switch (GameManager.gameManagerInstance.myJob)
        {
            case Player_Class.Warrior:
                jopAbility.text = "전  사 : 물리공격 + 2";
                if (card.cardType == 0 || card.cardType == 1)
                    damage += 2;
                break;
            case Player_Class.Wizard:
                jopAbility.text = "마법사 : 마법공격 + 2";
                if (card.cardType == 2 || card.cardType == 3)
                    damage += 2;
                break;
            case Player_Class.Archer:
                jopAbility.text = "궁  수 : 코스트 + 1";
                break;
            case Player_Class.Bard:
                jopAbility.text = "바  드 : 회복 + 1, 피해감소 + 1";
                if (card.cardType == 4)
                    heal += 1;
                break;
            case Player_Class.Priest:
                jopAbility.text = "사  제 : 물리공격 + 1, 마법공격 + 1";
                break;
        }

        // 카드 타입에 따른 내용과 총 대미지
        if (card.cardType == 0)
        {
            cardInfo.text = card.cardInfo + "\n물리 공격 : " + card.damage.ToString();
            totalDamage.text = "총 대미지 : " + damage.ToString();
        }
        else if (card.cardType == 1)
        {
            cardInfo.text = card.cardInfo + "\n물리 공격 : " + card.damage.ToString() + " X 2";
            totalDamage.text = "총 대미지 : " + damage.ToString() + " X 2";
        }            
        else if (card.cardType == 2)
        {
            cardInfo.text = card.cardInfo + "\n마법 공격 : " + card.damage.ToString();
            totalDamage.text = "총 대미지 : " + damage.ToString();
        }
        else if (card.cardType == 3)
        {
            cardInfo.text = card.cardInfo + "\n마법 공격 : " + card.damage.ToString() + " X 2";
            totalDamage.text = "총 대미지 : " + damage.ToString() + " X 2";
        }
        else if (card.cardType == 4)
        {
            cardInfo.text = card.cardInfo + "\n회복량 : " + card.heal.ToString();
            totalDamage.text = "총 회복량 : " + heal.ToString();
        }
        else if (card.cardType == 5)
        {
            cardInfo.text = card.cardInfo + "\n물리 공격 : " + card.damage.ToString();
        }
        else if (card.cardType == 6)
        {
            cardInfo.text = card.cardInfo + "\n마법 공격 : " + card.damage.ToString();
        }
        else if (card.cardType == 7)
        {
            cardInfo.text = card.cardInfo + "\n물리 공격 : " + card.damage.ToString() + ", 코스트 회복 : 1";
        }
        else if (card.cardType == 8)
        {
            cardInfo.text = card.cardInfo + "\n마법 공격 : 2,  체력 회복 : " + card.heal.ToString();
        }
        else if (card.cardType == 9)
        {
            cardInfo.text = card.cardInfo + "\n물리 공격 : " + card.damage.ToString() + ", 체력 회복 : 4";
        }
    }

    // 툴팁 숨기기
    public void HideToolTip()
    {
        gameObject.SetActive(false);
    }

}
