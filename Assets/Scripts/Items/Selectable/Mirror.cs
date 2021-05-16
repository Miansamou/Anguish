using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Mirror : Interactable
{

    public PlayerController player;
    public GameObject textMessage;
    public GameObject mirrorMessage;
    public string key;

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
        player.DisableKey("jump");
        player.DisableKey("movement");
        player.DisableKey("run");
        mirrorMessage.SetActive(true);
    }

    public override void CancelAct()
    {
        textMessage.SetActive(false);
    }

    public override void UseItem()
    {
        Debug.Log("Unable to use item");
    }
}
