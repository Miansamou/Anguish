using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScreen : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(LoadAsynchronously(GameScenes.instance.getNextScene()));
    }

    IEnumerator LoadAsynchronously(int sceneindex)
    {
        AsyncOperation gameLevel = SceneManager.LoadSceneAsync(sceneindex);

        gameLevel.allowSceneActivation = false;

        while (!gameLevel.isDone)
        {
            if (gameLevel.progress >= 0.9f)
            {
                yield return new WaitForSeconds(0.5f);
                gameLevel.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}