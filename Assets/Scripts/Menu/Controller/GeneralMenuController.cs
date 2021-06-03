using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralMenuController : MonoBehaviour
{
    public Animator QuitGame;
    public Animator BackMenu;

    public GameObject configurations;

    public void BackMenuButton()
    {
        configurations.SetActive(false);
        QuitGame.SetBool("AppearBox", false);
        AudioManager.instance.Play("ClickButton");
        BackMenu.SetBool("AppearBox", true);
    }

    public void DontExitMenu()
    {
        AudioManager.instance.Play("ClickButton");
        BackMenu.SetBool("AppearBox", false);
    }

    public void GoMenu()
    {
        GameScenes.instance.SetNextScene(0);
        GameScenes.instance.LoadLevel(0);
    }

    public void QuitButton()
    {
        configurations.SetActive(false);
        BackMenu.SetBool("AppearBox", false);
        AudioManager.instance.Play("ClickButton");
        QuitGame.SetBool("AppearBox", true);
    }

    public void DontExitGame()
    {
        AudioManager.instance.Play("ClickButton");
        QuitGame.SetBool("AppearBox", false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void OpenConfigurations()
    {
        configurations.SetActive(true);
        BackMenu.SetBool("AppearBox", false);
        QuitGame.SetBool("AppearBox", false);
    }

    public void BackGameButton()
    {
        configurations.SetActive(false);
        QuitGame.SetBool("AppearBox", false);
        BackMenu.SetBool("AppearBox", false);
        AudioManager.instance.Play("ClickButton");
        gameObject.SetActive(false);
    }
}
