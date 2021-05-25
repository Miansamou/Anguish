using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryDoor : MonoBehaviour
{
    public List<GameObject> ActiveObjects;
    public string locale;
    public Animator TitleArea;

    private void Start()
    {
        SetObjectsActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            SetObjectsActive(true);

            TitleArea.SetTrigger("EntryTitle");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            SetObjectsActive(false);
        }
    }

    private void SetObjectsActive(bool activation)
    {
        foreach (GameObject obj in ActiveObjects)
        {
            obj.SetActive(activation);
        }
    }
}
