using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterGameLogic : MonoBehaviour
{
    public GameObject machine;
    public GameObject MM;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public void StartMainMenu()
   {
     machine.SetActive(false);
     MM.SetActive(true);
   }
}
