using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseCard : MonoBehaviour
{
    // 각 Unit 포지션
    public Transform player;
    public Transform enemy;

    Player playerObj;
    // 전체 동료
    public GameObject[] partners;
    // 동료 이미지
    public GameObject partnerIn;

    public Stage1Battle stage1Battle;
    // 전체 카드 종류
    public List<Card> allCard;
    // 뽑은 카드
    public Card[] cardDeck;

    // 전체 동료 종류
    public List<Card> AllpartnerCard;
    // 동료 카드
    public Card[] partnerCard;

    // 카드 이미지
    public Image[] cardImage;

    // 카드 뒷면 이미지
    public Sprite backCard;

    // 동료 카드는 1개만 나오게 하기
    bool partnerOn;

    // 이펙트 생성
    // 베임
    public GameObject effectCut;
    // 화살           
    public GameObject effectArrow;
    // 맞음           
    public GameObject effectHit;
    // 마법           
    public GameObject effectfire;
    // 방어           
    public GameObject effectlightning;
    // 힐  
    public GameObject effectHeal;
    // 코스트 회복
    public GameObject costHeal;
    // 최종 이펙트
    GameObject hitEffect;

    public GameObject[] partnereff;


    // 버튼 1번 사용시 금지
    public Button[] buttons;
    public GameObject[] usedCard;

    // 게임 승리 보상화면으로

    public GameObject fadeImage;
    public GameObject reward;

    // 코스트값
    int curCost;

    public Image costImage;
    public Text curCostText;

    // NextTurn버튼
    public GameObject nextTurn;

    // 카드 위치 이동
    public GameObject[] cardPos;
    float[] posX = { -500, -250, 0, 250, 500 };
    bool cardUsing;
    bool cardReady;

    public AudioSource cardAudio;
    public AudioClip heal;
    public AudioClip costheal;
    public AudioClip fire;
    public AudioClip lightning;
    // 코스트 없을때 나는 소리
    public AudioClip beep;

    private void Start()
    {
        // gameManager에 있는 카드 가져오기
        for (int i = 0; i < GameManager.gameManagerInstance.attackCards.Length; i++)
        {
            allCard.Add(GameManager.gameManagerInstance.attackCards[i]);
        }

        for (int i = 0; i < GameManager.gameManagerInstance.partnerCards.Length; i++)
        {
            AllpartnerCard.Add(GameManager.gameManagerInstance.partnerCards[i]);
        }

        // 직업에 따른 이펙트 설정
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

        //동료선택에 따른 카드 추가
        partnerCard[0] = AllpartnerCard[(int)GameManager.gameManagerInstance.partner1 - 1];
        partnerCard[1] = AllpartnerCard[(int)GameManager.gameManagerInstance.partner2 - 1];
        playerObj = GameObject.Find("Player(Clone)").GetComponent<Player>();
        nextTurn.SetActive(false);
    }

    // 카드 뽑기
    public void CardDrow()
    {
        cardReady = true;
        // 나의 턴이 돌아올때 코스트 값 초기화
        curCost = GameManager.gameManagerInstance.playerTotalCost;

        // 직업이 궁수이면 코스트 1 추가
        if (GameManager.gameManagerInstance.myJob == Player_Class.Archer)
            curCost++;

        // 현재 코스트 값
        curCostText.text = curCost.ToString();

        // 동료카드 뽑기 갱신
        partnerOn = false;

        // 카드 뽑기
        for (int i = 0; i < 5; i++)
        {
            usedCard[i].transform.eulerAngles = Vector3.zero;
            buttons[i].interactable = true;
            int n = Random.Range(1, 101);
            cardDeck[i] = DrowCardInfo(n);
        }
        //카드 정렬 및 돌리기
        StartCoroutine("CardSetting");
    }

    //카드 정렬 및 돌리기
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
    #region 카드위치
    // 1   : -460, -940
    // 2   : -230
    // 3   : 0
    // 4   : 230
    // 5   : 460
    // 무덤 : 901
    #endregion
    //카드 정렬
    IEnumerator SortCart(int n, float x)
    {
        // 카드 덱정렬
        while (cardPos[n].transform.localPosition.x <= x)
        {
            cardPos[n].transform.localPosition += Vector3.right * 25;
            yield return new WaitForSeconds(0.01f);
        }
        // 위치 보정
        cardPos[n].transform.localPosition = new Vector3(x, cardPos[n].transform.localPosition.y, 0);
    }
    // 카드 뒤집기
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

    // 카드 확률로 카드 뽑기
    Card DrowCardInfo(int n)
    {
        // 확률
        if (partnerOn)
        {
            return NormalCardDrow();
        }
        else
        {
            // 동료카드가 아직 안나왔을시(동료카드는 한턴에 최대 1개만나옴)
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
            // 동료가 나왔으면
            else
            {
                return NormalCardDrow();
            }
        }
    }

    // 노말카드만 뽑힘
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

    // 카드 클릭시
    public void OnClickCard(int butNum)
    {
        // 카드 사용중, 내차레 아닐때, 적 HP가 0이하 일때
        if (cardUsing || stage1Battle.state != BattleState.PlayerTurn || stage1Battle.enemy.CurHP <= 0 || cardReady)
            return;

        cardUsing = true;
        // 코스트 없을때
        if (curCost < cardDeck[butNum - 1].cardCost)
        {
            cardUsing = false;
            cardAudio.clip = beep;
            cardAudio.Play();
            StartCoroutine("EmptyCost");
            return;
        }

        // 현재 코스트 감소
        curCost -= cardDeck[butNum - 1].cardCost;
        // 현재코스트 알려주기
        curCostText.text = curCost.ToString();

        StartCoroutine(Damage(butNum - 1, cardDeck[butNum - 1].cardType));

        buttons[butNum - 1].interactable = false;
        StartCoroutine(RotateCard(butNum - 1));
    }

    // 코스트 없을때 알림
    IEnumerator EmptyCost()
    {
        for (int i = 0; i < 3; i++)
        {
            costImage.color = new Color(255, 0, 0);
            yield return new WaitForSeconds(0.1f);
            costImage.color = new Color(255, 255, 255);
        }
    }

    // 카드 사용시 뒷면으로 뒤집기
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

    // 다음턴으로 넘기기
    public void OnClickNextTurn()
    {
        // 적 체력이 0 이면 return
        if (stage1Battle.enemy.CurHP <= 0 || 
            cardUsing || 
            stage1Battle.state != BattleState.PlayerTurn
            || stage1Battle.enemy.isAttack)
            return;

        StartCoroutine("NextTurn");
    }

    // 다음 턴 이동
    IEnumerator NextTurn()
    {
        // 중복 누름 방지 및 사용자 혼돈 방지
        nextTurn.SetActive(false);
        cardReady = true;
        // 모든 카드 돌리기
        for (int i = 0; i < 5; i++)
        {
            StartCoroutine(RotateCard(i));
        }
        // 카드 위치 세팅
        
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

    // 다음턴으로 넘길시 카드들 모두 무덤으로 보내기
    IEnumerator GoTomb(int n)
    {
        // 카드 무덤으로 보내기
        while (cardPos[n].transform.localPosition.x <= 770)
        {
            cardPos[n].transform.localPosition += Vector3.right * 25f;
            yield return new WaitForSeconds(0.01f);
        }
        // 보냈으면 덱으로 다시 이동
        cardPos[n].transform.localPosition = new Vector3(-785, cardPos[n].transform.localPosition.y, 0);
    }
    // 최종 피해량 계산
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
        // 이펙트 선택
        switch (type)
        {
            // 물리 공격
            case 0:
                player.position += Vector3.right * 0.2f;
                yield return new WaitForSeconds(0.05f);
                stage1Battle.enemy.TakeDamage(damage, type);
                Instantiate(hitEffect, enemy);
                // 적 죽을시 시간 느리게하기
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

            // 물리 연속 공격
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

            // 마법 공격
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

            // 마법 연속 공격
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

            // 힐
            case 4:
                Heal(n);
                break;

            // 동료 공격
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

    // 힐
    public void Heal(int n)
    {
        int finalHeal = cardDeck[n].heal;
        // 플레이어 직업이 바드일시 회복량 증가;
        if (GameManager.gameManagerInstance.myJob == Player_Class.Bard)
            finalHeal++;
        GameManager.gameManagerInstance.Heal(finalHeal);
        playerObj.SetHP();
        cardAudio.clip = heal;
        cardAudio.Play();
        Instantiate(effectHeal, player);
        cardUsing = false;
    }

    // 파트너 카드 사용
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

        // 이펙트 생성
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
    // 배틀 승리시 보상 화면으로
    IEnumerator Won(float start, float end)
    {
        // 각 스테이지에 따른 클리어 확인
        if(stage1Battle.stage==1)
            GameManager.gameManagerInstance.stage1clear = true;
        else if(stage1Battle.stage == 2)
            GameManager.gameManagerInstance.stage2clear = true;
        else if(stage1Battle.stage == 3)
            GameManager.gameManagerInstance.stage3clear = true;
        else if(stage1Battle.stage == 4)
            GameManager.gameManagerInstance.stage4clear = true;

        //보상 화면으로
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
