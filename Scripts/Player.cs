using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Unit
{
    public Slider hpBar;
    public Text hpValue;

    public SpriteRenderer playerImage;

    public AudioSource audioSource;
    public AudioClip hitClip;
    void Start()
    {
        SetHP();
    }

    public void SetHP()
    {
        MaxHP = GameManager.gameManagerInstance.maxHP;
        CurHP = GameManager.gameManagerInstance.curHP;
        hpBar.value = CurHP / MaxHP;
        hpValue.text = MaxHP.ToString() + " / " + CurHP.ToString();
    }

    public IEnumerator TakeDamageEffect()
    {
        playerImage.color = new Color(255, 0, 0);
        audioSource.clip = hitClip;
        audioSource.Play();
        yield return new WaitForSeconds(0.1f);
        playerImage.color = new Color(255, 255, 255);
    }
}
