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

    // �÷��̾� ���� ���
    Player playerOBJ;

    // ����
    public AudioSource audioSource;
    public AudioClip hit;

    public AudioClip slam;
    public AudioClip claw;
    public AudioClip howling;
    public AudioClip bite;

    Stage1Battle battle;
    // ���� ���� ����
    private void Start()
    {
        Name = "���ָ� ���� : ����Ű";
        MaxHP = 40;
        CurHP = MaxHP;
        isAttack = false;

        battle = GameObject.Find("BattleState").GetComponent<Stage1Battle>();
        playerOBJ = GameObject.FindWithTag("Player").GetComponent<Player>();
        attackName.SetActive(false);

        hpValue.text = MaxHP.ToString() + " / " + CurHP.ToString();
        enemyName.text = "���ָ� ���� : <color=red>����Ű</color>";
    }

    public override bool Attack(int stage)
    {
        int n = Random.Range(1, 101);

        if (n <= 39)
        {
            attackNameText.text = "�����ġ��";
            StartCoroutine(AttackToPlayer(3, slam));
        }
        else if (n <= 51)
        {
            attackNameText.text = "������";
            StartCoroutine(ChainAttck(2, claw));
        }
        else if (n <= 74)
        {
            attackNameText.text = "�Ͽ︵";
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
            attackNameText.text = "������";
            StartCoroutine(StrongAttack(7, bite));
        }

        // �÷��̾� �׾����� Ȯ��;
        if (GameManager.gameManagerInstance.curHP <= 0)
            return true;
        else
            return false;
    }
    // ������ �ޱ�
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

    //������ ����Ʈ
    IEnumerator TakeDamageEffect()
    {
        enemyImage.color = new Color(255, 0, 0);
        audioSource.clip = hit;
        audioSource.Play();
        yield return new WaitForSeconds(0.1f);
        enemyImage.color = new Color(255, 255, 255);
    }
    // �÷��̾� ����
    IEnumerator AttackToPlayer(int damage, AudioClip clip)
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
        Instantiate(effecthit, battle.playerPosition);
        yield return new WaitForSeconds(0.05f);
        PlayerDamage(damage);
        gameObject.transform.position += Vector3.right * 0.2f;
        //�� �ѱ��
        yield return new WaitForSeconds(0.5f);
        isAttack = false;
        battle.PlayerTurn();
    }
    // ���� ����
    IEnumerator ChainAttck(int damage, AudioClip clip)
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
        Instantiate(effectCut, battle.playerPosition);

        yield return new WaitForSeconds(0.3f);
        //�÷��̾� ������ ó��
        PlayerDamage(damage);

        // ī�޶� ȿ��
        //...
        PlaySound(clip);
        Instantiate(effectCut, battle.playerPosition);

        //�÷��̾� ������ ó��
        PlayerDamage(damage);

        // �� �ѱ��
        yield return new WaitForSeconds(0.5f);
        isAttack = false;
        battle.PlayerTurn();
    }
    // �ʻ�� ����
    IEnumerator StrongAttack(int damage, AudioClip clip)
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
        Instantiate(effectCut, battle.playerPosition);
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
        if (clip == null)
            return;
        audioSource.clip = clip;
        audioSource.Play();
    }
}
