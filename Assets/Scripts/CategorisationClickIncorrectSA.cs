using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CategorisationClickIncorrectSA : MonoBehaviour
{
   private CategorisationPretestSA logic;
    public GameObject[] stimuli;
    public GameObject cross;

    // Start is called before the first frame update
    void Start()
    {
        logic = FindObjectOfType<CategorisationPretestSA>();  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseDown()
    {
        Debug.Log("clicked");

        logic.incorrectclicked();
        cross.SetActive(true);

        StartCoroutine(clickdelay());

    }


    private IEnumerator clickdelay()
    {
        yield return new WaitForSeconds(1f); 
        cross.SetActive(false);
    }
}
