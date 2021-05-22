
using System.Collections.Generic; 
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] Sound[] sounds;

    private Dictionary<string, Sound> _soundDict;
    void Awake()
    {
        InitializeAudioSources();
    }

    private void InitializeAudioSources(){
        foreach(Sound sound in sounds){
            sound.source = gameObject.AddComponent<AudioSource>();
            _soundDict.Add(sound.name, sound);
        }
    }

    public void Play(string soundName){
        if(_soundDict.ContainsKey(soundName)){
            _soundDict[soundName].Play();
        }
        else{
            Debug.LogError("Unknown sound: " + soundName);
        }
    }

}
