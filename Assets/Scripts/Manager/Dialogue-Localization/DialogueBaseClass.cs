using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueBaseClass : MonoBehaviour
{
    private bool endLine;

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

            yield return new WaitForSecondsRealtime(delay);
        }

        endLine = true;
    }

    public bool GetEndLine()
    {
        return endLine;
    }
}
