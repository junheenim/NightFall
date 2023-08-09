using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bear : Unit
{
    // hp 표시 슬라이더
    public Slider hpBar;
    // hp 숫자 표시
    public Text hpValue;
    // 적 이름
    public Text enemyName;

    // 이펙트
    public GameObject effectHit;
    public GameObject effectfrozen;
    public GameObject frozensharp;
    public GameObject absorption;
    public GameObject cut;
    public GameObject snowStorm;

    public GameObject effectHeal;

    // 피격 모션 스프라이트
    public SpriteRenderer enemyImage;

    //기술이름 나오는 곳
    public GameObject attackName;
    // 기술이름
    public Text attackNameText;

    // 강 공격시 나오는 이미지
    public GameObject enemyPicture;

    // 플레이어 정보 얻기
    Player playerOBJ;
    Stage1Battle battle;

    // 사운드
    public AudioSource audioSource;
    public AudioClip hit;

    public AudioClip slam;
    public AudioClip crevasse;
    public AudioClip skySplitter;
    public AudioClip absorptionClip;
    public AudioClip bite;
    public AudioClip storm;


    public AudioClip healClip;

    // 곰 몬스터 세팅
    private void Start()
    {
        Name = "만년설산의 수호자 : 울르";
        MaxHP = 60;
        CurHP = MaxHP;
        isAttack = false;

        battle = GameObject.Find("BattleState").GetComponent<Stage1Battle>();
        playerOBJ = GameObject.FindWithTag("Player").GetComponent<Player>();
        attackName.SetActive(false);

        hpValue.text = MaxHP.ToString() + " / " + CurHP.ToString();
        enemyName.text = "만년설산의 수호자 : <color=blue>울르</color>";
    }

    public override bool Attack(int stage)
    {
        int n = Random.Range(1, 101);

        if (n <= 28)
        {
            attackNameText.text = "몸통박치기";
            StartCoroutine(AttackToPlayer(5, effectHit,slam));
        }
        else if (n <= 49)
        {
            attackNameText.text = "크레바스";
            StartCoroutine(ChainAttck(4, effectfrozen, crevasse));
        }
        else if (n <= 67)
        {
            attackNameText.text = "천공";
            StartCoroutine(AttackToPlayer(6, frozensharp, skySplitter));
            Instantiate(effectHeal, transform);
            heal(2);
        }
        else if (n <= 83)
        {
            attackNameText.text = "흡수";
            StartCoroutine(AttackToPlayer(5, absorption, absorptionClip));
            heal(3);
        }
        else if (n <= 93)
        {
            attackNameText.text = "물어뜯기";
            StartCoroutine(StrongAttack(10, cut, bite));
            heal(4);
        }
        else
        {
            attackNameText.text = "눈폭풍";
            StartCoroutine(StrongAttack(8, snowStorm, storm));
            heal(5);
        }

        // 플레이어 죽었는지 확인;
        if (GameManager.gameManagerInstance.curHP <= 0)
            return true;
        else
            return false;
    }

    // 체력 회복
    public void heal(int heal)
    {
        Instantiate(effectHeal, gameObject.transform);
        CurHP += heal;
        if (CurHP > MaxHP)
        {
            CurHP = MaxHP;
        }
        hpBar.value = CurHP / MaxHP;
        hpValue.text = MaxHP.ToString() + " / " + CurHP.ToString();
    }
    // 데미지 받기
    public override void TakeDamage(int damage,int attackType)
    {
        // 공격 타입에 따른 데미지 감소
        if(attackType==0|| attackType==1|| attackType == 5|| attackType == 7 || attackType == 9)
        {
            damage--;
        }

        CurHP -= damage;
        StartCoroutine("TakeDamageEffect");
        if (CurHP <= 0)
        {
            CurHP = 0;
        }
        hpBar.value = CurHP / MaxHP;
        hpValue.text = MaxHP.ToString() + " / " + CurHP.ToString();
    }
    IEnumerator TakeDamageEffect()
    {
        enemyImage.color = new Color(255, 0, 0);
        audioSource.clip = hit;
        audioSource.Play();
        yield return new WaitForSeconds(0.1f);
        enemyImage.color = new Color(255, 255, 255);
    }
    // 플레이어 공격
    IEnumerator AttackToPlayer(int damage,GameObject effect, AudioClip clip)
    {
        // 공격 이름 보이기
        attackName.SetActive(true);
        // 몬스터 이미지
        yield return new WaitForSeconds(1f);
        // 공격이름 없애기
        attackName.SetActive(false);

        // 공격!
        gameObject.transform.position += Vector3.left * 0.2f;

        // 이펙트 자리
        PlaySound(clip);
        Instantiate(effect, battle.playerPosition);
        yield return new WaitForSeconds(0.05f);

        PlayerDamage(damage);

        gameObject.transform.position += Vector3.right * 0.2f;
        //턴 넘기기
        yield return new WaitForSeconds(0.5f);
        isAttack = false;
        battle.PlayerTurn();
    }
    // 연속 공격
    IEnumerator ChainAttck(int damage, GameObject effect, AudioClip clip)
    {
        // 공격 이름 보이기
        attackName.SetActive(true);
        yield return new WaitForSeconds(1f);
        // 공격이름 없애기
        attackName.SetActive(false);

        // 공격
        gameObject.transform.position += Vector3.left * 0.2f;
        yield return new WaitForSeconds(0.05f);
        // 돌아가기
        gameObject.transform.position += Vector3.right * 0.2f;


        // 카메라 효과
        // ...
        PlaySound(clip);
        Instantiate(effect, battle.playerPosition);

        //플레이어 데미지 처리
        PlayerDamage(damage);
        yield return new WaitForSeconds(0.3f);

        // 카메라 효과
        //...
        PlaySound(clip);
        Instantiate(effect, battle.playerPosition);

        //플레이어 데미지 처리
        PlayerDamage(damage);

        // 턴 넘기기
        yield return new WaitForSeconds(0.5f);
        isAttack = false;
        battle.PlayerTurn();
    }
    // 필살기 공격
    IEnumerator StrongAttack(int damage, GameObject effect, AudioClip clip)
    {
        // 공격 이름 보이기
        attackName.SetActive(true);
        // 몬스터 이미지
        while (enemyPicture.transform.localPosition.x >= 450f)
        {
            enemyPicture.transform.localPosition += Vector3.left * 25;
            yield return new WaitForSeconds(0.005f);
        }
        yield return new WaitForSeconds(0.5f);
        while (enemyPicture.transform.localPosition.x <= 1250)
        {
            enemyPicture.transform.localPosition += Vector3.right * 25;
            yield return new WaitForSeconds(0.005f);
        }
        // 공격이름 없애기
        attackName.SetActive(false);

        // 공격!
        gameObject.transform.position += Vector3.left * 0.2f;
        // 카메라 효과
        //...
        PlaySound(clip);
        Instantiate(effect, battle.playerPosition);
        yield return new WaitForSeconds(0.05f);

        PlayerDamage(damage);
        gameObject.transform.position += Vector3.right * 0.2f;

        // 턴 넘기기
        yield return new WaitForSeconds(0.5f);
        isAttack = false;
        battle.PlayerTurn();
    }
    // 플레이어 데미지 처리
    void PlayerDamage(int damage)
    {
        GameManager.gameManagerInstance.TakeDamage(damage);
        playerOBJ.SetHP();
        StartCoroutine(playerOBJ.TakeDamageEffect());
    }
    // 사운드 플레이
    void PlaySound(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}
