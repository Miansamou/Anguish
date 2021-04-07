using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Tutorial : MonoBehaviour
{
    public PlayerController player;
    public Animator tutorialBox;
    public GameObject movementText;
    public GameObject interactText;
    public GameObject otherText;
    public InputActionAsset Controller;
    public Collider cell;

    private int levelTutorial;

    // Inputs
    private InputAction movementKey;

    private void Start()
    {
        InputActionMap Map = Controller.FindActionMap("PlayerInput");

        movementKey = Map.FindAction("Movement");

        levelTutorial = 0;
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        player.EnableKey("movement");
        tutorialBox.SetTrigger("Enter");
        movementText.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (movementText.activeInHierarchy && levelTutorial == 0)
        {
            if (movementKey.ReadValue<Vector2>() != Vector2.zero)
            {
                levelTutorial++;
                tutorialBox.SetTrigger("Exit");
                StartCoroutine(PlayAnimation(movementText, interactText));
                player.EnableKey("interact");
            }
        }

        if (interactText.activeInHierarchy && levelTutorial == 1)
        {
            if (!cell.enabled)
            {
                levelTutorial++;
                tutorialBox.SetTrigger("Exit");
                StartCoroutine(PlayAnimation(interactText, otherText));
                player.EnableKey("run");
                player.EnableKey("jump");
            }
        }

        if (otherText.activeInHierarchy && levelTutorial == 2)
        {
            levelTutorial++;
            Invoke("ExitTutorial", 3f);
            Invoke("EndTutorial", 4f);
        }
    }

    private IEnumerator PlayAnimation(GameObject oldText, GameObject newText)
    {
        yield return new WaitForSeconds(1f);

        tutorialBox.SetTrigger("Enter");
        oldText.SetActive(false);
        newText.SetActive(true);
    }

    private void ExitTutorial()
    {
        tutorialBox.SetTrigger("Exit");
    }

    private void EndTutorial()
    {
        tutorialBox.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}
