using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

[RequireComponent(typeof(SpriteRenderer))]
public class OnVisibleEvent : MonoBehaviour 
{
    [Range(0, 1)]
    public float percentVisible;
    public UnityEvent Event;
    SpriteRenderer _rend;
    SpriteRenderer rend
    {
        get
        {
            if (_rend == null) _rend = GetComponent<SpriteRenderer>();
            return _rend;
        }
    }
    Camera mainCam;
    bool onScreenLogged = false;
    bool validating = false;
    int frames = 5;

    void OnEnable() 
    {
        mainCam = Camera.main;
        onScreenLogged = false;
    }

    void OnDrawGizmosSelected()
    {
        var bounds = new Bounds(rend.bounds.center, rend.bounds.size);

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(bounds.center, bounds.size);

        Gizmos.color = new Color(201, 215, 255, 255);
        var left = new Bounds(bounds.center + (1 - percentVisible) * bounds.extents.x * Vector3.right, 
                              bounds.size - (1 - percentVisible) * bounds.size.x * Vector3.right);
        
        var right = new Bounds(bounds.center + (1 - percentVisible) * bounds.extents.x * Vector3.left, 
                              bounds.size - (1 - percentVisible) * bounds.size.x * Vector3.right);
        
        var top = new Bounds(bounds.center + (1 - percentVisible) * bounds.extents.x * Vector3.up, 
                              bounds.size - (1 - percentVisible) * bounds.size.y * Vector3.up);
        
        var bottom = new Bounds(bounds.center + (1 - percentVisible) * bounds.extents.x * Vector3.down, 
                              bounds.size - (1 - percentVisible) * bounds.size.y * Vector3.up);
        
        var color = validating ? Color.blue : Color.white;
        color.a = validating ? 0.05f : 0;
        Gizmos.color = color;
        Gizmos.DrawCube(left.center, left.size);
        Gizmos.DrawCube(right.center, right.size);
        Gizmos.DrawCube(top.center, top.size);
        Gizmos.DrawCube(bottom.center, bottom.size);
        color.a = 1;
        Gizmos.color = color;
        Gizmos.DrawWireCube(left.center, left.size);
        Gizmos.DrawWireCube(right.center, right.size);
        Gizmos.DrawWireCube(top.center, top.size);
        Gizmos.DrawWireCube(bottom.center, bottom.size);

        if (validating) frames--;
        if (frames == 0) { validating = false; frames = 5; }
    }
    void OnValidate() {
        validating = true;
        frames++;
    } 

    void Update() 
    {
        if (!onScreenLogged && rend.bounds.ArePartiallyVisibleFrom(percentVisible, mainCam))
        {
            onScreenLogged = true;
            Event.Invoke();
        }
        else if (onScreenLogged && !rend.IsVisibleFrom(mainCam)) onScreenLogged = false;
    }
}