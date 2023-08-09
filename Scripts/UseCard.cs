using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseCard : MonoBehaviour
{
    // �� Unit ������
    public Transform player;
    public Transform enemy;

    Player playerObj;
    // ��ü ����
    public GameObject[] partners;
    // ���� �̹���
    public GameObject partnerIn;

    public Stage1Battle stage1Battle;
    // ��ü ī�� ����
    public List<Card> allCard;
    // ���� ī��
    public Card[] cardDeck;

    // ��ü ���� ����
    public List<Card> AllpartnerCard;
    // ���� ī��
    public Card[] partnerCard;

    // ī�� �̹���
    public Image[] cardImage;

    // ī�� �޸� �̹���
    public Sprite backCard;

    // ���� ī��� 1���� ������ �ϱ�
    bool partnerOn;

    // ����Ʈ ����
    // ����
    public GameObject effectCut;
    // ȭ��           
    public GameObject effectArrow;
    // ����           
    public GameObject effectHit;
    // ����           
    public GameObject effectfire;
    // ���           
    public GameObject effectlightning;
    // ��  
    public GameObject effectHeal;
    // �ڽ�Ʈ ȸ��
    public GameObject costHeal;
    // ���� ����Ʈ
    GameObject hitEffect;

    public GameObject[] partnereff;


    // ��ư 1�� ���� ����
    public Button[] buttons;
    public GameObject[] usedCard;

    // ���� �¸� ����ȭ������

    public GameObject fadeImage;
    public GameObject reward;

    // �ڽ�Ʈ��
    int curCost;

    public Image costImage;
    public Text curCostText;

    // NextTurn��ư
    public GameObject nextTurn;

    // ī�� ��ġ �̵�
    public GameObject[] cardPos;
    float[] posX = { -500, -250, 0, 250, 500 };
    bool cardUsing;
    bool cardReady;

    public AudioSource cardAudio;
    public AudioClip heal;
    public AudioClip costheal;
    public AudioClip fire;
    public AudioClip lightning;
    // �ڽ�Ʈ ������ ���� �Ҹ�
    public AudioClip beep;

    private void Start()
    {
        // gameManager�� �ִ� ī�� ��������
        for (int i = 0; i < GameManager.gameManagerInstance.attackCards.Length; i++)
        {
            allCard.Add(GameManager.gameManagerInstance.attackCards[i]);
        }

        for (int i = 0; i < GameManager.gameManagerInstance.partnerCards.Length; i++)
        {
            AllpartnerCard.Add(GameManager.gameManagerInstance.partnerCards[i]);
        }

        // ������ ���� ����Ʈ ����
        if (GameManager.gameManagerInstance.myJob == Player_Class.Archer)
        {
            hitEffect = effectArrow;
        }
        else if (GameManager.gameManagerInstance.myJob == Player_Class.Bard || GameManager.gameManagerInstance.myJob == Player_Class.Wizard)
        {
            hitEffect = effectHit;
        }
        else
        {
            hitEffect = effectCut;
        }
        cardUsing = false;

        //���ἱ�ÿ� ���� ī�� �߰�
        partnerCard[0] = AllpartnerCard[(int)GameManager.gameManagerInstance.partner1 - 1];
        partnerCard[1] = AllpartnerCard[(int)GameManager.gameManagerInstance.partner2 - 1];
        playerObj = GameObject.Find("Player(Clone)").GetComponent<Player>();
        nextTurn.SetActive(false);
    }

    // ī�� �̱�
    public void CardDrow()
    {
        cardReady = true;
        // ���� ���� ���ƿö� �ڽ�Ʈ �� �ʱ�ȭ
        curCost = GameManager.gameManagerInstance.playerTotalCost;

        // ������ �ü��̸� �ڽ�Ʈ 1 �߰�
        if (GameManager.gameManagerInstance.myJob == Player_Class.Archer)
            curCost++;

        // ���� �ڽ�Ʈ ��
        curCostText.text = curCost.ToString();

        // ����ī�� �̱� ����
        partnerOn = false;

        // ī�� �̱�
        for (int i = 0; i < 5; i++)
        {
            usedCard[i].transform.eulerAngles = Vector3.zero;
            buttons[i].interactable = true;
            int n = Random.Range(1, 101);
            cardDeck[i] = DrowCardInfo(n);
        }
        //ī�� ���� �� ������
        StartCoroutine("CardSetting");
    }

    //ī�� ���� �� ������
    IEnumerator CardSetting()
    {
        for (int i = 0; i < 5; i++)
        {
            StartCoroutine(SortCart(i, posX[i]));
        }
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < 5; i++)
        {
            StartCoroutine(CardOn(i));
        }
        nextTurn.SetActive(true);
        cardReady = false;
    }
    #region ī����ġ
    // 1   : -460, -940
    // 2   : -230
    // 3   : 0
    // 4   : 230
    // 5   : 460
    // ���� : 901
    #endregion
    //ī�� ����
    IEnumerator SortCart(int n, float x)
    {
        // ī�� ������
        while (cardPos[n].transform.localPosition.x <= x)
        {
            cardPos[n].transform.localPosition += Vector3.right * 25;
            yield return new WaitForSeconds(0.01f);
        }
        // ��ġ ����
        cardPos[n].transform.localPosition = new Vector3(x, cardPos[n].transform.localPosition.y, 0);
    }
    // ī�� ������
    IEnumerator CardOn(int n)
    {
        while (cardPos[n].transform.eulerAngles.y <= 90)
        {
            cardPos[n].transform.eulerAngles += Vector3.up * 10;
            yield return new WaitForSeconds(0.001f);
        }
        cardPos[n].GetComponent<Image>().sprite = cardDeck[n].card_Image;
        cardPos[n].transform.eulerAngles = new Vector3(0, -90, 0);
        while (cardPos[n].transform.eulerAngles.y >= 0.1f)
        {
            cardPos[n].transform.eulerAngles += Vector3.up * 10;
            yield return new WaitForSeconds(0.001f);
        }
        cardPos[n].transform.eulerAngles = new Vector3(0, 0, 0);
        cardUsing = false;
    }

    // ī�� Ȯ���� ī�� �̱�
    Card DrowCardInfo(int n)
    {
        // Ȯ��
        if (partnerOn)
        {
            return NormalCardDrow();
        }
        else
        {
            // ����ī�尡 ���� �ȳ�������(����ī��� ���Ͽ� �ִ� 1��������)
            if (!partnerOn)
            {
                if (n <= 24)
                    return allCard[0];
                else if (n <= 43)
                    return allCard[1];
                else if (n <= 63)
                    return allCard[2];
                else if (n <= 80)
                    return allCard[3];
                else if (n <= 90)
                    return allCard[4];
                else
                {
                    partnerOn = true;
                    int num = Random.Range(0, 2);
                    return partnerCard[num];
                }
            }
            // ���ᰡ ��������
            else
            {
                return NormalCardDrow();
            }
        }
    }

    // �븻ī�常 ����
    Card NormalCardDrow()
    {
        int n = Random.Range(1, 101);
        if (n <= 22)
            return allCard[0];
        else if (n <= 42)
            return allCard[1];
        else if (n <= 63)
            return allCard[2];
        else if (n <= 83)
            return allCard[3];
        else
            return allCard[4];
    }

    // ī�� Ŭ����
    public void OnClickCard(int butNum)
    {
        // ī�� �����, ������ �ƴҶ�, �� HP�� 0���� �϶�
        if (cardUsing || stage1Battle.state != BattleState.PlayerTurn || stage1Battle.enemy.CurHP <= 0 || cardReady)
            return;

        cardUsing = true;
        // �ڽ�Ʈ ������
        if (curCost < cardDeck[butNum - 1].cardCost)
        {
            cardUsing = false;
            cardAudio.clip = beep;
            cardAudio.Play();
            StartCoroutine("EmptyCost");
            return;
        }

        // ���� �ڽ�Ʈ ����
        curCost -= cardDeck[butNum - 1].cardCost;
        // �����ڽ�Ʈ �˷��ֱ�
        curCostText.text = curCost.ToString();

        StartCoroutine(Damage(butNum - 1, cardDeck[butNum - 1].cardType));

        buttons[butNum - 1].interactable = false;
        StartCoroutine(RotateCard(butNum - 1));
    }

    // �ڽ�Ʈ ������ �˸�
    IEnumerator EmptyCost()
    {
        for (int i = 0; i < 3; i++)
        {
            costImage.color = new Color(255, 0, 0);
            yield return new WaitForSeconds(0.1f);
            costImage.color = new Color(255, 255, 255);
        }
    }

    // ī�� ���� �޸����� ������
    IEnumerator RotateCard(int n)
    {
        while (usedCard[n].transform.eulerAngles.y <= 90)
        {
            usedCard[n].transform.eulerAngles += Vector3.up * 10;
            yield return new WaitForSeconds(0.003f);
        }
        cardImage[n].sprite = backCard;
        while (usedCard[n].transform.eulerAngles.y <= 180)
        {
            usedCard[n].transform.eulerAngles += Vector3.up * 10;
            yield return new WaitForSeconds(0.003f);
        }
        cardUsing = false;
    }

    // ���������� �ѱ��
    public void OnClickNextTurn()
    {
        // �� ü���� 0 �̸� return
        if (stage1Battle.enemy.CurHP <= 0 || 
            cardUsing || 
            stage1Battle.state != BattleState.PlayerTurn
            || stage1Battle.enemy.isAttack)
            return;

        StartCoroutine("NextTurn");
    }

    // ���� �� �̵�
    IEnumerator NextTurn()
    {
        // �ߺ� ���� ���� �� ����� ȥ�� ����
        nextTurn.SetActive(false);
        cardReady = true;
        // ��� ī�� ������
        for (int i = 0; i < 5; i++)
        {
            StartCoroutine(RotateCard(i));
        }
        // ī�� ��ġ ����
        
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < 5; i++)
        {
            StartCoroutine(GoTomb(i));
        }
        yield return new WaitForSeconds(1f);
        stage1Battle.state = BattleState.EnemyTurn;
        cardReady = false;
        stage1Battle.EnemyTurn();
    }

    // ���������� �ѱ�� ī��� ��� �������� ������
    IEnumerator GoTomb(int n)
    {
        // ī�� �������� ������
        while (cardPos[n].transform.localPosition.x <= 770)
        {
            cardPos[n].transform.localPosition += Vector3.right * 25f;
            yield return new WaitForSeconds(0.01f);
        }
        // �������� ������ �ٽ� �̵�
        cardPos[n].transform.localPosition = new Vector3(-785, cardPos[n].transform.localPosition.y, 0);
    }
    // ���� ���ط� ���
    public int FinalDamage(int damage,int type)
    {
        int finalDamage = damage;
        if (type == 0 || type == 1)
        {
            if(GameManager.gameManagerInstance.myJob==Player_Class.Warrior)
            {
                finalDamage += 2;
            }
            else if(GameManager.gameManagerInstance.myJob == Player_Class.Priest)
            {
                finalDamage++;
            }
        }
        else if(type == 2 || type == 3)
        {
            if (GameManager.gameManagerInstance.myJob == Player_Class.Wizard)
            {
                finalDamage += 2;
            }
            else if (GameManager.gameManagerInstance.myJob == Player_Class.Priest)
            {
                finalDamage++;
            }
        }
        return finalDamage;
    }

    // n : Deck index, type : card Type
    IEnumerator Damage(int n, int type)
    {
        int damage = FinalDamage(cardDeck[n].damage, type);
        // ����Ʈ ����
        switch (type)
        {
            // ���� ����
            case 0:
                player.position += Vector3.right * 0.2f;
                yield return new WaitForSeconds(0.05f);
                stage1Battle.enemy.TakeDamage(damage, type);
                Instantiate(hitEffect, enemy);
                // �� ������ �ð� �������ϱ�
                if (stage1Battle.enemy.CurHP <= 0)
                {
                    Time.timeScale = 0.5f;
                    yield return new WaitForSeconds(1f);
                    Time.timeScale = 1;
                    StopAllCoroutines();
                    StartCoroutine(Won(0, 1));
                }
                player.position += Vector3.left * 0.2f;
                yield return new WaitForSeconds(1f);
                cardUsing = false;
                break;

            // ���� ���� ����
            case 1:
                player.position += Vector3.right * 0.2f;
                yield return new WaitForSeconds(0.05f);
                stage1Battle.enemy.TakeDamage(damage, type);
                Instantiate(hitEffect, enemy);
                player.position += Vector3.left * 0.2f;
                yield return new WaitForSeconds(0.3f);
                stage1Battle.enemy.TakeDamage(damage, type);
                Instantiate(hitEffect, enemy);
                if (stage1Battle.enemy.CurHP <= 0)
                {
                    Time.timeScale = 0.5f;
                    yield return new WaitForSeconds(1f);
                    Time.timeScale = 1;
                    StopAllCoroutines();
                    StartCoroutine(Won(0, 1));
                }
                yield return new WaitForSeconds(1f);
                cardUsing = false;
                break;

            // ���� ����
            case 2:
                cardAudio.clip = fire;
                cardAudio.Play();
                player.position += Vector3.right * 0.2f;
                yield return new WaitForSeconds(0.05f);
                stage1Battle.enemy.TakeDamage(damage, type);
                Instantiate(effectfire, enemy);
                if (stage1Battle.enemy.CurHP <= 0)
                {
                    Time.timeScale = 0.5f;
                    yield return new WaitForSeconds(1f);
                    Time.timeScale = 1;
                    StopAllCoroutines();
                    StartCoroutine(Won(0, 1));
                }
                player.position += Vector3.left * 0.2f;
                yield return new WaitForSeconds(1f);
                cardUsing = false;
                break;

            // ���� ���� ����
            case 3:
                cardAudio.clip = lightning;
                cardAudio.Play();
                player.position += Vector3.right * 0.2f;
                yield return new WaitForSeconds(0.05f);
                stage1Battle.enemy.TakeDamage(damage, type);
                Instantiate(effectlightning, enemy);
                player.position += Vector3.left * 0.2f;
                yield return new WaitForSeconds(0.5f);
                stage1Battle.enemy.TakeDamage(damage, type);
                Instantiate(effectlightning, enemy);
                if (stage1Battle.enemy.CurHP <= 0)
                {
                    Time.timeScale = 0.5f;
                    yield return new WaitForSeconds(1f);
                    Time.timeScale = 1;
                    StopAllCoroutines();
                    StartCoroutine(Won(0, 1));
                }
                yield return new WaitForSeconds(1f);
                cardUsing = false;
                break;

            // ��
            case 4:
                Heal(n);
                break;

            // ���� ����
            case 5:
                partnerIn = partners[type - 5];
                StartCoroutine(PartnerAttack(damage, type - 5));
                break;                             
            case 6:                                
                partnerIn = partners[type - 5];
                StartCoroutine(PartnerAttack(damage, type - 5));
                break;                             
            case 7:
                curCost++;
                curCostText.text = curCost.ToString();
                cardAudio.clip = costheal;
                cardAudio.Play();
                Instantiate(costHeal, player);
                partnerIn = partners[type - 5];
                StartCoroutine(PartnerAttack(damage, type - 5));
                break;                             
            case 8:
                Heal(n); 
                partnerIn = partners[type - 5];
                StartCoroutine(PartnerAttack(damage, type - 5));
                break;                             
            case 9:
                Heal(n);
                partnerIn = partners[type - 5];
                StartCoroutine(PartnerAttack(damage, type - 5));
                break;
        }
    }

    // ��
    public void Heal(int n)
    {
        int finalHeal = cardDeck[n].heal;
        // �÷��̾� ������ �ٵ��Ͻ� ȸ���� ����;
        if (GameManager.gameManagerInstance.myJob == Player_Class.Bard)
            finalHeal++;
        GameManager.gameManagerInstance.Heal(finalHeal);
        playerObj.SetHP();
        cardAudio.clip = heal;
        cardAudio.Play();
        Instantiate(effectHeal, player);
        cardUsing = false;
    }

    // ��Ʈ�� ī�� ���
    IEnumerator PartnerAttack(int damage,int type)
    {
        while (partnerIn.transform.position.x <= -1)
        {
            partnerIn.transform.position += Vector3.right * 0.5f;
            yield return new WaitForSeconds(0.01f);
        }

        yield return new WaitForSeconds(0.5f);
        partnerIn.transform.position += Vector3.right * 0.2f;
        stage1Battle.enemy.TakeDamage(damage, type);

        // ����Ʈ ����
        Instantiate(partnereff[type], enemy);
        if (stage1Battle.enemy.CurHP <= 0)
        {
            Time.timeScale = 0.5f;
            yield return new WaitForSeconds(1f);
            Time.timeScale = 1;
            StopAllCoroutines();
            StartCoroutine(Won(0, 1));
        }
        yield return new WaitForSeconds(0.05f);
        partnerIn.transform.position += Vector3.left * 0.2f;
        yield return new WaitForSeconds(0.5f);

        while (partnerIn.transform.position.x >= -13)
        {
            partnerIn.transform.position += Vector3.left * 0.5f;
            yield return new WaitForSeconds(0.01f);
        }
        cardUsing = false;
    }
    // ��Ʋ �¸��� ���� ȭ������
    IEnumerator Won(float start, float end)
    {
        // �� ���������� ���� Ŭ���� Ȯ��
        if(stage1Battle.stage==1)
            GameManager.gameManagerInstance.stage1clear = true;
        else if(stage1Battle.stage == 2)
            GameManager.gameManagerInstance.stage2clear = true;
        else if(stage1Battle.stage == 3)
            GameManager.gameManagerInstance.stage3clear = true;
        else if(stage1Battle.stage == 4)
            GameManager.gameManagerInstance.stage4clear = true;

        //���� ȭ������
        fadeImage.SetActive(true);
        float cur = 0.0f, per = 0.0f;
        while (per < 1.0f)
        {
            cur += Time.deltaTime;
            per = cur / 1.0f;

            Color color = fadeImage.GetComponent<Image>().color;
            color.a = Mathf.Lerp(start, end, per);
            fadeImage.GetComponent<Image>().color = color;

            yield return null;
        }
        reward.SetActive(true);

        cur = 0.0f;
        per = 0.0f;
        while (per < 1.0f)
        {
            cur += Time.deltaTime;
            per = cur / 1.0f;

            Color color = fadeImage.GetComponent<Image>().color;
            color.a = Mathf.Lerp(end, start, per);
            fadeImage.GetComponent<Image>().color = color;

            yield return null;
        }
    }
}
