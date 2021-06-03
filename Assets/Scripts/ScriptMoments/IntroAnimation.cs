using Cinemachine;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class IntroAnimation : MonoBehaviour
{
    #region variables

    [Header("Cameras")]
    [SerializeField]
    private Animator cameraAnimate;
    [SerializeField]
    private CinemachineVirtualCamera nextCamera;

    [Header("Effects")]
    [SerializeField]
    private GameObject canvasBlinkEyes;
    [SerializeField]
    private Volume blur;

    [Header("Tutorial")]
    [SerializeField]
    private GameObject tutorial;

    [Header("Ymir")]
    [SerializeField]
    private GameObject ymirExplanation;
    [SerializeField]
    private PlayerController player;
    [SerializeField]
    private DialogueLine ymirDialogue;

    private int speedBlur = 0;
    private DepthOfField bluring;
    private string phase;

    #endregion

    #region initialization

    void Start()
    {
        phase = "cutscene";

        Invoke(nameof(PlayCamera), 5f);

        if (blur.profile.TryGet<DepthOfField>(out DepthOfField tempDof))
        {
            bluring = tempDof;
        }
    }

    #endregion

    #region cutscene

    private void Update()
    {
        bluring.aperture.value += speedBlur * Time.deltaTime;

        YmirDialogue();
    }

    private void PlayCamera()
    {
        cameraAnimate.SetTrigger("IntroAnimationCamera");

        Invoke(nameof(ClearScreen), 0.5f);

        Invoke(nameof(EndIntro), 3f);
    }

    private void ClearScreen()
    {
        speedBlur = 8;
    }

    private void EndIntro()
    {
        cameraAnimate.GetComponent<CinemachineVirtualCamera>().enabled = false;
        nextCamera.enabled = true;

        bluring.aperture.value = 0;
        blur.gameObject.SetActive(false);

        phase = "ymir";
    }

    #endregion

    #region ymir

    private void YmirDialogue()
    {
        if (phase.Equals("ymir"))
        {
            if (ymirExplanation.activeInHierarchy)
            {
                if (player.GetInteractTrigger())
                {
                    if (!ymirDialogue.GetDialogueEnded())
                    {
                        if (ymirDialogue.GetEndLine())
                            ymirDialogue.Play();
                    }
                    else
                    {
                        canvasBlinkEyes.SetActive(false);
                        phase = "finish";
                        ymirExplanation.SetActive(false);
                        tutorial.SetActive(true);
                        gameObject.SetActive(false);
                    }
                }
            }
            else
            {
                ymirExplanation.SetActive(true);
                ymirDialogue.Play();
                player.EnableKey("interact");
            }
        }
    }

    #endregion
}
