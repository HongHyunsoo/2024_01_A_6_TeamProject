using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   
using UnityEngine.Audio;

public class AudioMixerController : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;  //private 선언한 것들을 인스팩터 창에서 보여지게
    [SerializeField] private Slider MusicMasterSlider;  //UI Slider
    [SerializeField] private Slider MusicBGMSlider;  //UI Slider
    [SerializeField] private Slider MusicSFXSlider;  //UI Slider
    //슬라이더 MinValue를 0.001

    private void Awake()
    {
        MusicMasterSlider.onValueChanged.AddListener(SetMasterVolume);  //UI Slider의 값이 변경되었을 경우 SetMasterVolume 함수를 호출 한다.
        MusicMasterSlider.onValueChanged.AddListener(SetBGMVolume);  //UI Slider의 값이 변경되었을 경우 SetBGMVolume 함수를 호출 한다.
        MusicMasterSlider.onValueChanged.AddListener(SetSFXVolume);  //UI Slider의 값이 변경되었을 경우 SetSFXVolume 함수를 호출 한다.
    }
    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("Master",Mathf.Log(volume) * 20);       //볼륨에서의 0 ~ 1 <- Mathf.Log10(valume) * 20
    }
    public void SetBGMVolume(float volume)
    {
        audioMixer.SetFloat("BGM", Mathf.Log(volume) * 20);       //볼륨에서의 0 ~ 1 <- Mathf.Log10(valume) * 20
    }
    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFX", Mathf.Log(volume) * 20);       //볼륨에서의 0 ~ 1 <- Mathf.Log10(valume) * 20
    }
}
