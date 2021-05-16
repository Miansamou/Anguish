using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    private string Location;

    // Start is called before the first frame update
    void Start()
    {
        Location = "MainCorridor";
    }

    public void setLocation(string newLocation)
    {
        Location = newLocation;
    }

    public string getLocation()
    {
        return Location;
    }
}
