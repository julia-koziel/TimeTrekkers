using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipsBehaviour : MonoBehaviour
{
    public GameObject[] targetships;
    public GameObject[] otherships;
    public GameLogicPirateAttack gameLogic;
    private int index;
    private float x;
    private int i;
    private float time =0f;
    private float position = 0;
    public int minShipSeparation = 2;
    public int blockLength =7;
    private int pipBuffer = 0;
    private int iBlock =0;
    private int[] currentBlock;
    private int trial =0;
    private int currentship =-1;
    private enum shipType

    {
        Other = 0,
        Target =1
    }




    // Start is called before the first frame update
    void Start()
    {
        Random.InitState(2);
        gameLogic = FindObjectOfType<GameLogicPirateAttack>();
        foreach (GameObject ship in targetships)
        {
            ship.SetActive(false);
            ship.transform.position = new Vector3(-13,0,0);
        }
        foreach (GameObject ship in otherships)
        {
            ship.SetActive(false);
            ship.transform.position = new Vector3(-13,0,0);
        }
    }
    void Update()
    {

    time += Time.deltaTime;
        position = time * gameLogic.velocity;

        if (position > gameLogic.distance)
        {
            iBlock = trial == 0 ? 0 : iBlock;       // Handles start and restarts
            if (trial == 0 || iBlock == currentBlock.Length)   // Create new shuffled block at end of old block, or at start of trials
            {
                createNextBlock();
                iBlock = 0;
            }
            currentship = currentBlock[iBlock];    // Reads trial type from currentBlock
            iBlock++;

            switch (currentship)
            {
                case (int)shipType
                .Other:
                    do
                    {
                        index = Random.Range(0, otherships.Length);
                    } while (otherships[index].activeSelf);

                    otherships[index].SetActive(true);
                    otherships[index].GetComponent<Ship2MouseClick>().trial = trial;
                    break;

                case (int)shipType.Target:
                    do
                    {
                        index = Random.Range(0, targetships.Length);
                    } while (targetships[index].activeSelf);

                    targetships[index].SetActive(true);
                    targetships[index].GetComponent<ShipTargetMouseClick>().trial = trial;
                    break;
                default:
                    break;
            }

            trial++;
            time = 0;
        }
            

        if(gameLogic.quit){
            gameObject.SetActive(false);
        }

    }

    void createNextBlock() {
        
        currentBlock = new int[blockLength];                    
        // Allows for possible online adjustment of blockLength

        System.Random rnd = new System.Random();                          

        for (int i = 0; i < blockLength; i++)
        {
            currentBlock[i] = (int) shipType
            .Other;              
            // Set default as non-Pip car
        }
        
        int pipCarIndex = rnd.Next(pipBuffer, blockLength);             
        // Random index within block, kept greater than buffer from most recent pip car
        
        currentBlock[pipCarIndex] = (int) shipType
        .Target;                     
        // Set pip car value within block (enum value 1)
        
        // Buffer to track space needed from pipCar trial in current block to pipCar trial in next block
        int buffer = pipCarIndex - (blockLength - 1 - minShipSeparation);      

        pipBuffer = buffer > 0 ? buffer : 0;                            
        // Ignore buffer when negative
    }
}
