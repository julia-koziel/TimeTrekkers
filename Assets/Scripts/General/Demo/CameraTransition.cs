using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

// TODO switch skip and home to backgroundCam
public class CameraTransition : MonoBehaviour
{
    public Camera backgroundCam;
    Camera _mainCam;
    Camera mainCam {
        get
        {
            if (_mainCam == null) _mainCam = Camera.main;
            return _mainCam;
        }
    }
    public Camera scientistCam;
    static Camera staticScientistCam;
    static Camera staticMainCam;
    public Scientist scientist;
    Rect startRect;
    Vector3 startPos;
    public bool transitionCalled = false;
    Movement movement;

    // Debug only
    public void transitionIn()
    {
        transitionInThen();
    }

    public void transitionInThen(Action onEndTransition = null)
    {
        onEndTransition = onEndTransition ?? (() => { });
        staticScientistCam = scientistCam;
        staticMainCam =  mainCam;

        transitionCalled = true;

        backgroundCam.gameObject.SetActive(true);
        scientistCam.gameObject.SetActive(true);
        scientist.gameObject.SetActive(true);
        startRect = mainCam.rect;
        startPos = scientist.transform.position;

        movement = this.BuildMovement()
                           .First(run: transitionCam)
                           .Then(run: transitionScientist)
                           .Then(run: onEndTransition);
        
        movement.Start();
    }

    public void transitionOutThen(Action onEndTransition)
    {
        movement.Stop();
        StopAllCoroutines();

        Action resetCameras = delegate { 
            backgroundCam.gameObject.SetActive(false); 
            scientistCam.gameObject.SetActive(false);
            scientist.gameObject.SetActive(false);
        };

        movement = this.BuildMovement()
                           .First(run: transitionScientist, reverse: true)
                           .Then(run: transitionCam, reverse: true)
                           .Then(run: Reset)
                           .Then(run: onEndTransition);
        
        movement.Start();
    }

    public void Reset()
    {
        if (transitionCalled)
        {
            scientist.Reset();
            scientist.transform.position = startPos;
            mainCam.rect = startRect;
            backgroundCam.gameObject.SetActive(false); 
            scientistCam.gameObject.SetActive(false);
            scientist.gameObject.SetActive(false);
        }
    }

    void transitionCam(float delta)
    {
        mainCam.rect = new Rect(
            startRect.xMin + 0.1f * startRect.width * delta, 
            startRect.yMin + 0.2f * startRect.height * delta, 
            startRect.width - 0.2f * startRect.width * delta, 
            startRect.height - 0.2f * startRect.height * delta
        );
    }

    void transitionScientist(float delta)
    {
        scientist.transform.position = startPos + Vector3.up * (-5 - startPos.y)*delta;
    }

    // TODO tidy
    public static Vector3 convertMainToScientist(Vector3 pos)
    {
        if (staticScientistCam == null || staticMainCam == null) return pos;
        return staticScientistCam.ScreenToWorldPoint(staticMainCam.WorldToScreenPoint(pos));
    }
}
