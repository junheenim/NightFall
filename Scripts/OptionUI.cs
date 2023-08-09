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
    // ����� ���� �����̴�
    // ����� �ͼ� �ּ� ~ �ִ� �������� -80 ~ 0
    [SerializeField]
    private Slider masterSlider;
    [SerializeField]
    private Slider bgmSlider;
    [SerializeField]
    private Slider sfxSlider;

    // ��� ���� ON ~ OFF
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
        // ����Ŵ��� ������ ����
        masterSlider.value = SoundManager.soundInstance.masterVolume;
        bgmSlider.value = SoundManager.soundInstance.bgmVolume;
        sfxSlider.value = SoundManager.soundInstance.sfxVolume;

        // toggle �ʱ�ȭ�� �ʱⰪ�̶� �ٸ��� OnChangeToggle()�Լ� �������� ���� ���� üũ
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
    
    //�����̴��� ���� ����
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
    
    // ��۷� ���� ON - OFF
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
