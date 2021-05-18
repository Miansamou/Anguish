using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScreen : MonoBehaviour
{
    public Image bar;

    private void Start()
    {
        Time.timeScale = 1;
        bar.fillAmount = 0;

        StartCoroutine(LoadAsynchronously(GameScenes.instance.getNextScene()));
    }

    IEnumerator LoadAsynchronously(int sceneindex)
    {
        AsyncOperation gameLevel = SceneManager.LoadSceneAsync(sceneindex);

        gameLevel.allowSceneActivation = false;

        while (!gameLevel.isDone)
        {
            bar.fillAmount = gameLevel.progress;

            if (gameLevel.progress >= 0.9f)
            {
                yield return new WaitForSeconds(0.5f);
                System.GC.Collect();
                gameLevel.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
