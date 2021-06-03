using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    private string playerLocation;

    private void Awake()
    {
        playerLocation = "MainArea";
    }

    public void SetLocation(string location)
    {
        playerLocation = location;
    }

    public string GetPlayerLocation()
    {
        return playerLocation;
    }
}
