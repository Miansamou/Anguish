using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PrologueController : MonoBehaviour
{
    #region variables

    [SerializeField]
    private PlayerController player;
    [SerializeField]
    private DialogueLine dialogue;
    [SerializeField]
    private Button buttonEyes;
    [SerializeField]
    private GameObject textButton;
    [SerializeField]
    private TextMeshProUGUI text;

    private string phase = "start";
    private Animator anim;
    private float timer = 0f;
    private int seconds;

    #endregion

    #region initialization

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        if (AudioManager.instance.GetMusicIsPlaying())
            StartCoroutine(AudioManager.instance.FadeOut(-0.02f));
    }

    #endregion

    #region methods

    public void EnablePlayer()
    {
        player.EnableKey("movement");
        player.EnableKey("esc");
        player.EnableKey("interact");
        player.EnableKey("run");
    }

    void Update()
    {
        DialoguePhases();
    }

    private void DialoguePhases()
    {
        timer += Time.deltaTime;
        seconds = (int)(timer % 60);
        float pos = player.gameObject.transform.position.z;

        switch (phase)
        {
            case "start":
                if (seconds >= 4)
                {
                    anim.SetTrigger("StartDialogue");
                    phase = "second";
                }
                break;
            case "second":
                if (seconds >= 12 && Mathf.Abs(pos) > 70)
                {
                    anim.SetTrigger("StartDialogue");
                    phase = "third";
                }
                break;
            case "third":
                if (seconds >= 20 && Mathf.Abs(pos) > 250)
                {
                    anim.SetTrigger("StartDialogue");
                    phase = "fourth";
                }
                break;
            case "fourth":
                if (seconds >= 28 && Mathf.Abs(pos) > 400)
                {
                    text.alignment = TextAlignmentOptions.MidlineLeft;
                    anim.SetTrigger("DarkYmir");
                    phase = "fifth";
                }
                break;
            case "fifth":
                if (dialogue.GetEndLine())
                {
                    if (player.GetInteractTrigger())
                    {
                        anim.SetTrigger("NormalYmir");
                        phase = "sixth";
                    }
                }
                break;
            case "sixth":
                if (dialogue.GetEndLine())
                {
                    if (dialogue.GetDialogueEnded())
                    {
                        buttonEyes.interactable = true;
                        textButton.SetActive(true);
                        phase = "finish";
                    }
                    else if (player.GetInteractTrigger())
                    {
                        PlayDialogue();
                    }
                }
                break;
        }
    }

    public void PlayDialogue()
    {
        dialogue.Play();
    }

    public void CloseEyes()
    {
        player.DisableControls();
        AudioManager.instance.Play("ClickButton");
        anim.SetTrigger("CloseEyes");
    }

    public void LoadGame()
    {
        GameScenes.instance.SetNextScene(3);
        GameScenes.instance.LoadLevel(0);
    }

    #endregion
}
