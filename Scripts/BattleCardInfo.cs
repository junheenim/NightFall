using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BattleCardInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // ���� ���� ������

    public BattleCardToolTip battleCardToolTip;
    // �� �� ī�� ����
    public UseCard useCard;
    // �� ��ȣ
    public int cardDeckNum;

    // ���콺�� �׸� ������ ��������
    public void OnPointerEnter(PointerEventData eventData)
    {
        battleCardToolTip.ShowToolTip(useCard.cardDeck[cardDeckNum]);
    }

    // ��������
    public void OnPointerExit(PointerEventData eventData)
    {
        battleCardToolTip.HideToolTip();
    }
}
