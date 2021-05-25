using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public class MenuController : MonoBehaviour
{
    public InputActionAsset Controller;
    public Animator ExitBox;
    public VisualEffect fog;
    public Camera cam;
    public GameObject YmirBox;

    private AudioManager audioManager;
    private InputAction anyKey;
    private InputAction acceptKey;
    private Animator animator;
    private DialogueLine IntroDialogue;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.Play("MenuMusic");

        InputActionMap Map = Controller.FindActionMap("UIController");

        IntroDialogue = YmirBox.GetComponentInChildren<DialogueLine>();

        anyKey = Map.FindAction("AnyKey");
        acceptKey = Map.FindAction("Accept");

        anyKey.Enable();
        acceptKey.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        AnyKeyScreen();

        YmirDialogue();
    }

    public void AnyKeyScreen()
    {
        if (anyKey.triggered)
        {
            audioManager.Play("DarkEntering");
            animator.SetTrigger("HomeMenu");
            anyKey.Disable();
        }
    }

    public void PlayButton()
    {
        DontExitGame();
        audioManager.Play("ClickButton");
        animator.SetTrigger("PlayButton");
    }

    public void ConfigurationsButton()
    {
        DontExitGame();
        audioManager.Play("ClickButton");
        animator.SetTrigger("ConfigurationsButton");
    }

    public void QuitButton()
    {
        audioManager.Play("ClickButton");
        ExitBox.SetBool("AppearBox", true);
    }

    public void DontExitGame()
    {
        audioManager.Play("ClickButton");
        ExitBox.SetBool("AppearBox", false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void NewGame()
    {
        audioManager.Play("ClickButton");
        //fog.SetVector4("Color", new Vector4(0.5f, 0.5f, 0.5f, 1f));
        Invoke("ShowYmirDialogue", 5);
        
        animator.SetTrigger("NewGame");
    }

    public void ShowYmirDialogue()
    {
        YmirBox.SetActive(true);
        IntroDialogue.Play();
        cam.backgroundColor = new Color(0.3333333f, 0.3333333f, 0);
    }

    public void YmirDialogue()
    {
        if (YmirBox.activeInHierarchy)
        {
            if (!IntroDialogue.getDialogueEnded())
            {
                if (acceptKey.triggered && IntroDialogue.getEndLine())
                {
                    IntroDialogue.Play();
                }
            }
            else
            {
                GameScenes.instance.setNextScene(2);
                GameScenes.instance.LoadLevel(2);
                animator.SetTrigger("CloseMenu");
            }
        }
    }

    public void BackButton()
    {
        audioManager.Play("ClickButton");
        animator.SetTrigger("Back");
    }
}
