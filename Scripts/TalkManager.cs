using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkManager : MonoBehaviour
{
    // 대화 저장
    Dictionary<int, string[]> talkData;
    // 플레이어 이미지 창
    public Image playerImage;
    // 몬스터 이미지 창
    public Image enemyImage;
    // 몬스터 이미지
    public Sprite[] enemySprites;
    // ImageColor
    Color wColor = new Color(1, 1, 1);
    Color dColor = new Color(0.4f, 0.4f, 0.4f);
    // 대화창
    public Text talkText;
    // 정보 가져오기
    public Stage1Battle stage1Battle;
    
    // 스테이지 정보
    int stage;
    // 대화 정보
    int talkIndex;
    // 이펙트 완료
    bool effectCom;

    private void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        GenerateData();
    }
    
    //대화 생성
    void GenerateData()
    {
        // key : 스테이지, value : 대사
        talkData.Add(1, new string[] { 
            "마을에서 '낮이 계속되는 밤', 백야가 시작된 이후 늑대가 물어 죽인 사람 수가 셋을 넘어갔다.",
            "얼마 후 늑대는 모습을 드러냈다.",
            "풀숲 사이에서 나타난 늑대는 사냥을 준비하는 자세였다.",
            "그르릉...",
            "위험해!",
            "먹이! 먹이! 먹이!",
            "늑대는 시뻘겋게 달아오른 눈이 광기로 이글거리고, 입가에는 흰 거품이 들끓었다. ",
            "위협적인 기세와 달리 굶주려 말라붙은 털가죽과 가벼워진 몸이 휘청거렸다.",
            "늑대는 대화할 수 없었지만, 일행 모두 늑대의 이유를 알았다.",
            "<color=#B22222><size=90>굶주림</size></color>",
            "계속되는 불면과 굶주림으로 늑대는 달려든 것이다."});

        talkData.Add(2, new string[] {
            "설산에 발을 들이자마자 눈이 갈라지며 깊고 좁은절벽이 생겨나 죽을 고비를 넘겨야 했다.",
            "<color=#00BFFF><size=90>크레바스</size></color>",
            "겨우 피해낸 일행이 몸을 추리는데, 산과 같이 컨다란 곰이 나타나 물었다.",
            "돌아가라, 인간이여. 설산은 그대들을 반기지 않는다.",
            "커다란 눈 뭉치 같은 몸에 흉터와 푸른 눈. 마을에서 들었던 대로였다.",
            "만년 설산의 수호자시여, 하나 저희는 백야를 고치기 위해 움직여야 합니다. 부디 아량을 베푸소서.",
            "아량? 인간이 만들어 낸 재앙에 내 산의 아이들이 잠겨 죽고, 익어죽는다. 그런데 내게 아량을 베풀어라?",
            "그의 말과 동시에 바위와 같던 눈이 쩍 갈라졌다.",
            "만년 설산은 더 이상 만년조차 아니다, 아둔한 인간이여! 그대들은 그대 자신의 죄를 알고 더 이상 설산을 어지럽히지 말라!",
            "울라는 가죽이 두꺼워 물리적인 피해는 효과가 줄어들꺼야"});

        talkData.Add(3, new string[] {
            "어두컴컴한 던전 속에 천천히 발을 내디뎠다.",
            "밖은 한낮임에도 던전의 안은 을씨년스럽고 스산했다.",
            "멀리, 마왕 성이 보이자 마침내 긴장이 슬그머니 풀리려던 찰나, 그림자 같은 목소리가 물었다.",
            "거기까지. 여기는 인간에게 금지된 땅이다.",
            "<color=#4B0082><size=90>살아있는 저주, 삿됨, 어둠.</size></color>",
            "움직임을 따라 짙어지는 마기에 그가 마왕의 심복임을 한눈에 알아보았다.",
            "그림자는 차근히 화를 더하듯, 억울해하듯, 한이 맺힌 듯 말을 내뱉었다.",
            "자연의 배신자들은 여기서 잠드리라.",
            "담담할 만치 다듬어진 분노와 살의에 일행은 몸서리치며 다가올 전투를 준비했다.",
            "네크로맨서는 마법방어막을 두르고있어!"});

        talkData.Add(4, new string[] {
            "간신히 도착한 마왕 성의 중심부는 텅 비어있었다. 허탈함에 주위를 둘러보지만, 온통 낡은 건물과 먼지뿐, 대답마저 구할 수 없었다.",
            "일행 역시 허망해하는 데, 한켠에서 어린아이의 목소리가 들려왔다.",
            "너무 어리고 밝아서, 이질적인 목소리가.",
            "안녕? 드디어 왔구나. 기다렸어, 용사님",
            "…네가, 네가 <color=#FF69B4><size=90>마왕?</size></color>",
            "응, 날 이기면 저주를 풀어줄게. 대신 지면 아주 혼나야 해.",
            "이러는 이유가, 뭐야? 네 저주 때문에 얼마나 많은 이들이 다치고 죽었는지 알아?",
            "꺄핫! 그래, 그렇다면 교훈과 명분은 충분히 얻었겠네.",
            "나에게 너의 공격이 통할거 같아? 얼른 끝내자."});
    }

    // 전투 시작시 이야기 시작
    public void StartTalk(int curStage)
    {
        // 현재 스테이지 받아오기
        stage = curStage;
        // 인덱스 초기화
        talkIndex = 0;
        //이펙트 초기화
        effectCom = false;

        // 스테이지에 따른 적 이미지 변경
        if (stage == 1)
            enemyImage.sprite = enemySprites[0];
        else if(stage == 2)
            enemyImage.sprite = enemySprites[1];
        else if (stage == 3)
            enemyImage.sprite = enemySprites[2];
        else if (stage == 4)
            enemyImage.sprite = enemySprites[3];

        // 이펙트 시작
        StartCoroutine(Typing());
    }

    // 버튼 클릭
    public void OnClickNextTalk()
    {
        // 이펙트가 끝났으면 다음 대화로
        if (effectCom)
        {
            talkIndex++;
            ChangeImage(stage, talkIndex);
            // 대화가 마지막이면 전투 시작
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
        // 아니면 스킵
        else 
        {
            StopCoroutine(Typing());
            talkText.text = GetTalk(stage, talkIndex);
            effectCom = true;
        }
    }
    // 대화에 따른 캐릭터 명암 조정
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

    // count Key, talkIndex value, 다음 대화내용 가져오기
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
