using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public AudioMixerGroup musicGroup;
    public AudioMixerGroup sfxGroup;
    public AudioMixerGroup uiGroup;
    public AudioMixerGroup voicesGroup;

    [Header("Sounds Library")]
    public Sound[] musics;
    public Sound[] sfx;
    public Sound[] ui;
    public Sound[] voices;

    public static AudioManager instance;

    private Sound currentMusic;

    private Dictionary<string, AudioSource> SoundsPlaying;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (SceneManager.GetActiveScene().name != "Menu")
        {
            Destroy(instance.gameObject);
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        
        DontDestroyOnLoad(gameObject);

        SoundsPlaying = new Dictionary<string, AudioSource>();

        InitializeAudioSources(musics);
        InitializeAudioSources(sfx);
        InitializeAudioSources(ui);
        InitializeAudioSources(voices);
    }

    private void InitializeAudioSources(Sound[] sounds)
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.outputAudioMixerGroup = sfxGroup;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private void Update()
    {
        RemoveSoundsNotPlaying();
    }

    public void Play(string name, bool saveAudio = false)
    {
        AudioSource audio = null;
        try
        {
            Sound s = Array.Find(sfx, sound => sound.name == name);
            s.source.Play();
            audio = s.source;
        }
        catch
        {
            Debug.Log(name + " not found in sfx");
        }

        try
        {
            Sound u = Array.Find(ui, sound => sound.name == name);
            u.source.Play();
            audio = u.source;
        }
        catch
        {
            Debug.Log(name + " not found in ui");
        }

        try
        {
            Sound v = Array.Find(voices, sound => sound.name == name);
            v.source.Play();
            audio = v.source;
        }
        catch
        {
            Debug.Log(name + " not found in voices");
        }

        if(saveAudio)
            AddSoundsPlaying(name, audio);

        try
        {

            Sound nextMusic = Array.Find(musics, sound => sound.name == name);
            Debug.Log(nextMusic.name);
            currentMusic = nextMusic;
            currentMusic.source.volume = 1;
            currentMusic.source.Play();

            return;
        }
        catch
        {
            Debug.Log(name + " not found in musics");
        }
    }

    public void StopMusic()
    {
        if (currentMusic.source.isPlaying)
        {
            currentMusic.source.Stop();
        }
    }

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

    public bool getAudioIsPlaying(string name)
    {
        if (SoundsPlaying.ContainsKey(name))
            return true;
        return false;
    }

    public bool getMusicIsPlaying()
    {
        if (currentMusic != null && currentMusic.source.isPlaying)
            return true;
        return false;
    }

    private void AddSoundsPlaying(string name, AudioSource source)
    {
        if (source == null)
            return;
        SoundsPlaying.Add(name, source);
        Debug.Log("Salvo");
    }

    private void RemoveSoundsNotPlaying()
    {
        List<string> keys = new List<string>();
        foreach (KeyValuePair<string, AudioSource> kvp in SoundsPlaying)
        {
            if (!kvp.Value.isPlaying)
            {
                keys.Add(kvp.Key);
            }
        }

        foreach(string key in keys)
        {
            SoundsPlaying.Remove(key);
        }
    }

    public void StopSound(string name)
    {
        if (SoundsPlaying.ContainsKey(name))
        {
            SoundsPlaying[name].Stop();
        }
    }
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
