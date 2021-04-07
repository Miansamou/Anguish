using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class IntroAnimation : MonoBehaviour
{
    public Animator CameraAnimate;
    public GameObject CanvasBlinkEyes;
    public Volume Blur;
    public CinemachineVirtualCamera nextCamera;
    public GameObject Tutorial;

    private float speed;
    private DepthOfField bluring;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("PlayCamera", 5f);

        DepthOfField tempDof;

        if (Blur.profile.TryGet<DepthOfField>(out tempDof))
        {
            bluring = tempDof;
        }
    }

    private void Update()
    {
        bluring.aperture.value += speed * Time.deltaTime;
    }

    private void PlayCamera()
    {
        CanvasBlinkEyes.SetActive(false);

        CameraAnimate.SetTrigger("IntroAnimationCamera");

        Invoke("ClearScreen", 0.5f);

        Invoke("EndIntro", 3f);
    }

    private void ClearScreen()
    {
        speed = 8;
    }

    private void EndIntro()
    {
        CameraAnimate.GetComponent<CinemachineVirtualCamera>().enabled = false;
        nextCamera.enabled = true;

        bluring.aperture.value = 0;
        Blur.gameObject.SetActive(false);

        Tutorial.SetActive(true);

        gameObject.SetActive(false);
    }
}
