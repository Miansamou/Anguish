using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowAnimationOneTime : MonoBehaviour
{
    public Animator anim;
    public string trigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            anim.SetTrigger(trigger);
            gameObject.SetActive(false);
        }
    }
}
