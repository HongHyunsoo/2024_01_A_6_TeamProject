using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]       //직열화 시켜준다. (유니티 안에서 데이터를 인스팩터창에서 나타나게 보여주는 기능)
public class Sound
{
    public string name;         //사운드의 이름
    public AudioClip clip;      //사운드 클립

    [Range(0f, 1f)]             //인스팩터 화면에서 0과 1사이 값을 선택할 수 있는 슬라이더로 변환
    public float volume = 1.0f;

    [Range(0f, 3f)]             //인스팩터 화면에서 0과 3사이 값을 선택할 수 있는 슬라이더로 변환
    public float pitch = 1.0f;  //사운드 패치
    public bool loop;           //반복재생 여부
    public AudioMixerGroup mixerGroup;  //오디오 믹서 그룹

    [HideInInspector]                   //인스팩터 창에서 안보이게 해주는 기능
    public AudioSource sources;         //오디오 소스
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;                //싱글톤 인스턴스 (static은 전역 변수로 올려서 어디서든 접근할 수 있게 해준다)
    public List <Sound> sounds = new List <Sound>();    //사운드 리스트 관리 List 자료 구조로 관리
    public AudioMixer audioMixer;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);      //Scene이 변경되어도 (이 오브젝트는) 파괴되지 않는다.
        }
        else
        {
            Destroy(gameObject);                //이미 다른 오브젝트가 있을 경우 파괴한다. 클래스를 전역으로 1개를 유지 시킬 수 있다.
        }

        foreach (Sound sound in sounds)
        {
            sound.sources = gameObject.AddComponent<AudioSource>();     //AddComponent는 Class 컴포넌트를 오브젝트에 붙인다.
            sound.sources.clip = sound.clip;
            sound.sources.volume = sound.volume;
            sound.sources.pitch = sound.pitch;
            sound.sources.loop = sound.loop;
            sound.sources.outputAudioMixerGroup = sound.mixerGroup;     //List에 있는 사운드 객체를 (생성된) <AudioSource> 에 1개씩 값을 전달한다.
        }

       

    }
    //사운드를 재생하는 함수
    public void PlaySound(string name)                                  //인수로 사운드 name을 받고
    {
        Sound soundToPlay = sounds.Find(sound => sound.name == name);   //사운드 List에서 name 찾아서 Sound를 찾는다
        if (soundToPlay != null)
        {

            soundToPlay.sources.Play();                                 //사운드 재생
        }
        else
        {
            Debug.LogWarning("사운드" + name + "찾을 수 없다.");        //사운드가 없다는 경고 로고
        }
    }

    public void StopSound(string name)                                  //인수로 사운드 name을 받고
    {
        Sound soundToStop = sounds.Find(sound => sound.name == name);   //사운드 List에서 name 찾아서 Sound를 찾는다
        if (soundToStop != null)
        {

            soundToStop.sources.Stop();                                 //사운드 재생
        }
    }

}
