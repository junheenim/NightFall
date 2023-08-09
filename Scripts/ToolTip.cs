using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ToolTip : MonoBehaviour
{
    // ī�� �̸�
    [SerializeField]
    Text cardName;

    // ī�� ����
    [SerializeField]
    Text cardLevel;

    //ī�� ����
    [SerializeField]
    Text cardDetail;

    //ī�� �ɷ�
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
    // ���� ����
    public void ShowToolTip(Card card)
    {
        gameObject.SetActive(true);

        cardLevel.text = "lv : " + card.cardLevel.ToString();
        cardName.text = card.cardname;
        cardDetail.text = card.cardInfo;

        // ī�� ������ ���� �ٸ� ����
        if (card.cardType == 0)
        {
            cardAbility.text = "���� ���� : " + card.damage;
        }
        else if (card.cardType == 1)
        {
            cardAbility.text = "���� ���� : " + card.damage + " X 2";
        }
        else if(card.cardType == 2)
        {
            cardAbility.text = "���� ���� : " + card.damage;
        }
        else if(card.cardType == 3)
        {
            cardAbility.text = "���� ���� : " + card.damage + " X 2";
        }
        else if(card.cardType == 4)
        {
            cardAbility.text = "ȸ�� : " + card.heal;
        }

        // ���� ī��
        else if(card.cardType == 5)
        {
            cardAbility.text = "���� ���� : " + card.damage.ToString();
        }
        else if (card.cardType == 6)
        {
            cardAbility.text = "���� ���� : " + card.damage.ToString();
        }
        else if (card.cardType == 7)
        {
            cardAbility.text = "���� ���� : " + card.damage.ToString() + "\n �ڽ�Ʈ ȸ�� : 1 ";
        }
        else if (card.cardType == 8)
        {
            cardAbility.text = "���� ���� : " + card.damage.ToString() + "\nü�� ȸ�� : " + card.heal.ToString();
        }
        else if (card.cardType == 9)
        {
            cardAbility.text = "���� ���� : " + card.damage.ToString() + "\nü�� ȸ�� : " + card.heal.ToString();
        }
    }

    // ���� �����
    public void HideToolTip()
    {
        gameObject.SetActive(false);
    }
}
