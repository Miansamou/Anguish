using UnityEngine;
using UnityEngine.InputSystem;

public class MenuController : MonoBehaviour
{
    #region variables

    [SerializeField]
    private InputActionAsset controller;
    [SerializeField]
    private Animator exitBox;

    private InputAction acceptKey;
    private InputAction anyKey;
    private Animator animator;

    #endregion

    #region initialization

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        AudioManager.instance.Play("MenuMusic");

        InputActionMap Map = controller.FindActionMap("UIController");

        anyKey = Map.FindAction("AnyKey");
        acceptKey = Map.FindAction("Accept");

        anyKey.Enable();
        acceptKey.Enable();
    }

    #endregion

    #region buttons actions

    // Update is called once per frame
    void Update()
    {
        AnyKeyScreen();
    }

    public void AnyKeyScreen()
    {
        if (anyKey.triggered)
        {
            AudioManager.instance.Play("DarkEntering");
            animator.SetTrigger("HomeMenu");
            anyKey.Disable();
        }
    }

    public void PlayButton()
    {
        DontExitGame();
        animator.SetTrigger("PlayButton");
    }

    public void ConfigurationsButton()
    {
        DontExitGame();
        animator.SetTrigger("ConfigurationsButton");
    }

    public void QuitButton()
    {
        AudioManager.instance.Play("ClickButton");
        exitBox.SetBool("AppearBox", true);
    }

    public void DontExitGame()
    {
        AudioManager.instance.Play("ClickButton");
        exitBox.SetBool("AppearBox", false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void NewGame()
    {
        AudioManager.instance.Play("ClickButton");
        GameScenes.instance.SetNextScene(2);
        GameScenes.instance.LoadLevel(4);
        animator.SetTrigger("NewGame");
    }

    public void BackButton()
    {
        AudioManager.instance.Play("ClickButton");
        animator.SetTrigger("Back");
    }

    #endregion
}
