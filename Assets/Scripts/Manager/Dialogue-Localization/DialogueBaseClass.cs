using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class DialogueBaseClass : MonoBehaviour
{
    public InputActionAsset Controller;

    private bool endLine;

    private InputAction acceptKey;

    private void Start()
    {
        InputActionMap Map = Controller.FindActionMap("UIController");

        acceptKey = Map.FindAction("Accept");
    }

    protected IEnumerator WriteText(string input, TextMeshProUGUI textHolder, Color textColor, float delay, string appearSound)
    {
        endLine = false;

        textHolder.text = "";

        float time = 0;

        textHolder.color = textColor;

        /*AudioManager.instance.Play(appearSound);*/

        for (int i = 0; i < input.Length; i++)
        {

            textHolder.text += input[i];

            time += Time.deltaTime;

            /*if ((acceptKey.triggered) && time > 0.25f)
            {
                i = input.Length;

                textHolder.text = "";

                textHolder.text = input;

                yield return new WaitForSecondsRealtime(0.25f);
            }*/

            yield return new WaitForSecondsRealtime(delay);
        }

        endLine = true;
    }

    public bool getEndLine()
    {
        return endLine;
    }
}
