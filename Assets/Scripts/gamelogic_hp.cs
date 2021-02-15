using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gamelogic_hp : MonoBehaviour
{
    float velocity = 50.0f;        // sets the speed of the camera movement
    bool r_on = false;            // boolean to define that it should be moving
    bool l_on = false;           
    bool l2_on = false;
    float translation = 1700.0f;  // expected camera movement in the x axis

    float origin;
    float dist;

    public AudioSource sound;

    void Start()
    {
    }

    private void Update()
    {
        if (r_on)
        {
            Camera.main.transform.Translate(velocity, 0, 0);
            dist = Camera.main.gameObject.transform.position[0] - origin;
            if(dist >= translation)
            {
                r_on = false;         // close gate if the desired position has been achieved
                dist = 0;
                //Camera.main.transform.Translate(-20, 0, 0); // to adjust for 1930 not being 
                                                            //divisable by 50
            }
        }

        if (l_on)
        {
            Camera.main.transform.Translate(-velocity, 0, 0);
            dist = origin - Camera.main.gameObject.transform.position[0];
            if (dist >= translation)
            {
                l_on = false;
                dist = 0;
                //Camera.main.transform.Translate(20, 0, 0);
            }
        }

        if (l2_on) // moves two screens to the left
        {
            Camera.main.transform.Translate(-velocity, 0, 0);
            dist = origin - Camera.main.gameObject.transform.position[0];
            if (dist >= translation*2)
            {
                l2_on = false;
                dist = 0;
                //Camera.main.transform.Translate(40, 0, 0);
            }
        }
    }


    // These are used for the buttons to move in-between screens and back to the home page
    public void move_right()
    {
        r_on = true;
        origin = Camera.main.gameObject.transform.position[0];
        sound.Play();
    }

    public void move_left()
    {
        l_on = true;
        origin = Camera.main.gameObject.transform.position[0];
        sound.Play();
    }

    public void move_left2()
    {
        l2_on = true;
        origin = Camera.main.gameObject.transform.position[0];
        sound.Play();
    }
}
