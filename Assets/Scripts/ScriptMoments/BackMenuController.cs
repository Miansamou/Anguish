using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackMenuController : MonoBehaviour
{
    public void BackMenu()
    {
        GameScenes.instance.SetNextScene(0);
        GameScenes.instance.LoadLevel(0);
    }
}
