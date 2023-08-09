using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour
{
    public AudioMixer audioMixer;


    public AudioSource audioSource;
    public AudioClip click;
    // 오디오 조절 슬라이더
    // 오디오 믹서 최소 ~ 최대 볼륨값은 -80 ~ 0
    [SerializeField]
    private Slider masterSlider;
    [SerializeField]
    private Slider bgmSlider;
    [SerializeField]
    private Slider sfxSlider;

    // 토글 볼륨 ON ~ OFF
    [SerializeField]
    private Toggle masterToggleON;
    [SerializeField]
    private Toggle masterToggleOFF;
    [SerializeField]
    private Toggle bgmToggleON;
    [SerializeField]
    private Toggle bgmToggleOFF;
    [SerializeField]
    private Toggle sfxToggleON;
    [SerializeField]
    private Toggle sfxToggleOFF;

    private void Start()
    {
        // 사운드매니저 볼륨값 저장
        masterSlider.value = SoundManager.soundInstance.masterVolume;
        bgmSlider.value = SoundManager.soundInstance.bgmVolume;
        sfxSlider.value = SoundManager.soundInstance.sfxVolume;

        // toggle 초기화시 초기값이랑 다르면 OnChangeToggle()함수 실행으로 인한 조건 체크
        if (!SoundManager.soundInstance.masterVolumeOn)
        {
            masterToggleOFF.isOn = true;
            masterSlider.interactable = false;
        }

        if (!SoundManager.soundInstance.bgmVolumeOn)
        {
            bgmToggleOFF.isOn = true;
            bgmSlider.interactable = false;
        }

        if (!SoundManager.soundInstance.sfxVolumeOn)
        {
            sfxToggleOFF.isOn = true;
            sfxSlider.interactable = false;
        }

        gameObject.SetActive(false);
    }
    
    //슬라이더로 볼륨 조절
    public void SetMasterVolume(float sliderValue)
    {
        audioMixer.SetFloat("Master", sliderValue);
        SoundManager.soundInstance.masterVolume = masterSlider.value;
    }

    public void SetBGMVolume(float sliderValue)
    {
        audioMixer.SetFloat("BGM", sliderValue);
        SoundManager.soundInstance.bgmVolume = bgmSlider.value;
    }

    public void SetSFXVolume(float sliderValue)
    {
        audioMixer.SetFloat("SFX", sliderValue);
        SoundManager.soundInstance.sfxVolume = sfxSlider.value;
    }
    
    // 토글로 볼륨 ON - OFF
    public void SetMasterVolumeToggle(bool On)
    {
        float sound = SoundManager.soundInstance.masterVolume;
        if (On)
        {
            audioMixer.SetFloat("Master", sound);
            masterSlider.interactable = true;
        }
        else
        {
            audioMixer.SetFloat("Master", -80);
            masterSlider.interactable = false;
        }

        SoundManager.soundInstance.masterVolumeOn = On;
    }
    public void SetBGMVolumeToggle(bool On)
    {
        float sound = SoundManager.soundInstance.bgmVolume;
        if (On)
        {
            audioMixer.SetFloat("BGM", sound);
            bgmSlider.interactable = true;
        }
        else
        {
            audioMixer.SetFloat("BGM", -80);
            bgmSlider.interactable = false;
        }

        SoundManager.soundInstance.bgmVolumeOn = On;
    }
    public void SetSFXVolumeToggle(bool On)
    {
        float sound = SoundManager.soundInstance.sfxVolume;
        if (On)
        {
            audioMixer.SetFloat("SFX", sound);
            sfxSlider.interactable = true;
        }
        else
        {
            audioMixer.SetFloat("SFX", -80);
            sfxSlider.interactable = false;
        }

        SoundManager.soundInstance.sfxVolumeOn = On;
    }

    public void OnCLickReturn()
    {
        audioSource.clip = click;
        audioSource.Play();
        gameObject.SetActive(false);
    }
}
