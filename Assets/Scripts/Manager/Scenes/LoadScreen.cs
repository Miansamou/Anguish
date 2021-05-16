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

/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadScreen : MonoBehaviour
{
    public TextMeshProUGUI porcent;

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
            Debug.Log(Time.timeSinceLevelLoad);
            if (gameLevel.progress >= 0.9f)
            {
                System.GC.Collect();
                gameLevel.allowSceneActivation = true;
            }
            yield return null;
        }

        Debug.Log("feito");
    }
}*/