using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBoxController : MonoBehaviour
{
    #region variables

    public static TutorialBoxController instance;

    [SerializeField]
    private List<GameObject> boxes;

    private Animator anim;

    #endregion

    #region initialization

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        foreach(GameObject box in boxes)
        {
            box.SetActive(false);
        }
    }

    #endregion

    #region active text

    public IEnumerator ActiveTextTutorial(string obj, float time)
    {
        yield return new WaitForSeconds(time);

        foreach (GameObject box in boxes)
        {
            if (box.name.Equals(obj))
            {
                box.SetActive(true);
            }
            else
                box.SetActive(false);
        }
    }

    #endregion

    #region play animations

    public IEnumerator PlayAnimation(float time)
    {
        yield return new WaitForSeconds(time);

        anim.SetTrigger("Enter");
    }

    public IEnumerator ExitTutorial(float time)
    {
        yield return new WaitForSeconds(time);

        anim.SetTrigger("Exit");
    }

    #endregion
}
