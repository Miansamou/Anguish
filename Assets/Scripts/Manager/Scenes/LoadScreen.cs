using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScreen : MonoBehaviour
{
    #region initialization

    private void Start()
    {
        Time.timeScale = 1;

        StartCoroutine(LoadAsynchronously(GameScenes.instance.GetNextScene()));
    }

    #endregion

    #region loading

    IEnumerator LoadAsynchronously(int sceneindex)
    {
        AsyncOperation gameLevel = SceneManager.LoadSceneAsync(sceneindex);

        gameLevel.allowSceneActivation = false;

        while (!gameLevel.isDone)
        {
            if (gameLevel.progress >= 0.9f)
            {
                yield return new WaitForSeconds(0.5f);
                System.GC.Collect();
                gameLevel.allowSceneActivation = true;
            }
            yield return null;
        }
    }

    #endregion
}
