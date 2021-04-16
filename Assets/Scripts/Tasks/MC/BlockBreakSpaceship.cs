using UnityEngine;

public class BlockBreakSpaceship : MonoBehaviour 
{
    public Vector3 start;
    public Vector3 end;
    public float amplitude;
    public float displacement;
    public float frequency;
    public float speed;
    float time;

    void OnEnable()
    {
        transform.position = start;
        time = 0;
    }
        
    void Update()
    {
        time += Time.deltaTime;
        var y = speed * time;
        var x = Mathf.Sin(frequency * y + displacement) * amplitude;
        transform.position = start + y * Vector3.up + x * Vector3.left;
        if (transform.position.y > end.y) time = 0;
    }
}