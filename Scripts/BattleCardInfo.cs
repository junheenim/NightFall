using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BattleCardInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // 툴팁 정보 보내기

    public BattleCardToolTip battleCardToolTip;
    // 내 덱 카드 정보
    public UseCard useCard;
    // 덱 번호
    public int cardDeckNum;

    // 마우스가 그림 안으로 들어왔을시
    public void OnPointerEnter(PointerEventData eventData)
    {
        battleCardToolTip.ShowToolTip(useCard.cardDeck[cardDeckNum]);
    }

    // 나갔을시
    public void OnPointerExit(PointerEventData eventData)
    {
        battleCardToolTip.HideToolTip();
    }
}
