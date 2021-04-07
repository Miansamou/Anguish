using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Transform cam;
    public float JumpHeight;
    public float DistToGround;
    public float InterectableRange;
    public InputActionAsset Controller;

    private CharacterController characterController;
    private float cameraAngle;
    private Vector2 previousMovement;
    private Vector3 playerVelocity;
    private float playerSpeed;
    private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    private float gravity = -20f;
    private bool playerGrounded;
    private Interactable actualObjectInteract;

    // Inputs
    private InputAction jumpKey;
    private InputAction movementKey;
    private InputAction runKey;
    private InputAction interactKey;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Start()
    {
        InputActionMap Map = Controller.FindActionMap("PlayerInput");

        jumpKey = Map.FindAction("Jump");
        movementKey = Map.FindAction("Movement");
        runKey = Map.FindAction("Run");
        interactKey = Map.FindAction("Interact");

    }

    public void EnableControls()
    {
        EnableKey("jump");
        EnableKey("movement");
        EnableKey("run");
        EnableKey("interact");
    }

    public void DisableControls()
    {
        DisableKey("jump");
        DisableKey("movement");
        DisableKey("run");
        DisableKey("interact");
    }

    public void EnableKey(string key)
    {
        switch (key)
        {
            case "jump":
                jumpKey.Enable();
                break;
            case "movement":
                movementKey.Enable();
                break;
            case "run":
                runKey.Enable();
                break;
            case "interact":
                interactKey.Enable();
                break;
        }
    }

    public void DisableKey(string key)
    {
        switch (key)
        {
            case "jump":
                jumpKey.Disable();
                break;
            case "movement":
                movementKey.Disable();
                break;
            case "run":
                runKey.Disable();
                break;
            case "interact":
                interactKey.Disable();
                break;
        }
    }

    void Update()
    {
        Jump();

        Move();

        Interact();
    }

    private void Jump()
    {
        playerGrounded = Physics.Raycast(transform.position, Vector3.down, DistToGround);

        Debug.DrawRay(transform.position, Vector3.down, Color.red, DistToGround);

        if (jumpKey.triggered && playerGrounded)
        {
            playerVelocity.y = transform.position.y;
            playerVelocity.y += JumpHeight;
        }
        else if (playerGrounded)
        {
            playerVelocity.y = 0;
        }

        if (!playerGrounded)
        {
            playerVelocity.y += (gravity * Time.deltaTime);
        }
        characterController.Move(Time.deltaTime * playerVelocity);
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

        playerSpeed = 9;

        if (runKey.ReadValue<float>() == 1)
        {
            playerSpeed = 12;
        }


        if (moving.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(moving.x, moving.z) * Mathf.Rad2Deg + cameraAngle;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            characterController.Move(moveDirection * Time.deltaTime * playerSpeed);
        }
    }

    private void Interact()
    {
        Ray ray = new Ray(transform.position, transform.forward);

        Debug.DrawRay(transform.position, transform.forward, Color.blue, InterectableRange);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.distance < InterectableRange && hit.transform.CompareTag("Selectable"))
            {
                
                Interactable interact = hit.transform.GetComponent<Interactable>();

                if(actualObjectInteract == null)
                {
                    actualObjectInteract = interact;
                }
                else
                {
                    actualObjectInteract.CancelAct();
                    actualObjectInteract = interact;
                }

                interact.Act(interactKey);
            }
            else
            {
                if(actualObjectInteract != null)
                {
                    actualObjectInteract.CancelAct();
                    actualObjectInteract = null;
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
}
