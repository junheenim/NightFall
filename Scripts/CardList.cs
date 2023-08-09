using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardList : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public List<Card> cards;
    public ToolTip ToolTip;
    public Card card;
    public Slider expBar;
    public Text expText;
    public Image fillingBar;

    public AudioSource audioSource;
    public AudioClip levelUp;

    public int index;
    private void Start()
    {
        // ī�� ���� ��������
        for (int i = 0; i < GameManager.gameManagerInstance.attackCards.Length; i++)
        {
            cards.Add(GameManager.gameManagerInstance.attackCards[i]);
        }
        for (int i = 0; i < GameManager.gameManagerInstance.partnerCards.Length; i++)
        { 
            cards.Add(GameManager.gameManagerInstance.partnerCards[i]);
        }

        //ī�� ����
        card = cards[index];
        Setslider();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (card != null)
        {
            ToolTip.ShowToolTip(card);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ToolTip.HideToolTip();
    }

    public void LevelUP(int n)
    {
        if (cards[n].curEXP < cards[n].cardEXP)
        {
            return;
        }

        audioSource.clip = levelUp;
        audioSource.Play();
        cards[n].LevelUP();
        Setslider();
    }

    // �ʸ��� ���� �� ����ġ �ؽ�Ʈ ����
    public void Setslider()
    {
        if (card.curEXP == 0)
            expBar.value = 0;
        else
            expBar.value = (float)card.curEXP / (float)card.cardEXP;

        expText.text = card.curEXP.ToString() + " / " + card.cardEXP.ToString();
        if (expBar.value >= 1)
        {
            fillingBar.color = Color.green;
        }
        else
        {
            fillingBar.color = Color.red;
        }
    }
}
