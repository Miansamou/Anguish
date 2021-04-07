using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScenes : MonoBehaviour
{
    public static GameScenes instance;

    private int nextScene;

    private void Awake()
    {
        if (instance == null)
        {
            nextScene = 1;
            instance = this;
        }
        else if(SceneManager.GetActiveScene().name != "LoadScreen")
        {
            Destroy(instance.gameObject);
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void setNextScene(int scene)
    {
        nextScene = scene;
    }

    public void LoadLevel(float waitTime)
    {
        Invoke("LoadingLevel", waitTime);
    }

    public void LoadingLevel()
    {
        SceneManager.LoadScene(1);
    }

    public int getNextScene()
    {
        return nextScene;
    }
}
