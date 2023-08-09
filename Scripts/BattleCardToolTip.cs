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

    // ���� ����
    public void ShowToolTip(Card card)
    {
        int damage = card.damage;
        int heal = card.heal;

        gameObject.SetActive(true);
        cardName.text = card.cardname + " lv" + card.cardLevel.ToString();

        // ���� �ɷ� ����
        switch (GameManager.gameManagerInstance.myJob)
        {
            case Player_Class.Warrior:
                jopAbility.text = "��  �� : �������� + 2";
                if (card.cardType == 0 || card.cardType == 1)
                    damage += 2;
                break;
            case Player_Class.Wizard:
                jopAbility.text = "������ : �������� + 2";
                if (card.cardType == 2 || card.cardType == 3)
                    damage += 2;
                break;
            case Player_Class.Archer:
                jopAbility.text = "��  �� : �ڽ�Ʈ + 1";
                break;
            case Player_Class.Bard:
                jopAbility.text = "��  �� : ȸ�� + 1, ���ذ��� + 1";
                if (card.cardType == 4)
                    heal += 1;
                break;
            case Player_Class.Priest:
                jopAbility.text = "��  �� : �������� + 1, �������� + 1";
                break;
        }

        // ī�� Ÿ�Կ� ���� ����� �� �����
        if (card.cardType == 0)
        {
            cardInfo.text = card.cardInfo + "\n���� ���� : " + card.damage.ToString();
            totalDamage.text = "�� ����� : " + damage.ToString();
        }
        else if (card.cardType == 1)
        {
            cardInfo.text = card.cardInfo + "\n���� ���� : " + card.damage.ToString() + " X 2";
            totalDamage.text = "�� ����� : " + damage.ToString() + " X 2";
        }            
        else if (card.cardType == 2)
        {
            cardInfo.text = card.cardInfo + "\n���� ���� : " + card.damage.ToString();
            totalDamage.text = "�� ����� : " + damage.ToString();
        }
        else if (card.cardType == 3)
        {
            cardInfo.text = card.cardInfo + "\n���� ���� : " + card.damage.ToString() + " X 2";
            totalDamage.text = "�� ����� : " + damage.ToString() + " X 2";
        }
        else if (card.cardType == 4)
        {
            cardInfo.text = card.cardInfo + "\nȸ���� : " + card.heal.ToString();
            totalDamage.text = "�� ȸ���� : " + heal.ToString();
        }
        else if (card.cardType == 5)
        {
            cardInfo.text = card.cardInfo + "\n���� ���� : " + card.damage.ToString();
        }
        else if (card.cardType == 6)
        {
            cardInfo.text = card.cardInfo + "\n���� ���� : " + card.damage.ToString();
        }
        else if (card.cardType == 7)
        {
            cardInfo.text = card.cardInfo + "\n���� ���� : " + card.damage.ToString() + ", �ڽ�Ʈ ȸ�� : 1";
        }
        else if (card.cardType == 8)
        {
            cardInfo.text = card.cardInfo + "\n���� ���� : 2,  ü�� ȸ�� : " + card.heal.ToString();
        }
        else if (card.cardType == 9)
        {
            cardInfo.text = card.cardInfo + "\n���� ���� : " + card.damage.ToString() + ", ü�� ȸ�� : 4";
        }
    }

    // ���� �����
    public void HideToolTip()
    {
        gameObject.SetActive(false);
    }

}
