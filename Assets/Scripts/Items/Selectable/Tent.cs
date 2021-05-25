using UnityEngine;

public class Tent : Interactable
{
    public GameObject textMessage;
    public GameObject tentMessage;
    public string key;

    public override void Act()
    {
        LocalizedText localization = textMessage.GetComponent<LocalizedText>();
        localization.SetNewKey(key);

        textMessage.SetActive(true);

        if (player.getInteractTrigger())
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
