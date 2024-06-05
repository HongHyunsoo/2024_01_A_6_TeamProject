using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]       //����ȭ �����ش�. (����Ƽ �ȿ��� �����͸� �ν�����â���� ��Ÿ���� �����ִ� ���)
public class Sound
{
    public string name;         //������ �̸�
    public AudioClip clip;      //���� Ŭ��

    [Range(0f, 1f)]             //�ν����� ȭ�鿡�� 0�� 1���� ���� ������ �� �ִ� �����̴��� ��ȯ
    public float volume = 1.0f;

    [Range(0f, 3f)]             //�ν����� ȭ�鿡�� 0�� 3���� ���� ������ �� �ִ� �����̴��� ��ȯ
    public float pitch = 1.0f;  //���� ��ġ
    public bool loop;           //�ݺ���� ����
    public AudioMixerGroup mixerGroup;  //����� �ͼ� �׷�

    [HideInInspector]                   //�ν����� â���� �Ⱥ��̰� ���ִ� ���
    public AudioSource sources;         //����� �ҽ�
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;                //�̱��� �ν��Ͻ� (static�� ���� ������ �÷��� ��𼭵� ������ �� �ְ� ���ش�)
    public List <Sound> sounds = new List <Sound>();    //���� ����Ʈ ���� List �ڷ� ������ ����
    public AudioMixer audioMixer;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);      //Scene�� ����Ǿ (�� ������Ʈ��) �ı����� �ʴ´�.
        }
        else
        {
            Destroy(gameObject);                //�̹� �ٸ� ������Ʈ�� ���� ��� �ı��Ѵ�. Ŭ������ �������� 1���� ���� ��ų �� �ִ�.
        }

        foreach (Sound sound in sounds)
        {
            sound.sources = gameObject.AddComponent<AudioSource>();     //AddComponent�� Class ������Ʈ�� ������Ʈ�� ���δ�.
            sound.sources.clip = sound.clip;
            sound.sources.volume = sound.volume;
            sound.sources.pitch = sound.pitch;
            sound.sources.loop = sound.loop;
            sound.sources.outputAudioMixerGroup = sound.mixerGroup;     //List�� �ִ� ���� ��ü�� (������) <AudioSource> �� 1���� ���� �����Ѵ�.
        }

       

    }
    //���带 ����ϴ� �Լ�
    public void PlaySound(string name)                                  //�μ��� ���� name�� �ް�
    {
        Sound soundToPlay = sounds.Find(sound => sound.name == name);   //���� List���� name ã�Ƽ� Sound�� ã�´�
        if (soundToPlay != null)
        {

            soundToPlay.sources.Play();                                 //���� ���
        }
        else
        {
            Debug.LogWarning("����" + name + "ã�� �� ����.");        //���尡 ���ٴ� ��� �ΰ�
        }
    }

    public void StopSound(string name)                                  //�μ��� ���� name�� �ް�
    {
        Sound soundToStop = sounds.Find(sound => sound.name == name);   //���� List���� name ã�Ƽ� Sound�� ã�´�
        if (soundToStop != null)
        {

            soundToStop.sources.Stop();                                 //���� ���
        }
    }

}
