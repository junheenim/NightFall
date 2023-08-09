using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    // 음악 종류 열거
    public enum SoundList
    {
        MAINMENU,
        //...
    }
    // sound 관리를 위한 Singleton 패턴
    static public SoundManager soundInstance;

    public AudioMixer audioMixer;

    public AudioSource audioSource;
    // 배경음악 배열로 저장 상황에 따라 변환
    public AudioClip[] audio_BGM;

    public float masterVolume;
    public float bgmVolume;
    public float sfxVolume;

    public bool masterVolumeOn;
    public bool bgmVolumeOn;
    public bool sfxVolumeOn;

    // 객체 생성시 최소 실행(싱글톤 생성)
    private void Awake()
    {
        // 최초 생성시 soundInstance가 null 이라면
        if (soundInstance == null && soundInstance != this)
        {
            soundInstance = this;
            // Scene이 넘어가도 object 파괴되지 않도록함
            DontDestroyOnLoad(gameObject);
        }
        // 아니면 this로 변경
        else
        {
            Destroy(gameObject);
        }

        // 나중 저장 데이터 Load 대비
        masterVolume = 0;
        bgmVolume = 0;
        sfxVolume = 0;

        masterVolumeOn = true;
        bgmVolumeOn = true;
        sfxVolumeOn = true;
    }
    
    private void Start()
    {
        // 오디오 믹서 값 추가
        audioMixer.SetFloat("Master", masterVolumeOn ? masterVolume : -80);
        audioMixer.SetFloat("BGM", bgmVolumeOn ? bgmVolume : -80);
        audioMixer.SetFloat("SFX", sfxVolumeOn ? sfxVolume : -80);
    }

    public void PlayBGMAudio(int n)
    {
        audioMixer.SetFloat("Master", masterVolumeOn ? masterVolume : -80);
        audioSource.clip = audio_BGM[n];
        audioSource.Play();
    }

    public void StopBGM()
    {
        audioSource.Stop();
    }

    public IEnumerator SoundFade()
    {
        StopCoroutine("SoundFade");
        if(!masterVolumeOn)
            yield break;
        float sound = masterVolume;
        while (sound > -40f)
        {
            sound -= 0.5f;
            yield return new WaitForSeconds(0.02f);
            audioMixer.SetFloat("Master", sound);
        }
        audioMixer.SetFloat("Master", -80);
    }
}
