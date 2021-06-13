using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackMenuController : MonoBehaviour
{
    private Animator anim;
    private List<string> sounds;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        sounds = new List<string>();
    }

    public void InitalizeMusic(string music)
    {
        AudioManager.instance.Play(music);
    }

    public void InitializeSound(string speech)
    {
        sounds.Add(speech);
        AudioManager.instance.Play(speech, true);
    }

    public void BackMenu()
    {
        foreach(string sound in sounds)
        {
            AudioManager.instance.StopSound(sound);
        }
        anim.SetTrigger("FadeOut");
        StartCoroutine(AudioManager.instance.FadeOut(-0.05f));
        GameScenes.instance.SetNextScene(0);
        GameScenes.instance.LoadLevel(1.5f);
    }
}
