using System.Collections;
using System;
using UnityEngine;

public class MonoBehaviourBase : MonoBehaviour
{
    public Vector3 position {
        get { return transform.position; }
        set { transform.position = value; }
    }

    public Vector3 localScale {
        get { return transform.localScale; }
        set { transform.localScale = value; }
    }
    void Invoke(Action action, float delay)
    {
        StartCoroutine(InvokeCoroutine(action, delay));
    }

    IEnumerator InvokeCoroutine(Action action, float delay)
    {
        yield return new WaitForSeconds(delay);
        action();
    }

    protected Invoker In(float delay)
    {
        return new Invoker(this, delay);
    }

    protected class Invoker
    {
        float delay;
        MonoBehaviourBase outer;
        public Invoker(MonoBehaviourBase outer, float delay) {
            this.delay = delay;
            this.outer = outer;
        }
        public void Call(Action action) => outer.Invoke(action, delay);
    }

    public void show() => gameObject.SetActive(true);
    public void hide() => gameObject.SetActive(false);
    public void SetActive(bool active) => gameObject.SetActive(active);
}
