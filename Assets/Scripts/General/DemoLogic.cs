using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DemoLogic : MonoBehaviourBase
{
    public abstract void startTrials();
    public abstract void onEndTransitionOut();
}
