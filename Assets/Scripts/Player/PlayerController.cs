using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    #region variables

    //Set in Inspector
    [SerializeField]
    private InputActionAsset controller;
    [SerializeField]
    private PlayerMenu playerMenu;
    [SerializeField]
    private GameObject interactText;
    [SerializeField]
    private GameObject generalMenu;
    [SerializeField]
    private float interectableRange;
    [SerializeField]
    private float normalSpeed;
    [SerializeField]
    private float sprintSpeed;

    //Set in Script
    private CharacterController characterController;
    private Interactable actualObjectInteract;
    private PlayerItems inventory;
    private Animator anim;
    private Transform cam;
    private Vector2 previousMovement;
    private readonly float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    private float cameraAngle;
    private float playerSpeed;

    // Inputs
    private InputAction interactKey;
    private InputAction movementKey;
    private InputAction runKey;
    private InputAction escKey;
    private InputAction tabKey;

    private AudioManager audioManager;

    #endregion

    #region initialization

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        inventory = GetComponent<PlayerItems>();
        anim = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        cam = Camera.main.transform;
        audioManager = GameObject.FindWithTag("AudioController").GetComponent<AudioManager>();
        InputActionMap Map = controller.FindActionMap("PlayerInput");
        InputActionMap MapUi = controller.FindActionMap("UIController");

        movementKey = Map.FindAction("Movement");
        runKey = Map.FindAction("Run");
        interactKey = Map.FindAction("Interact");
        escKey = MapUi.FindAction("Esc");
        tabKey = MapUi.FindAction("Tab");

    }

    #endregion

    #region controls methods

    public bool GetInteractTrigger()
    {
        if (interactKey.triggered)
        {
            return true;
        }
        return false;
    }

    public void EnableControls()
    {
        EnableKey("movement");
        EnableKey("run");
        EnableKey("interact");
        EnableKey("esc");
        EnableKey("tab");
    }

    public void DisableControls()
    {
        DisableKey("movement");
        EnableKey("run");
        DisableKey("interact");
        DisableKey("esc");
        DisableKey("tab");
    }

    public void EnableKey(string key)
    {
        switch (key)
        {
            case "movement":
                movementKey.Enable();
                break;
            case "run":
                runKey.Enable();
                break;
            case "interact":
                interactKey.Enable();
                break;
            case "esc":
                escKey.Enable();
                break;
            case "tab":
                tabKey.Enable();
                break;
            default:
                Debug.Log("invalid key");
                break;
        }
    }

    public void DisableKey(string key)
    {
        switch (key)
        {
            case "movement":
                movementKey.Disable();
                break;
            case "run":
                runKey.Disable();
                break;
            case "interact":
                interactKey.Disable();
                break;
            case "esc":
                escKey.Disable();
                break;
            case "tab":
                tabKey.Disable();
                break;
            default:
                Debug.Log("invalid key");
                break;
        }
    }

    #endregion

    #region actions

    void Update()
    {
        Move();

        Interact();

        ShowMenus();
    }

    private void ShowMenus()
    {
        if (escKey.triggered)
        {
            if (generalMenu.activeInHierarchy)
            {
                EnableKey("tab");
                EnableKey("movement");
                EnableKey("run");
                generalMenu.SetActive(false);
            }
            else
            {
                DisableKey("tab");
                generalMenu.SetActive(true);
            }
        }
        
        if (tabKey.triggered)
        {
            if (playerMenu.gameObject.activeInHierarchy)
            {
                EnableKey("esc");
                EnableKey("movement");
                EnableKey("run");
                playerMenu.gameObject.SetActive(false);
                playerMenu.ItemButton();
            }
            else
            {
                DisableKey("esc");
                playerMenu.gameObject.SetActive(true);
            }
        }

        if (generalMenu.activeInHierarchy || playerMenu.gameObject.activeInHierarchy)
        {
            DisableKey("movement");
            DisableKey("run");
            Time.timeScale = 0;
        }
        else
            Time.timeScale = 1;
    }

    private void Move()
    {
        Vector2 movement = movementKey.ReadValue<Vector2>();

        if (movement == Vector2.zero || movement != previousMovement)
        {
            cameraAngle = cam.eulerAngles.y;
        }

        previousMovement = movement;

        Vector3 moving = new Vector3(movement.x, 0, movement.y).normalized;

        anim.SetFloat("movement", moving.magnitude);
        anim.SetBool("run", false);

        playerSpeed = normalSpeed;

        if (runKey.ReadValue<float>() == 1)
        {
            anim.SetBool("run", true);
            playerSpeed = sprintSpeed;
        }

        if (moving.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(moving.x, moving.z) * Mathf.Rad2Deg + cameraAngle;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            characterController.Move(moveDirection * Time.deltaTime * playerSpeed);

            if (!audioManager.getAudioIsPlaying("BrickSteps"))
                audioManager.Play("BrickSteps", true);
        }

        if(moving.magnitude < 0.1f)
            audioManager.StopSound("BrickSteps");
    }

    private void Interact()
    {
        Ray ray = new Ray(transform.position, transform.forward);

        Debug.DrawRay(transform.position, transform.forward, Color.blue, interectableRange);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.distance < interectableRange && hit.transform.CompareTag("Selectable"))
            {
                
                Interactable interact = hit.transform.GetComponent<Interactable>();

                if(actualObjectInteract == null)
                {
                    actualObjectInteract = interact;
                    inventory.ReceiveItemFromController(interact);
                }
                else if(interact != actualObjectInteract)
                {
                    actualObjectInteract.CancelAct();
                    actualObjectInteract = interact;
                    inventory.ReceiveItemFromController(interact);
                }
                
                interact.Act();
            }
            else
            {
                if (actualObjectInteract != null)
                {
                    actualObjectInteract.CancelAct();
                    actualObjectInteract = null;
                }
                else
                {
                    interactText.SetActive(false);
                    inventory.HideConfirmationScreen();
                    inventory.HideUseItemsScreen();
                }
            }
        }
        else
        {
            if (actualObjectInteract != null)
            {
                actualObjectInteract.CancelAct();
                actualObjectInteract = null;
            }
        }
    }

    #endregion
}
