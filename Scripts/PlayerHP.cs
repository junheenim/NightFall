using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    public Slider hpBar;
    public Text text;

    void Start()
    {
        SetHP();
    }

    public void SetHP()
    {
        hpBar.value = GameManager.gameManagerInstance.curHP / GameManager.gameManagerInstance.maxHP;
        text.text = GameManager.gameManagerInstance.curHP.ToString() + " / " + GameManager.gameManagerInstance.maxHP.ToString();
    }

}
