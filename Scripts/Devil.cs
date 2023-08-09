using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Devil : Unit
{
    // hp ǥ�� �����̴�
    public Slider hpBar;
    // hp ���� ǥ��
    public Text hpValue;
    // �� �̸�
    public Text enemyName;

    // ����Ʈ
    public GameObject Stareffect;
    public GameObject bubbleSleepffect;
    public GameObject spellthiefeffect;
    public GameObject glittereffect;
    public GameObject Sleepffect;

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

    public AudioSource audioSource;
    public AudioClip hit;
    public AudioClip starClip;
    public AudioClip bubbleSleep;
    public AudioClip spellthief;
    public AudioClip glitter;
    public AudioClip Sleep;

    Stage1Battle battle;

    // �� ���� ����
    private void Start()
    {
        Name = "������ ���� : �����ܵ�";
        MaxHP = 70;
        CurHP = MaxHP;
        isAttack = false;

        battle = GameObject.Find("BattleState").GetComponent<Stage1Battle>();
        playerOBJ = GameObject.FindWithTag("Player").GetComponent<Player>();
        attackName.SetActive(false);

        hpValue.text = MaxHP.ToString() + " / " + CurHP.ToString();
        enemyName.text = "������ ���� : <color=#FF69B4>�����ܵ�</color>";
    }

    public override bool Attack(int stage)
    {
        int n = Random.Range(1, 101);

        if (n <= 40)
        {
            attackNameText.text = "���뺰";
            StartCoroutine(AttackToPlayer(10, Stareffect,starClip));
        }
        else if (n <= 63)
        {
            attackNameText.text = "��������";
            StartCoroutine(AttackToPlayer(15, bubbleSleepffect,bubbleSleep));
        }
        else if (n <= 78)
        {
            attackNameText.text = "�ֹ�����";
            StartCoroutine(AttackToPlayer(9, spellthiefeffect,spellthief));
            Instantiate(effectHeal, transform);
            heal(7);
        }
        else if (n <= 91)
        {
            attackNameText.text = "��¦��¦!";
            StartCoroutine(StrongAttack(17, glittereffect,glitter));
            heal(5);
        }
        else
        {
            attackNameText.text = "������";
            StartCoroutine(StrongAttack(21, Sleepffect, Sleep));
            heal(2);
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
    public override void TakeDamage(int damage, int attackType)
    {
        // ���� Ÿ�Կ� ���� ������ ����
        // ������ ��� ���� Ÿ���̵� ������ 1����
        damage--;
        CurHP -= damage;
        StartCoroutine(TakeDamageEffect());
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
    IEnumerator AttackToPlayer(int damage, GameObject effect, AudioClip clip)
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
