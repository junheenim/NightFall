using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkManager : MonoBehaviour
{
    // ��ȭ ����
    Dictionary<int, string[]> talkData;
    // �÷��̾� �̹��� â
    public Image playerImage;
    // ���� �̹��� â
    public Image enemyImage;
    // ���� �̹���
    public Sprite[] enemySprites;
    // ImageColor
    Color wColor = new Color(1, 1, 1);
    Color dColor = new Color(0.4f, 0.4f, 0.4f);
    // ��ȭâ
    public Text talkText;
    // ���� ��������
    public Stage1Battle stage1Battle;
    
    // �������� ����
    int stage;
    // ��ȭ ����
    int talkIndex;
    // ����Ʈ �Ϸ�
    bool effectCom;

    private void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        GenerateData();
    }
    
    //��ȭ ����
    void GenerateData()
    {
        // key : ��������, value : ���
        talkData.Add(1, new string[] { 
            "�������� '���� ��ӵǴ� ��', ��߰� ���۵� ���� ���밡 ���� ���� ��� ���� ���� �Ѿ��.",
            "�� �� ����� ����� �巯�´�.",
            "Ǯ�� ���̿��� ��Ÿ�� ����� ����� �غ��ϴ� �ڼ�����.",
            "�׸���...",
            "������!",
            "����! ����! ����!",
            "����� �û��Ӱ� �޾ƿ��� ���� ����� �̱۰Ÿ���, �԰����� �� ��ǰ�� �������. ",
            "�������� �⼼�� �޸� ���ַ� ������� �а��װ� �������� ���� ��û�ŷȴ�.",
            "����� ��ȭ�� �� ��������, ���� ��� ������ ������ �˾Ҵ�.",
            "<color=#B22222><size=90>���ָ�</size></color>",
            "��ӵǴ� �Ҹ�� ���ָ����� ����� �޷��� ���̴�."});

        talkData.Add(2, new string[] {
            "���꿡 ���� �����ڸ��� ���� �������� ��� ���������� ���ܳ� ���� ��� �Ѱܾ� �ߴ�.",
            "<color=#00BFFF><size=90>ũ���ٽ�</size></color>",
            "�ܿ� ���س� ������ ���� �߸��µ�, ��� ���� ���ٶ� ���� ��Ÿ�� ������.",
            "���ư���, �ΰ��̿�. ������ �״���� �ݱ��� �ʴ´�.",
            "Ŀ�ٶ� �� ��ġ ���� ���� ���Ϳ� Ǫ�� ��. �������� ����� ��ο���.",
            "���� ������ ��ȣ�ڽÿ�, �ϳ� ����� ��߸� ��ġ�� ���� �������� �մϴ�. �ε� �Ʒ��� ��Ǫ�Ҽ�.",
            "�Ʒ�? �ΰ��� ����� �� ��ӿ� �� ���� ���̵��� ��� �װ�, �;��״´�. �׷��� ���� �Ʒ��� ��Ǯ���?",
            "���� ���� ���ÿ� ������ ���� ���� ½ ��������.",
            "���� ������ �� �̻� �������� �ƴϴ�, �Ƶ��� �ΰ��̿�! �״���� �״� �ڽ��� �˸� �˰� �� �̻� ������ ���������� ����!",
            "���� ������ �β��� �������� ���ش� ȿ���� �پ�鲨��"});

        talkData.Add(3, new string[] {
            "��������� ���� �ӿ� õõ�� ���� ���𵱴�.",
            "���� �ѳ��ӿ��� ������ ���� �����⽺���� �����ߴ�.",
            "�ָ�, ���� ���� ������ ��ħ�� ������ ���׸Ӵ� Ǯ������ ����, �׸��� ���� ��Ҹ��� ������.",
            "�ű����. ����� �ΰ����� ������ ���̴�.",
            "<color=#4B0082><size=90>����ִ� ����, ���, ���.</size></color>",
            "�������� ���� £������ ���⿡ �װ� ������ �ɺ����� �Ѵ��� �˾ƺ��Ҵ�.",
            "�׸��ڴ� ������ ȭ�� ���ϵ�, ������ϵ�, ���� ���� �� ���� �������.",
            "�ڿ��� ����ڵ��� ���⼭ ��帮��.",
            "����� ��ġ �ٵ���� �г�� ���ǿ� ������ ������ġ�� �ٰ��� ������ �غ��ߴ�.",
            "��ũ�θǼ��� �������� �θ����־�!"});

        talkData.Add(4, new string[] {
            "������ ������ ���� ���� �߽ɺδ� �� ����־���. ��Ż�Կ� ������ �ѷ�������, ���� ���� �ǹ��� ������, ��丶�� ���� �� ������.",
            "���� ���� ������ϴ� ��, ���ҿ��� ������� ��Ҹ��� ����Դ�.",
            "�ʹ� ��� ��Ƽ�, �������� ��Ҹ���.",
            "�ȳ�? ���� �Ա���. ��ٷȾ�, ����",
            "���װ�, �װ� <color=#FF69B4><size=90>����?</size></color>",
            "��, �� �̱�� ���ָ� Ǯ���ٰ�. ��� ���� ���� ȥ���� ��.",
            "�̷��� ������, ����? �� ���� ������ �󸶳� ���� �̵��� ��ġ�� �׾����� �˾�?",
            "����! �׷�, �׷��ٸ� ���ư� ����� ����� ����ڳ�.",
            "������ ���� ������ ���Ұ� ����? �� ������."});
    }

    // ���� ���۽� �̾߱� ����
    public void StartTalk(int curStage)
    {
        // ���� �������� �޾ƿ���
        stage = curStage;
        // �ε��� �ʱ�ȭ
        talkIndex = 0;
        //����Ʈ �ʱ�ȭ
        effectCom = false;

        // ���������� ���� �� �̹��� ����
        if (stage == 1)
            enemyImage.sprite = enemySprites[0];
        else if(stage == 2)
            enemyImage.sprite = enemySprites[1];
        else if (stage == 3)
            enemyImage.sprite = enemySprites[2];
        else if (stage == 4)
            enemyImage.sprite = enemySprites[3];

        // ����Ʈ ����
        StartCoroutine(Typing());
    }

    // ��ư Ŭ��
    public void OnClickNextTalk()
    {
        // ����Ʈ�� �������� ���� ��ȭ��
        if (effectCom)
        {
            talkIndex++;
            ChangeImage(stage, talkIndex);
            // ��ȭ�� �������̸� ���� ����
            if (GetTalk(stage, talkIndex) == null)
            {
                stage1Battle.state = BattleState.PlayerTurn;
                stage1Battle.PlayerTurn();
                gameObject.SetActive(false);
                return;
            }
            effectCom = false;
            StartCoroutine(Typing());
        }
        // �ƴϸ� ��ŵ
        else 
        {
            StopCoroutine(Typing());
            talkText.text = GetTalk(stage, talkIndex);
            effectCom = true;
        }
    }
    // ��ȭ�� ���� ĳ���� ��� ����
    public void ChangeImage(int stage, int index)
    {
        switch (stage)
        {
            case 1:
                if (index == 4 || index == 6)
                {
                    playerImage.color = dColor;
                    enemyImage.color = wColor;
                }
                else
                {
                    playerImage.color = wColor;
                    enemyImage.color = dColor;
                }
                break;
            case 2:
                if (index == 3 || index == 6 || index == 8)
                {
                    playerImage.color = dColor;
                    enemyImage.color = wColor;
                }
                else
                {
                    playerImage.color = wColor;
                    enemyImage.color = dColor;
                }
                break;
            case 3:
                if (index == 3 || index == 6)
                {
                    playerImage.color = dColor;
                    enemyImage.color = wColor;
                }
                else
                {
                    playerImage.color = wColor;
                    enemyImage.color = dColor;
                }
                break;
            case 4:
                if (index == 3 || index == 5 || index == 7 || index == 8)
                {
                    playerImage.color = dColor;
                    enemyImage.color = wColor;
                }
                else
                {
                    playerImage.color = wColor;
                    enemyImage.color = dColor;
                }
                break;
        }
    }

    // count Key, talkIndex value, ���� ��ȭ���� ��������
    public string GetTalk(int count, int talkIndex)
    {
        if (talkIndex == talkData[count].Length)
        {
            return null;
        }
        else
            return talkData[count][talkIndex];
    }
    IEnumerator Typing()
    {
        talkText.text = "";
        string text = GetTalk(stage, talkIndex);
        for (int i = 0; i < text.Length; i++)
        {
            if (talkText.text == text)
            {
                yield break;
            }
            if (text[i]=='<')
            {
                talkText.text = text;
                yield break;
            }
            talkText.text += text[i];
            yield return new WaitForSeconds(0.05f);
        }
        effectCom = true;
    }
}
