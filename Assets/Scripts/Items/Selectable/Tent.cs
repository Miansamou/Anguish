using UnityEngine;

public class Tent : IInteractable
{
    public GameObject tentMessage;
    public string key;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    public override void Act()
    {
        LocalizedText localization = textMessage.GetComponent<LocalizedText>();
        localization.SetNewKey(key);

        textMessage.SetActive(true);

        if (player.GetInteractTrigger())
        {
            Acting();
        }
    }

    public override void Acting()
    {
        player.DisableKey("movement");
        player.DisableKey("run");
        tentMessage.SetActive(true);
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
