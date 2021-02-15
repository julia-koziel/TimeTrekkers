using UnityEngine;
using System;
using System.Collections;

public static class UnityExtensions
{
    public static void SetActive(this MonoBehaviour mb, bool active) => mb.gameObject.SetActive(active);
    public static Invoker In(this MonoBehaviour mb, float delay)
    {
        return new Invoker(mb, delay);
    }

    public class Invoker
    {
        float delay;
        MonoBehaviour mb;
        public Invoker(MonoBehaviour mb, float delay) {
            this.delay = delay;
            this.mb = mb;
        }
        void Invoke(Action action, float delay)
        {
            mb.StartCoroutine(InvokeCoroutine(action, delay));
        }
        IEnumerator InvokeCoroutine(Action action, float delay)
        {
            yield return new WaitForSeconds(delay);
            action();
        }
        public void Call(Action action) => Invoke(action, delay);
    }

    public static bool AreVisibleFrom(this Bounds bounds, Camera camera)
	{
		Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
		return GeometryUtility.TestPlanesAABB(planes, bounds);
	}

    public static bool ArePartiallyVisibleFrom(this Bounds bounds, float percent, Camera camera)
    {
        var maxPartialBounds = new Bounds(bounds.center + percent * bounds.extents, (1-percent) * bounds.size);
        var minPartialBounds = new Bounds(bounds.center - percent * bounds.extents, (1-percent) * bounds.size);
        return maxPartialBounds.AreVisibleFrom(camera) && minPartialBounds.AreVisibleFrom(camera);
    }

    public static bool AreFullyVisibleFrom(this Bounds bounds, Camera camera) => bounds.ArePartiallyVisibleFrom(1, camera);
    public static bool IsVisibleFrom(this Renderer renderer, Camera camera) => renderer.bounds.AreVisibleFrom(camera);
    public static bool IsPartiallyVisibleFrom(this Renderer renderer, float percent, Camera camera) => renderer.bounds.ArePartiallyVisibleFrom(percent, camera);
    public static bool IsFullyVisibleFrom(this Renderer renderer, Camera camera) => renderer.bounds.AreFullyVisibleFrom(camera);
    public static Vector2 RandomPointInRect(this Rect rect)
    {
        return new Vector2(UnityEngine.Random.Range(rect.xMin, rect.xMax), UnityEngine.Random.Range(rect.yMin, rect.yMax));
    }

    public static Rect[] DivideHorizontally(this Rect rect, int nParts)
    {
        if (nParts < 1) throw new ArgumentException("nParts must be greater than 0");
        
        var rects = new Rect[nParts];
        var width = rect.size.x / nParts;
        for (int i = 0; i < nParts; i++)
        {
            var newRect = new Rect(rect);
            newRect.x += i * width;
            newRect.width = width;
            rects[i] = newRect;
        }

        return rects;
    }
}