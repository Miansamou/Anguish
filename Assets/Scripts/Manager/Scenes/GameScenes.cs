using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScenes : MonoBehaviour
{
    #region variables

    public static GameScenes instance;

    private int nextScene;

    #endregion

    #region initialization

    private void Awake()
    {
        if (instance == null)
        {
            nextScene = 1;
            instance = this;
        }
        else 
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    #endregion

    #region get/set

    public void SetNextScene(int scene)
    {
        nextScene = scene;
    }

    public int GetNextScene()
    {
        return nextScene;
    }

    #endregion

    #region load methods

    public void LoadLevel(float waitTime)
    {
        Invoke(nameof(LoadingLevel), waitTime);
    }

    private void LoadingLevel()
    {
        SceneManager.LoadScene(1);
    }

    #endregion
}
