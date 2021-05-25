using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    protected bool objHover;
    protected PlayerController player;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    public abstract void Act();

    public abstract void Acting();

    public abstract void CancelAct();

    public abstract void UseItem();
}
