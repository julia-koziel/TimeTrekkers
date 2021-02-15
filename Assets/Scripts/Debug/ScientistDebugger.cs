using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(DebugFocuser))]
public class ScientistDebugger : MonoBehaviour 
{
#if UNITY_EDITOR
    public TrialRunner tr;
    DebugFocuser focuser;
    
    private void Awake() 
    {
        focuser = GetComponent<DebugFocuser>();
    }

    public void OnDebug()
    {
        focuser.FocusGameWindow();
        this.In(1).Call(() => {
            focuser.FocusGameWindow();
            var ct = FindObjectOfType<CameraTransition>();
            if (ct != null)
            {
                ct.transitionInThen(tr.StartTrials);
            }
            else
            {
                print($"You need a Scientist & Cameras obj in your scene:");
            }
        });
    }

    public void OnEndDebug()
    {
        var ct = FindObjectOfType<CameraTransition>();
        ct.transitionOutThen(() => {});
    }
# endif
}