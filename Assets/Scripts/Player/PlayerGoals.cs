using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGoals : MonoBehaviour
{
    [SerializeField]
    private List<string> myraGoals;
    [SerializeField]
    private GameObject objectivesCanvas;
    [SerializeField]
    private LocalizedText objectivesTitle;
    [SerializeField]
    private LocalizedText objectivesText;

    private PlayerStatus status;
    private int myraCurrentGoal;

    // Start is called before the first frame update
    void Start()
    {
        myraCurrentGoal = 0;
        status = GetComponent<PlayerStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        VerifyLocation();
    }

    private void VerifyLocation()
    {
        if (status.GetPlayerLocation().Equals("MainArea"))
        {
            ClearHud();
        }
        else if (status.GetPlayerLocation().Equals("MyraArea"))
        {
            MyraArea();
        }
    }

    private void ClearHud()
    {
        objectivesCanvas.SetActive(false);
    }

    private void MyraArea()
    {
        objectivesCanvas.SetActive(true);

        objectivesTitle.SetNewKey("myra_title");
        objectivesTitle.UpdateText();

        objectivesText.SetNewKey(myraGoals[myraCurrentGoal]);
        objectivesText.UpdateText();
    }

    public void SetMyraGoal(string achieve)
    {
        if (achieve.Equals("bring_flower"))
        {
            myraCurrentGoal = 1;
        }
        else if (achieve.Equals("end"))
        {
            myraCurrentGoal = 2;
        }
    }
}
