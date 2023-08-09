using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bear : Unit
{
    // hp ǥ�� �����̴�
    public Slider hpBar;
    // hp ���� ǥ��
    public Text hpValue;
    // �� �̸�
    public Text enemyName;

    // ����Ʈ
    public GameObject effectHit;
    public GameObject effectfrozen;
    public GameObject frozensharp;
    public GameObject absorption;
    public GameObject cut;
    public GameObject snowStorm;

    public GameObject effectHeal;

    // �ǰ� ��� ��������Ʈ
    public SpriteRenderer enemyImage;

    //����̸� ������ ��
    public GameObject attackName;
    // ����̸�
    public Text attackNameText;

    // �� ���ݽ� ������ �̹���
    public GameObject enemyPicture;

    // �÷��̾� ���� ���
    Player playerOBJ;
    Stage1Battle battle;

    // ����
    public AudioSource audioSource;
    public AudioClip hit;

    public AudioClip slam;
    public AudioClip crevasse;
    public AudioClip skySplitter;
    public AudioClip absorptionClip;
    public AudioClip bite;
    public AudioClip storm;


    public AudioClip healClip;

    // �� ���� ����
    private void Start()
    {
        Name = "���⼳���� ��ȣ�� : �︣";
        MaxHP = 60;
        CurHP = MaxHP;
        isAttack = false;

        battle = GameObject.Find("BattleState").GetComponent<Stage1Battle>();
        playerOBJ = GameObject.FindWithTag("Player").GetComponent<Player>();
        attackName.SetActive(false);

        hpValue.text = MaxHP.ToString() + " / " + CurHP.ToString();
        enemyName.text = "���⼳���� ��ȣ�� : <color=blue>�︣</color>";
    }

    public override bool Attack(int stage)
    {
        int n = Random.Range(1, 101);

        if (n <= 28)
        {
            attackNameText.text = "�����ġ��";
            StartCoroutine(AttackToPlayer(5, effectHit,slam));
        }
        else if (n <= 49)
        {
            attackNameText.text = "ũ���ٽ�";
            StartCoroutine(ChainAttck(4, effectfrozen, crevasse));
        }
        else if (n <= 67)
        {
            attackNameText.text = "õ��";
            StartCoroutine(AttackToPlayer(6, frozensharp, skySplitter));
            Instantiate(effectHeal, transform);
            heal(2);
        }
        else if (n <= 83)
        {
            attackNameText.text = "���";
            StartCoroutine(AttackToPlayer(5, absorption, absorptionClip));
            heal(3);
        }
        else if (n <= 93)
        {
            attackNameText.text = "������";
            StartCoroutine(StrongAttack(10, cut, bite));
            heal(4);
        }
        else
        {
            attackNameText.text = "����ǳ";
            StartCoroutine(StrongAttack(8, snowStorm, storm));
            heal(5);
        }

        // �÷��̾� �׾����� Ȯ��;
        if (GameManager.gameManagerInstance.curHP <= 0)
            return true;
        else
            return false;
    }

    // ü�� ȸ��
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
    // ������ �ޱ�
    public override void TakeDamage(int damage,int attackType)
    {
        // ���� Ÿ�Կ� ���� ������ ����
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
    // �÷��̾� ����
    IEnumerator AttackToPlayer(int damage,GameObject effect, AudioClip clip)
    {
        // ���� �̸� ���̱�
        attackName.SetActive(true);
        // ���� �̹���
        yield return new WaitForSeconds(1f);
        // �����̸� ���ֱ�
        attackName.SetActive(false);

        // ����!
        gameObject.transform.position += Vector3.left * 0.2f;

        // ����Ʈ �ڸ�
        PlaySound(clip);
        Instantiate(effect, battle.playerPosition);
        yield return new WaitForSeconds(0.05f);

        PlayerDamage(damage);

        gameObject.transform.position += Vector3.right * 0.2f;
        //�� �ѱ��
        yield return new WaitForSeconds(0.5f);
        isAttack = false;
        battle.PlayerTurn();
    }
    // ���� ����
    IEnumerator ChainAttck(int damage, GameObject effect, AudioClip clip)
    {
        // ���� �̸� ���̱�
        attackName.SetActive(true);
        yield return new WaitForSeconds(1f);
        // �����̸� ���ֱ�
        attackName.SetActive(false);

        // ����
        gameObject.transform.position += Vector3.left * 0.2f;
        yield return new WaitForSeconds(0.05f);
        // ���ư���
        gameObject.transform.position += Vector3.right * 0.2f;


        // ī�޶� ȿ��
        // ...
        PlaySound(clip);
        Instantiate(effect, battle.playerPosition);

        //�÷��̾� ������ ó��
        PlayerDamage(damage);
        yield return new WaitForSeconds(0.3f);

        // ī�޶� ȿ��
        //...
        PlaySound(clip);
        Instantiate(effect, battle.playerPosition);

        //�÷��̾� ������ ó��
        PlayerDamage(damage);

        // �� �ѱ��
        yield return new WaitForSeconds(0.5f);
        isAttack = false;
        battle.PlayerTurn();
    }
    // �ʻ�� ����
    IEnumerator StrongAttack(int damage, GameObject effect, AudioClip clip)
    {
        // ���� �̸� ���̱�
        attackName.SetActive(true);
        // ���� �̹���
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
        // �����̸� ���ֱ�
        attackName.SetActive(false);

        // ����!
        gameObject.transform.position += Vector3.left * 0.2f;
        // ī�޶� ȿ��
        //...
        PlaySound(clip);
        Instantiate(effect, battle.playerPosition);
        yield return new WaitForSeconds(0.05f);

        PlayerDamage(damage);
        gameObject.transform.position += Vector3.right * 0.2f;

        // �� �ѱ��
        yield return new WaitForSeconds(0.5f);
        isAttack = false;
        battle.PlayerTurn();
    }
    // �÷��̾� ������ ó��
    void PlayerDamage(int damage)
    {
        GameManager.gameManagerInstance.TakeDamage(damage);
        playerOBJ.SetHP();
        StartCoroutine(playerOBJ.TakeDamageEffect());
    }
    // ���� �÷���
    void PlaySound(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}
