using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralMenuController : MonoBehaviour
{
    public Animator QuitGame;
    public Animator BackMenu;

    private AudioManager audioManager;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.Play("MenuMusic");
    }

    public void BackMenuButton()
    {
        QuitGame.SetBool("AppearBox", false);
        audioManager.Play("ClickButton");
        BackMenu.SetBool("AppearBox", true);
    }

    public void DontExitMenu()
    {
        audioManager.Play("ClickButton");
        BackMenu.SetBool("AppearBox", false);
    }

    public void GoMenu()
    {
        GameScenes.instance.setNextScene(0);
        GameScenes.instance.LoadLevel(0);
    }

    public void QuitButton()
    {
        BackMenu.SetBool("AppearBox", false);
        audioManager.Play("ClickButton");
        QuitGame.SetBool("AppearBox", true);
    }

    public void DontExitGame()
    {
        audioManager.Play("ClickButton");
        QuitGame.SetBool("AppearBox", false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void BackGameButton()
    {
        QuitGame.SetBool("AppearBox", false);
        BackMenu.SetBool("AppearBox", false);
        audioManager.Play("ClickButton");
        gameObject.SetActive(false);
    }
}
