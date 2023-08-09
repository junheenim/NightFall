using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rich : Unit
{
    // hp ǥ�� �����̴�
    public Slider hpBar;
    // hp ���� ǥ��
    public Text hpValue;
    // �� �̸�
    public Text enemyName;

    // ����Ʈ
    public GameObject devastationeffect;
    public GameObject corruptioneffect;
    public GameObject wailingeffect;
    public GameObject curseeffect;
    public GameObject requiemeffect;

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

    public AudioClip devastation;
    public AudioClip corruption;
    public AudioClip wailing;
    public AudioClip curse;
    public AudioClip requieme;

    public AudioClip healClip;

    Stage1Battle battle;

    // �� ���� ����
    private void Start()
    {
        Name = "����� ���� : ��ٸ�";
        MaxHP = 60;
        CurHP = MaxHP;
        isAttack = false;

        battle = GameObject.Find("BattleState").GetComponent<Stage1Battle>();
        playerOBJ = GameObject.FindWithTag("Player").GetComponent<Player>();
        attackName.SetActive(false);

        hpValue.text = MaxHP.ToString() + " / " + CurHP.ToString();
        enemyName.text = "����� ���� : <color=#8B008B>��ٸ�</color>";
    }

    public override bool Attack(int stage)
    {
        int n = Random.Range(1, 101);

        if (n <= 39)
        {
            attackNameText.text = "Ȳ��ȭ";
            StartCoroutine(AttackToPlayer(7, devastationeffect, devastation));
        }
        else if (n <= 67)
        {
            attackNameText.text = "����";
            StartCoroutine(AttackToPlayer(10, corruptioneffect, corruption));
        }
        else if (n <= 88)
        {
            attackNameText.text = "���";
            StartCoroutine(AttackToPlayer(6, wailingeffect, wailing));
            Instantiate(effectHeal, transform);
            heal(5);
        }
        else if (n <= 98)
        {
            attackNameText.text = "����";
            StartCoroutine(StrongAttack(13, curseeffect, curse));
            heal(3);
        }
        else
        {
            attackNameText.text = "��ȥ��";
            StartCoroutine(StrongAttack(15, requiemeffect, requieme));
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
        if (attackType == 2 || attackType == 3 || attackType == 6 || attackType == 8)
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
    IEnumerator TakeDamageEffect()
    {
        enemyImage.color = new Color(255, 0, 0);
        audioSource.clip = hit;
        audioSource.Play();
        yield return new WaitForSeconds(0.1f);
        enemyImage.color = new Color(255, 255, 255);
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
