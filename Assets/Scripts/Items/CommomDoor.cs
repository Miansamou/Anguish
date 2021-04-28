using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CommomDoor : Interactable
{
    public GameObject textMessage;
    public string key;
    public string animationName;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public override void Act(InputAction input)
    {

        LocalizedText localization = textMessage.GetComponent<LocalizedText>();
        localization.setNewKey(key);

        textMessage.SetActive(true);

        if (input.triggered)
        {
            Acting();
        }
    }

    public override void Acting()
    {
        animator.SetTrigger(animationName);
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
    }

    public override void CancelAct()
    {
        textMessage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
