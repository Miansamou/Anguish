using UnityEngine;

public abstract class IInteractable : MonoBehaviour
{
    #region variables

    [Header("HUD")]
    [SerializeField]
    protected GameObject textMessage;

    protected PlayerController player;

    #endregion

    #region actions

    public abstract void Act();

    public abstract void Acting();

    public abstract void CancelAct();

    public abstract void UseItem();

    #endregion
}
