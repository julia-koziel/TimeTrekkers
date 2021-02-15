using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingStimulus : MonoBehaviour
{
    int[] velocity = new int[] {5, 12, 10, 15, 20, 30};
    public float position = 0;
    public float time;
    public float animtime=1.2f;
    public float x; 
    public float RT;
    public int random;
    public int currentspeed=4;
    public bool clicked; 
    public GameObject[] pop;
    private Animator anim;
    private StardustGameLogic gameLogic;
    private StardustTravelDestination stardust;
    private AudioSource sound;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        gameLogic = FindObjectOfType<StardustGameLogic>();
        stardust = FindObjectOfType<StardustTravelDestination>();
        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        x = time * velocity[currentspeed];
        gameObject.transform.position = new Vector3(x-15, -1, 0);

        if (x>30)
        {
           Reactivate();

        }
    }

    public void OnMouseDown()
    {  
        logRT();
        sound.Play(0);
        anim.SetTrigger("pop");
        gameLogic.click();
        clicked=true;

        if (time>(RT + animtime))
        {
            stardust.reset();   
            clicked=false; 
             
        } 
        
    }

    public void Reactivate()
    {

        time=0;
        x = time * velocity[currentspeed];
        clicked=false;
        if (!clicked)
        {   
            time=0;
            x = time * velocity[currentspeed];
        }
        gameLogic.reset();
        currentspeed = Random.Range(0,5);
    }

    public void logRT()
    {
        RT=time;
    }

}
