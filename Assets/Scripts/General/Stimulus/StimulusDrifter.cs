using UnityEngine;

public class StimulusDrifter : MonoBehaviour 
{
    public float startForce;
    
    void OnEnable()
    {
        var dir = (Vector3.zero - transform.position).normalized;
        var body = GetComponent<Rigidbody2D>();
        body.AddForce(startForce * dir);
    }
}