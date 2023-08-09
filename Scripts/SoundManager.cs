using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    // ���� ���� ����
    public enum SoundList
    {
        MAINMENU,
        //...
    }
    // sound ������ ���� Singleton ����
    static public SoundManager soundInstance;

    public AudioMixer audioMixer;

    public AudioSource audioSource;
    // ������� �迭�� ���� ��Ȳ�� ���� ��ȯ
    public AudioClip[] audio_BGM;

    public float masterVolume;
    public float bgmVolume;
    public float sfxVolume;

    public bool masterVolumeOn;
    public bool bgmVolumeOn;
    public bool sfxVolumeOn;

    // ��ü ������ �ּ� ����(�̱��� ����)
    private void Awake()
    {
        // ���� ������ soundInstance�� null �̶��
        if (soundInstance == null && soundInstance != this)
        {
            soundInstance = this;
            // Scene�� �Ѿ�� object �ı����� �ʵ�����
            DontDestroyOnLoad(gameObject);
        }
        // �ƴϸ� this�� ����
        else
        {
            Destroy(gameObject);
        }

        // ���� ���� ������ Load ���
        masterVolume = 0;
        bgmVolume = 0;
        sfxVolume = 0;

        masterVolumeOn = true;
        bgmVolumeOn = true;
        sfxVolumeOn = true;
    }
    
    private void Start()
    {
        // ����� �ͼ� �� �߰�
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
