using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Stimulus))]
public class StimulusMover : MonoBehaviour
{
    Stimulus stimulus;
    bool isMoving = false;
    public bool IsMoving { get => isMoving; }
    public FloatVariable speed;
    public Vector3 startPosition;
    public Vector3 endPosition;
    Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        stimulus = GetComponent<Stimulus>();
        transform.position = startPosition;
        direction = (endPosition - startPosition).normalized;
    }

    // TODO change if needed
    void OnEnable() => isMoving = true;

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            transform.position += direction * Time.deltaTime * speed;

            // TODO review
            if ((endPosition - transform.position).normalized != direction)
            {
                isMoving = false;
                stimulus.LogData();
            }
        }
        // Always moving unless break trial
    }

    public void Reset()
    {
        isMoving = false;
        transform.position = startPosition;
    }
}