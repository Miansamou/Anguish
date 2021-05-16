using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryDoor : MonoBehaviour
{
    public List<GameObject> ActiveObjects;
    public string locale;
    public PlayerStatus status;
    public Animator TitleArea;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            foreach(GameObject obj in ActiveObjects)
            {
                obj.SetActive(true);
            }

            TitleArea.SetTrigger("EntryTitle");
            status.setLocation(locale);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            foreach (GameObject obj in ActiveObjects)
            {
                obj.SetActive(false);
            }
        }
    }
}
