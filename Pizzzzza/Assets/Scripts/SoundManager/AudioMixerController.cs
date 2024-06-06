using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   
using UnityEngine.Audio;

public class AudioMixerController : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;  //private ������ �͵��� �ν����� â���� ��������
    [SerializeField] private Slider MusicBGMSlider;  //UI Slider
    [SerializeField] private Slider MusicSFXSlider;  //UI Slider
    //�����̴� MinValue�� 0.001

    private void Awake()
    {
        MusicBGMSlider.onValueChanged.AddListener(SetBGMVolume);  //UI Slider�� ���� ����Ǿ��� ��� SetBGMVolume �Լ��� ȣ�� �Ѵ�.
        MusicSFXSlider.onValueChanged.AddListener(SetSFXVolume);  //UI Slider�� ���� ����Ǿ��� ��� SetSFXVolume �Լ��� ȣ�� �Ѵ�.
    }
   
    public void SetBGMVolume(float volume)
    {
        audioMixer.SetFloat("BGM", Mathf.Log(volume) * 10);       //���������� 0 ~ 1 <- Mathf.Log10(valume) * 20
    }
    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFX", Mathf.Log(volume) * 10);       //���������� 0 ~ 1 <- Mathf.Log10(valume) * 20
    }
}
