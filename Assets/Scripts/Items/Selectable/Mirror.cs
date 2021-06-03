using UnityEngine;

public class Mirror : IInteractable
{
    public GameObject mirrorMessage;
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
        player.DisableKey("tab");
        player.DisableKey("esc");
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
