using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoShipBehaviour : MonoBehaviour
{
     private float time;

     private PirateAttackCursorBehaviour handclick;
    private float x;
    private PirateAttackDemoLogic gameLogic;
    // public GameObject clicked;
    public AudioSource cannon;

    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        gameLogic = FindObjectOfType<PirateAttackDemoLogic>();
        animator = GetComponent<Animator>();
        handclick = FindObjectOfType<PirateAttackCursorBehaviour>();
        cannon = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        x = -13 + time * gameLogic.velocity;

        gameObject.transform.position = new Vector3(x, -1, 0);

        if(handclick.clicked)
        {
            cannon.Play();
        }

        if (x > 13)
        {
            time = 0;
            gameObject.transform.position = new Vector3(-13, -1, 0);
            gameObject.SetActive(false);
            // clicked.SetActive(false);
        }
    }

    public void Restart()
    {
        time = 0;
        gameObject.transform.position = new Vector3(-13, -1, 0);
        gameObject.SetActive(false);
        // clicked.SetActive(false);
    }
}
