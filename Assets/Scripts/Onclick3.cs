using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Onclick3 : MonoBehaviour
{
    private GamelogictrialsKitty gameLogic;
    
    private KittyBehaviour catBehaviour;

    public float time;
    public int catorder;
    public int correct;
    
    public int sleepycat;
    // Start is called before the first frame update
    void Start()
    {
        gameLogic = FindObjectOfType<GamelogictrialsKitty>();
        catBehaviour = FindObjectOfType<KittyBehaviour>();
        time = gameLogic.SetTime();
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    public void OnMouseDown()
    {
        if (gameLogic.catIsClickable)
        {
            gameLogic.newtrial();
            gameLogic.sleepycat = 2;
           
        }

        if (catBehaviour.firstcat == 2)
    {
       if (catBehaviour.firstincorrect)
       {
           correct = 0;

          if(catBehaviour.firstamplevel >0)
           {
           catBehaviour.firstamplevel --;
           }
       }

       else if (catBehaviour.firstcorrect)
       {
           correct=1;
            if (catBehaviour.firstamplevel <9)
           {catBehaviour.firstamplevel ++;
           }
           Debug.Log("CatCorrect");
           gameLogic.cummulativecorrect++;
       }
    }

    
       else if (catBehaviour.secondcat == 2)
       {
           if(catBehaviour.secondincorrect)
           {
               correct =0;
               catBehaviour.firstamplevel --;
           }

           else if (catBehaviour.secondcorrect)
           {
               correct=1;
               gameLogic.cummulativecorrect++;
                if (catBehaviour.firstamplevel <9)
           {catBehaviour.firstamplevel ++;
           }
               Debug.Log("correctsecond");
    
           }
       }
}

}
   