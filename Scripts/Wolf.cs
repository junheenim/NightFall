using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Wolf : Unit
{
    public Slider hpBar;
    public Text hpValue;
    public Text enemyName;

    public GameObject effectCut;
    public GameObject effecthit;
    public GameObject effectHeal;

    public SpriteRenderer enemyImage;

    public GameObject attackName;
    public Text attackNameText;
    public GameObject enemyPicture;

    // 플레이어 정보 얻기
    Player playerOBJ;

    // 사운드
    public AudioSource audioSource;
    public AudioClip hit;

    public AudioClip slam;
    public AudioClip claw;
    public AudioClip howling;
    public AudioClip bite;

    Stage1Battle battle;
    // 늑대 몬스터 세팅
    private void Start()
    {
        Name = "굶주린 늑대 : 프레키";
        MaxHP = 40;
        CurHP = MaxHP;
        isAttack = false;

        battle = GameObject.Find("BattleState").GetComponent<Stage1Battle>();
        playerOBJ = GameObject.FindWithTag("Player").GetComponent<Player>();
        attackName.SetActive(false);

        hpValue.text = MaxHP.ToString() + " / " + CurHP.ToString();
        enemyName.text = "굶주린 늑대 : <color=red>프레키</color>";
    }

    public override bool Attack(int stage)
    {
        int n = Random.Range(1, 101);

        if (n <= 39)
        {
            attackNameText.text = "몸통박치기";
            StartCoroutine(AttackToPlayer(3, slam));
        }
        else if (n <= 51)
        {
            attackNameText.text = "할퀴기";
            StartCoroutine(ChainAttck(2, claw));
        }
        else if (n <= 74)
        {
            attackNameText.text = "하울링";
            StartCoroutine(AttackToPlayer(2, null));
            Instantiate(effectHeal, gameObject.transform);
            audioSource.clip = howling;
            audioSource.Play();
            CurHP += 2;
            if (CurHP > MaxHP)
            {
                CurHP = MaxHP;
            }
            hpBar.value = CurHP / MaxHP;
            hpValue.text = MaxHP.ToString() + " / " + CurHP.ToString();
        }
        else
        {
            attackNameText.text = "깨물기";
            StartCoroutine(StrongAttack(7, bite));
        }

        // 플레이어 죽었는지 확인;
        if (GameManager.gameManagerInstance.curHP <= 0)
            return true;
        else
            return false;
    }
    // 데미지 받기
    public override void TakeDamage(int damage, int attckType)
    {
        CurHP -= damage;
        StartCoroutine("TakeDamageEffect");
        if (CurHP <= 0)
        {
            CurHP = 0;
        }
        hpBar.value = CurHP / MaxHP;
        hpValue.text = MaxHP.ToString() + " / " + CurHP.ToString();
    }

    //데미지 이펙트
    IEnumerator TakeDamageEffect()
    {
        enemyImage.color = new Color(255, 0, 0);
        audioSource.clip = hit;
        audioSource.Play();
        yield return new WaitForSeconds(0.1f);
        enemyImage.color = new Color(255, 255, 255);
    }
    // 플레이어 공격
    IEnumerator AttackToPlayer(int damage, AudioClip clip)
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
        Instantiate(effecthit, battle.playerPosition);
        yield return new WaitForSeconds(0.05f);
        PlayerDamage(damage);
        gameObject.transform.position += Vector3.right * 0.2f;
        //턴 넘기기
        yield return new WaitForSeconds(0.5f);
        isAttack = false;
        battle.PlayerTurn();
    }
    // 연속 공격
    IEnumerator ChainAttck(int damage, AudioClip clip)
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
        Instantiate(effectCut, battle.playerPosition);

        yield return new WaitForSeconds(0.3f);
        //플레이어 데미지 처리
        PlayerDamage(damage);

        // 카메라 효과
        //...
        PlaySound(clip);
        Instantiate(effectCut, battle.playerPosition);

        //플레이어 데미지 처리
        PlayerDamage(damage);

        // 턴 넘기기
        yield return new WaitForSeconds(0.5f);
        isAttack = false;
        battle.PlayerTurn();
    }
    // 필살기 공격
    IEnumerator StrongAttack(int damage, AudioClip clip)
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
        Instantiate(effectCut, battle.playerPosition);
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
        if (clip == null)
            return;
        audioSource.clip = clip;
        audioSource.Play();
    }
}
