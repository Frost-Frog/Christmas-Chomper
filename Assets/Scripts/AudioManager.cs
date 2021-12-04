using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    //Scene activescene;
    public AudioMixerGroup audioMixer;
    public static AudioManager instance;
    public Sound[] sounds;    // Start is called before the first frame update
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    
        DontDestroyOnLoad(gameObject);
        
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.outputAudioMixerGroup = audioMixer;
            s.source.loop = s.loop;
        }
    }
    void Start()
    { 
        //SceneManager.sceneLoaded += PlayGameTheme;
        Play("Walk");
    }
    public void PlayGameTheme()
    {
        //Scene activescene = SceneManager.GetActiveScene();
        //if(activescene.name == "PongPlay" )
        //{
            //Stop("MenuTheme");
            //Play("GameTheme");
        //}
        
    }
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }

    // Update is called once per frame
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }
}
