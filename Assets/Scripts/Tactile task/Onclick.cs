using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Onclick : MonoBehaviour
{
     private GamelogictrialsKitty gameLogic;
    
    private KittyBehaviour catBehaviour;

    public float time;
    public int catorder;
    public int correct;
    public GameObject sleepy; 

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
            gameLogic.sleepycat = 0;
           
        }

        if (catBehaviour.firstcat == 0)
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

    
       else if (catBehaviour.secondcat == 0)
       {
           if(catBehaviour.secondincorrect)
           {
               correct =0;
               catBehaviour.firstamplevel --;
           }

           else if (catBehaviour.secondcorrect)
           {
               correct=1;
               if (catBehaviour.firstamplevel <9)
           {catBehaviour.firstamplevel ++;
           }
               Debug.Log("correctsecond");
               gameLogic.cummulativecorrect++;
    
           }
       }
    }

   

//     public void getorder()
//     {
//         if (catBehaviour.firstcat<1)
//         {
//             catorder = 1;
//         }
//         if (catBehaviour.firstcat > 0.9f)
//         {
//             catorder = 2;
//         }



//     }

//     public void calculatecorrect()
//    {
      
//        {
//            correct = 1;
//            catBehaviour.firstamplevel ++;
//        }

//        if (catBehaviour.vibrationpresentation > 0.9f && catBehaviour.firstamplevel>1)
//        {
//            correct=0;
//            catBehaviour.firstamplevel --;
//        }
   

}
