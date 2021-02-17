using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StarBehaviour : MonoBehaviour
{
    public GameEvent starArrived;
    public Vector3 destination;
    public Color colour;
    float time;
    float totalTime = 1;
    float angleStep;
    float yInitSpeed;
    float xSpeed;
    Vector3 originalPos;
    bool isMoving = false;
    bool fadedIn = false;
    bool arrived = false;
    SpriteRenderer spriteRenderer;
    Quaternion[] isorotations = new Quaternion[]
    {
        Quaternion.identity,
        Quaternion.Euler(0, 0, 72),
        Quaternion.Euler(0, 0, 144),
        Quaternion.Euler(0, 0, 216),
        Quaternion.Euler(0, 0, 288)
    };

    int rotationDir;

    // Start is called before the first frame update
    void Awake()
    {
        originalPos = transform.position;
        transform.GetChild(0).GetComponent<SpriteRenderer>().color = colour;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void OnEnable()
    {
        plotTrajectory();
        isMoving = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            Move();
        }
        else if (!fadedIn)
        {
            var color = spriteRenderer.color;
            color.a += 0.2f;
            spriteRenderer.color = color;
            fadedIn = color.a == 1;
        }
        else if (!arrived)
        {
            starArrived.Raise();
            arrived = true;
        } 
    }

    private void plotTrajectory()
    {
        float xDist = destination.x - transform.position.x;
        float yDist = destination.y - transform.position.y;
        yInitSpeed = Mathf.Sqrt(yDist * 2 * 9.81f);
        totalTime = yInitSpeed / 9.81f;
        xSpeed = xDist / totalTime;
        angleStep = 54;

        rotationDir = xDist > 0 ? 1 : -1;
    }

    private void Move()
    {
        float dist = Vector3.Distance(transform.position, destination);

        bool pastTarget = (destination.x - transform.position.x) * rotationDir < 0;
        
        if (dist > 0.2f && !pastTarget)
        {
            float x = originalPos.x + xSpeed * time;
            // x = (Mathf.Sign(xSpeed) )
            float y = originalPos.y + yInitSpeed * time - 0.5f * 9.81f * time * time;
            transform.position = new Vector3(x, y, 0);
            transform.Rotate(0, 0, rotationDir * angleStep, Space.Self);
            time += Time.deltaTime;
        }
        else
        {
            var iRotation = isorotations.Select(q => Quaternion.Angle(transform.rotation, q)).ArgMin();
            var rotation = isorotations[iRotation];

            if (dist > 0.001f || Quaternion.Angle(transform.rotation, rotation) > 0.5f)
            {
                transform.position = Vector3.MoveTowards(transform.position, destination, 0.05f);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, angleStep);
            }
            else
            {
                transform.rotation = Quaternion.identity;
                isMoving = false;
            }
            
        }
    }
}
