using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Interactable : MonoBehaviour
{
    protected bool objHover;

    public abstract void Act(InputAction input);

    public abstract void Acting();

    public abstract void CancelAct();

    public abstract void UseItem();
}
