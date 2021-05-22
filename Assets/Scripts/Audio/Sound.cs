using UnityEngine.Audio;

using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip audioClip;
    [Range(0f, 1f)]
    public float volume = 1f;
    [Range(.1f, 3f)]
    public float pitch = 1f;
    public bool loop;
    public bool playOnStart;

    public AudioSource source{
        get => _source;
        set => SetSource(value);
    }

    private AudioSource _source;

    private void SetSource(AudioSource audioSource){
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.pitch = pitch;
        audioSource.loop = loop;
        _source = audioSource;

        if (playOnStart){
            Play();
        }
    }

    public void Play(){
        source.Play();
    }
}
