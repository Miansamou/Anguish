using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    #region variables

    public static AudioManager instance;

    [Header("Sounds Groups")]
    [SerializeField]
    private AudioMixerGroup musicGroup;
    [SerializeField]
    private AudioMixerGroup sfxGroup;
    [SerializeField]
    private AudioMixerGroup uiGroup;
    [SerializeField]
    private AudioMixerGroup voicesGroup;

    [Header("Sounds Library")]
    [SerializeField]
    private Sound[] musics;
    [SerializeField]
    private Sound[] sfx;
    [SerializeField]
    private Sound[] ui;
    [SerializeField]
    private Sound[] voices;

    private Sound currentMusic;
    private Dictionary<string, AudioSource> soundsPlaying;

    #endregion

    #region initialization

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        
        DontDestroyOnLoad(instance);

        soundsPlaying = new Dictionary<string, AudioSource>();

        InitializeAudioSources(musics, musicGroup);
        InitializeAudioSources(sfx, sfxGroup);
        InitializeAudioSources(ui, uiGroup);
        InitializeAudioSources(voices, voicesGroup);
    }

    private void InitializeAudioSources(Sound[] sounds, AudioMixerGroup groupMixer)
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.outputAudioMixerGroup = groupMixer;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    #endregion

    #region manager sounds

    private void Update()
    {
        RemoveSoundsNotPlaying();
    }

    public void Play(string name, bool saveAudio = false, bool playPaused = false)
    {
        AudioSource audio = null;
        Sound s = null;

        if (playPaused && soundsPlaying.ContainsKey(name))
        {
            soundsPlaying[name].Play();
            return;
        }

        s = Array.Find(sfx, sound => sound.name == name);
        if(s != null)
        {
            s.source.Play();
            audio = s.source;
        }

        s = Array.Find(ui, sound => sound.name == name);
        if (s != null)
        { 
            s.source.Play();
            audio = s.source;
        }

        s = Array.Find(voices, sound => sound.name == name);
        if (s != null)
        { 
            s.source.Play();
            audio = s.source;
        }

        if(saveAudio)
            AddSoundsPlaying(name, audio);

        s = Array.Find(musics, sound => sound.name == name);
        if(s != null)
        {
        currentMusic = s;
        currentMusic.source.volume = 1;
        currentMusic.source.Play();
        }
    }

    public void StopMusic()
    {
        if (currentMusic.source.isPlaying)
        {
            currentMusic.source.Stop();
        }
    }

    public void StopSound(string name)
    {
        if (soundsPlaying.ContainsKey(name))
        {
            soundsPlaying[name].Stop();
        }
    }

    public void PauseSound(string name)
    {
        if (soundsPlaying.ContainsKey(name))
        {
            soundsPlaying[name].Pause();
        }
    }

    private void RemoveSoundsNotPlaying()
    {
        List<string> keys = new List<string>();
        foreach (KeyValuePair<string, AudioSource> kvp in soundsPlaying)
        {
            if (!kvp.Value.isPlaying)
            {
                keys.Add(kvp.Key);
            }
        }

        foreach (string key in keys)
        {
            soundsPlaying.Remove(key);
        }
    }

    private void AddSoundsPlaying(string name, AudioSource source)
    {
        if (source == null)
            return;
        soundsPlaying.Add(name, source);
    }

    #endregion

    #region get/set
    public bool GetAudioIsPlaying(string name)
    {
        if (soundsPlaying.ContainsKey(name))
            return true;
        return false;
    }

    public bool GetMusicIsPlaying()
    {
        if (currentMusic != null && currentMusic.source.isPlaying)
            return true;
        return false;
    }

    #endregion

    #region effects

    public IEnumerator FadeOut(float speed)
    {
        float audioVolume = currentMusic.source.volume;

        while (audioVolume >= 0)
        {
            audioVolume += speed;
            currentMusic.source.volume = audioVolume;
            yield return new WaitForSeconds(0.1f);
        }
    }

    #endregion
}

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    public bool loop;

    [Range(0f, 1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;

    [HideInInspector]
    public AudioSource source;
}
