using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Tutorial : MonoBehaviour
{
    #region variables

    [Header("Input")]
    [SerializeField]
    private InputActionAsset controller;
    [SerializeField]
    private PlayerController player;

    [Header("Interact trigger")]
    [SerializeField]
    private Collider cell;

    private int levelTutorial;

    // Inputs
    private InputAction movementKey;

    #endregion

    #region initialization

    private void Start()
    {
        InputActionMap Map = controller.FindActionMap("PlayerInput");

        movementKey = Map.FindAction("Movement");

        levelTutorial = 0;
    }

    void OnEnable()
    {
        player.EnableKey("movement");
        player.EnableKey("esc");
        player.EnableKey("tab");
        StartCoroutine(TutorialBoxController.instance.ActiveTextTutorial("Movement", 0f));
        StartCoroutine(TutorialBoxController.instance.PlayAnimation(0f));
    }

    #endregion

    #region tutorial controller

    void Update()
    {
        TutorialController();
    }

    private void TutorialController()
    {
        if (levelTutorial == 0)
        {
            if (movementKey.ReadValue<Vector2>() != Vector2.zero)
            {
                levelTutorial++;
                StartCoroutine(TutorialBoxController.instance.ExitTutorial(0f));
                StartCoroutine(TutorialBoxController.instance.PlayAnimation(1f));
                StartCoroutine(TutorialBoxController.instance.ActiveTextTutorial("Interact", 1f));
            }
        }

        if (levelTutorial == 1)
        {
            if (!cell.enabled)
            {
                levelTutorial++;
                player.EnableKey("run");
                StartCoroutine(TutorialBoxController.instance.ExitTutorial(0f));
                StartCoroutine(TutorialBoxController.instance.PlayAnimation(1f));
                StartCoroutine(TutorialBoxController.instance.ActiveTextTutorial("Run", 1f));
            }
        }

        if (levelTutorial == 2)
        {
            levelTutorial++;
            StartCoroutine(TutorialBoxController.instance.ExitTutorial(3f));
        }
    }

    #endregion
}
